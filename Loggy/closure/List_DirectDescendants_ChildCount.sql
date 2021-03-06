
SELECT 
	 Comments.comment_id 
	,Comments.comment 
	,ancestor 
	,descendant 
	,depth 

	,
	(
		SELECT COUNT(*) FROM TreePaths AS tp 
		WHERE tp.ancestor = Comments.comment_id 
		AND tp.depth = 1 
	) AS ChildCount 
FROM Comments 

LEFT JOIN TreePaths --AS tp 
	ON TreePaths.descendant = Comments.comment_id 
	
WHERE TreePaths.ancestor = 1 
AND depth = 1 

ORDER BY comment_date 
