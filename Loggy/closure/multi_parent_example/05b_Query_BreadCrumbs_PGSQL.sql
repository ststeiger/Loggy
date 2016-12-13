SELECT 
	 ctAncestors.path_id
	,ctAncestors.descendant 
	,ctDescendants.ancestor 

	--,GROUP_CONCAT(ctAncestors.ancestor ORDER BY ctAncestors.depth DESC) AS breadcrumbs_id 
	,array_to_string(array_agg(ctAncestors.ancestor ORDER BY ctAncestors.depth DESC), ',') as breadcrumbs_id
	--,GROUP_CONCAT(breadcrumb_data.COM_Text ORDER BY ctAncestors.depth DESC) AS breadcrumbs 
	,string_agg(breadcrumb_data.COM_Text, ',' ORDER BY ctAncestors.depth DESC) AS breadcrumbs 
	--,array_to_string(array_agg(breadcrumb_data.COM_Text ORDER BY ctAncestors.depth DESC), ',') as breadcrumbs_oldPgPriorStringAgg 
	
	,
	(
		SELECT COUNT(*) FROM T_Comments_Closure AS tp 
		WHERE tp.ancestor = tClosureItemsTable.COM_Id AND tp.depth = 1 
	) AS ChildCount 
FROM T_Comments_Closure AS ctAncestors  

-- Must be left join, for root node
LEFT JOIN T_Comments_Closure AS ctDescendants 
	ON (ctDescendants.descendant = ctAncestors.descendant) 
	AND 
	(
		ctDescendants.path_id = ctAncestors.path_id 
		--OR ctDescendants.depth = 0 
	)
	AND (ctDescendants.depth = 1) 
	
INNER JOIN T_Comments_Closure AS breadcrumb 
	ON (breadcrumb.descendant = ctAncestors.descendant) 
	AND 
	( 
		   breadcrumb.path_id = ctAncestors.path_id 
		OR breadcrumb.depth = 0 
	) 
	
LEFT JOIN T_Comments AS breadcrumb_data 
	ON breadcrumb_data.COM_Id = breadcrumb.ancestor 
	
INNER JOIN T_Comments AS tClosureItemsTable 
	ON (tClosureItemsTable.COM_Id = ctAncestors.descendant) 
	
WHERE (1=1) 

-- Magic line, so we don't have to group prior to breadcrumbing (remove duplicates)...
AND 
( 
	   ctAncestors.depth = 1 
	OR ctAncestors.depth = 0 
) 

GROUP BY 
	 ctAncestors.path_id 
	,ctAncestors.descendant 
	,ctDescendants.ancestor 
	,tClosureItemsTable.COM_Id 
	,tClosureItemsTable.COM_Text 
	 
ORDER BY breadcrumbs 
