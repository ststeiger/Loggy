
-- http://www.unlimitedtricks.com/sorting-a-subtree-in-a-closure-table-hierarchical-data-structure/

-- DECLARE @__in_rootnodeId AS bigint 
-- SET @__in_rootnodeId = 8 
-- SET @__in_rootnodeId = 1 


SELECT 
	 COALESCE(ctDescendants.ancestor, 0) AS parent_id 
	,ctAncestors.descendant AS child_id 
	--,tClosureItemsTable.COM_Id 
	--,tClosureItemsTable.COM_Text 
	 
	--,GROUP_CONCAT(ctAncestors.ancestor ORDER BY ctAncestors.depth DESC) AS breadcrumbs_id 
	--,array_to_string(array_agg(ctAncestors.ancestor ORDER BY ctAncestors.depth DESC), ',') as breadcrumbs_id
	--,GROUP_CONCAT(breadcrumb_data.COM_Text ORDER BY ctAncestors.depth DESC) AS breadcrumbs 
	--,string_agg(breadcrumb_data.COM_Text, ',' ORDER BY ctAncestors.depth DESC) AS breadcrumbs 
	--,array_to_string(array_agg(breadcrumb_data.COM_Text ORDER BY ctAncestors.depth DESC), ',') as breadcrumbs_manual
	 
	,
	(
		SELECT 
			-- breadcrumb.ancestor AS 'text()'  -- Remove substring for this 
			-- ', ' + CAST(breadcrumb.ancestor AS nvarchar(36)) AS [text()] 
			--', ' + CAST(breadcrumb_data.COM_Text AS nvarchar(36)) AS [text()] 
			string_agg(breadcrumb_data.COM_Text, ',' ORDER BY breadcrumb.depth DESC) AS breadcrumbs 
		FROM T_Comments_Closure AS breadcrumb 
		
		LEFT JOIN T_Comments AS breadcrumb_data 
			ON breadcrumb_data.COM_Id = breadcrumb.ancestor 
			
		WHERE (breadcrumb.descendant = ctAncestors.descendant) 
		AND 
		( 
			breadcrumb.path_id = ctAncestors.path_id 
			OR breadcrumb.depth = 0 
		) 
		
	) AS breadcrumbs 
	
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
	
INNER JOIN T_Comments AS tClosureItemsTable 
	ON (tClosureItemsTable.COM_Id = ctAncestors.descendant) 
	
WHERE (1=1) 

GROUP BY 
	 ctAncestors.path_id
	,ctAncestors.descendant 
	,ctDescendants.ancestor 
	,tClosureItemsTable.COM_Id 
	,tClosureItemsTable.COM_Text 
	 
ORDER BY breadcrumbs ASC  -- DESC
