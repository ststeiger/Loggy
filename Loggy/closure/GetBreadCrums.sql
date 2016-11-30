
USE TestDB;

-- http://www.unlimitedtricks.com/sorting-a-subtree-in-a-closure-table-hierarchical-data-structure/

DECLARE @__in_rootnodeId AS bigint 
SET @__in_rootnodeId = 8 
-- SET @__in_rootnodeId = 1 


SELECT 
	 COALESCE(ctDescendants.ancestor, 0) AS parent_id 
	,ctAncestors.descendant AS child_id 
	,tClosureItemsTable.comment_id 
	,tClosureItemsTable.comment	 
	
	-- ,GROUP_CONCAT(breadcrumb.ancestor ORDER BY breadcrumb.depth DESC) AS breadcrumbs
	,
	SUBSTRING
	(
		(
			SELECT 
				-- breadcrumb.ancestor AS 'text()'  -- Remove substring for this 
				-- ', ' + CAST(breadcrumb.ancestor AS nvarchar(36)) AS 'text()'
				', ' + CAST(breadcrumb_data.comment AS nvarchar(36)) AS 'text()'
			FROM TreePaths AS breadcrumb 
			
			LEFT JOIN Comments AS breadcrumb_data
				ON breadcrumb_data.comment_id = breadcrumb.ancestor

			WHERE (breadcrumb.descendant = ctAncestors.descendant) 

			ORDER BY breadcrumb.depth DESC
			FOR XML PATH('')
		)
		,2
		,8000
	) AS breadcrumbs 

	,
	(
		SELECT COUNT(*) FROM TreePaths AS tp 
		WHERE tp.ancestor = tClosureItemsTable.comment_id AND tp.depth = 1 
	) AS ChildCount 
	
FROM TreePaths AS ctAncestors  

-- Must be left join, for root node
LEFT JOIN TreePaths AS ctDescendants 
	ON (ctDescendants.descendant = ctAncestors.descendant) 
	AND (ctDescendants.depth = 1) 

-- INNER JOIN just in case item has been somehow deleted when FK disabled 
INNER JOIN Comments AS tClosureItemsTable  
	ON (ctAncestors.descendant = tClosureItemsTable.comment_id) 

-- For group_concat 
-- INNER JOIN category_closure AS breadcrumb 
--	ON (ctAncestors.descendant = breadcrumb.descendant) 

WHERE (1=1) 
--AND (ctAncestors.ancestor = @__in_rootnodeId) -- ROOT node id 
AND 
( 
    (ctAncestors.ancestor = @__in_rootnodeId) -- ROOT node id 
    OR 
    (@__in_rootnodeId IS NULL) 
) 

-- AND tClosureItemsTable.active = 1 

GROUP BY 
	 ctAncestors.descendant 
	,ctDescendants.ancestor 
	,tClosureItemsTable.comment_id 
	,tClosureItemsTable.comment 

ORDER BY breadcrumbs  ASC  --DESC
