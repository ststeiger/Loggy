
-- http://www.unlimitedtricks.com/sorting-a-subtree-in-a-closure-table-hierarchical-data-structure/

-- DECLARE @__in_rootnodeId AS bigint 
-- SET @__in_rootnodeId = 8 
-- SET @__in_rootnodeId = 1 


SELECT 
	 COALESCE(ctDescendants.ancestor, 0) AS parent_id 
	,ctAncestors.descendant AS child_id 
	,tClosureItemsTable.COM_Id 
	,tClosureItemsTable.COM_Text 

	,GROUP_CONCAT(ctAncestors.ancestor ORDER BY ctAncestors.depth DESC) AS breadcrumbs_id 
	-- ,GROUP_CONCAT(breadcrumb_data.COM_Text ORDER BY ctAncestors.depth DESC) AS breadcrumbs 
,GROUP_CONCAT(breadcrumb_data.COM_Text ORDER BY ctAncestors.depth) AS breadcrumbs 
,GROUP_CONCAT(breadcrumb_data.COM_Text ORDER BY ctAncestors.depth DESC SEPARATOR '-') AS breadcrumbs2 




/*	
,
	SUBSTRING
	(
		(
			SELECT 
				-- breadcrumb.ancestor AS 'text()'  -- Remove substring for this 
				-- ', ' + CAST(breadcrumb.ancestor AS nvarchar(36)) AS 'text()'
				', ' + CAST(breadcrumb_data.comment AS nvarchar(36)) AS 'text()'
			FROM T_CommentClosure AS breadcrumb 
			
			LEFT JOIN Comments AS breadcrumb_data
				ON breadcrumb_data.COM_Id = breadcrumb.ancestor

			WHERE (breadcrumb.descendant = ctAncestors.descendant) 

			ORDER BY breadcrumb.depth DESC
			FOR XML PATH('')
		)
		,2
		,8000
	) AS breadcrumbs 
*/	

	,
	(
		SELECT COUNT(*) FROM T_CommentClosure AS tp 
		WHERE tp.ancestor = tClosureItemsTable.COM_Id AND tp.depth = 1 
	) AS ChildCount 

FROM T_CommentClosure AS ctAncestors  

-- Must be left join, for root node
LEFT JOIN T_CommentClosure AS ctDescendants 
	ON (ctDescendants.descendant = ctAncestors.descendant) 
	AND (ctDescendants.depth = 1) 

-- INNER JOIN just in case item has been somehow deleted when FK disabled 
INNER JOIN T_Comments AS tClosureItemsTable  
	ON (ctAncestors.descendant = tClosureItemsTable.COM_Id) 
	
INNER JOIN T_Comments AS breadcrumb_data
	ON breadcrumb_data.COM_Id = ctAncestors.ancestor 
	
WHERE (1=1) 
-- AND (ctAncestors.ancestor = @__in_rootnodeId) -- ROOT node id 
AND 
( 
    -- ( ctAncestors.ancestor = @__in_rootnodeId) -- ROOT node id 
    (1=2) 
	OR 
    (@__in_rootnodeId IS NULL) 
) 

-- AND tClosureItemsTable.active = 1 

GROUP BY 
	 ctAncestors.descendant 
	,ctDescendants.ancestor 
	,tClosureItemsTable.COM_Id 
	,tClosureItemsTable.COM_Text 

ORDER BY breadcrumbs ASC  -- DESC
