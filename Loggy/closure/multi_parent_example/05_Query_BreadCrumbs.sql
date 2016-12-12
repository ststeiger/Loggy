
SELECT 
	 CTE.COM_Id 
	,CTE.COM_Text 
	,breadcrumbs_id 
	,ISNULL(breadcrumbs + ', ', '') + COM_Text AS path 
FROM 
(
	SELECT 
		ctAncestors.path_id, 
		SUBSTRING
		(
			(
				SELECT 
					-- breadcrumb.ancestor AS 'text()'  -- Remove substring for this 
					', ' + RIGHT('000000' + CAST(breadcrumb.ancestor AS nvarchar(36)), 6) AS [text()] 
					-- ', ' + CAST(breadcrumb_data.COM_Text AS nvarchar(36)) AS [text()] 
				FROM T_Comments_Closure AS breadcrumb 
				
				LEFT JOIN T_Comments AS breadcrumb_data
					ON breadcrumb_data.COM_Id = breadcrumb.ancestor

				WHERE (breadcrumb.descendant = ctAncestors.descendant) 
				AND breadcrumb.path_id = ctAncestors.path_id 
				
				ORDER BY breadcrumb.depth DESC 
				FOR XML PATH(''), TYPE
			).value('.', 'nvarchar(MAX)') 
			,2
			,8000
		) AS breadcrumbs_id 
		
		,
		SUBSTRING
		(
			(
				SELECT 
					-- breadcrumb.ancestor AS 'text()'  -- Remove substring for this 
					-- ', ' + CAST(breadcrumb.ancestor AS nvarchar(36)) AS [text()] 
					', ' + CAST(breadcrumb_data.COM_Text AS nvarchar(36)) AS [text()] 
				FROM T_Comments_Closure AS breadcrumb 
				
				LEFT JOIN T_Comments AS breadcrumb_data
					ON breadcrumb_data.COM_Id = breadcrumb.ancestor
					--ON breadcrumb_data.COM_Id = breadcrumb.descendant
					
				WHERE (breadcrumb.descendant = ctAncestors.descendant) 
				AND breadcrumb.path_id = ctAncestors.path_id 
				AND breadcrumb.depth > 0
				
				GROUP BY 
					 breadcrumb_data.COM_Id 
					,breadcrumb_data.COM_Text 
					,breadcrumb.depth 
					 
				ORDER BY breadcrumb.depth DESC 
				
				FOR XML PATH(''), TYPE
			).value('.', 'nvarchar(MAX)') 
			,3
			,8000
		) AS breadcrumbs 
		
		,tClosureItemsTable.COM_Id 
		,tClosureItemsTable.COM_Text 
		
		--,ctAncestors.*
		--,ctDescendants.* 
		--,breadcrumb_data.*
		
	FROM T_Comments_Closure AS ctAncestors  

	-- Must be left join, for root node
	LEFT JOIN T_Comments_Closure AS ctDescendants 
		ON (ctDescendants.descendant = ctAncestors.descendant) 
		AND ctDescendants.path_id = ctAncestors.path_id 
		AND (ctDescendants.depth = 1) 

	-- INNER JOIN just in case item has been somehow deleted when FK disabled 
	INNER JOIN T_Comments AS tClosureItemsTable  
		ON (tClosureItemsTable.COM_Id = ctAncestors.descendant) 

	--INNER JOIN T_Comments AS breadcrumb_data ON breadcrumb_data.COM_Id = ctAncestors.ancestor 

	WHERE ctAncestors.depth > 0 
	OR ctAncestors.path_id = 1 
	
	-- ORDER BY path_id, depth
) AS CTE 

GROUP BY 
	 COM_Id
	,COM_Text
	,breadcrumbs
	,breadcrumbs_id
	 
--ORDER BY Path 
ORDER BY breadcrumbs_id, path 
-- ORDER BY LEN(breadcrumbs_id), breadcrumbs_id 
