
SELECT 
	 T_Comments.COM_Id 
	,T_Comments.COM_Text
	,ancestor 
	,descendant 
	,depth 

	,
	(
		SELECT COUNT(*) FROM T_CommentClosure AS tp 
		WHERE tp.ancestor = T_Comments.COM_Id AND tp.depth = 1 
	) AS ChildCount 

FROM T_Comments 

LEFT JOIN T_CommentClosure 
	ON T_CommentClosure.descendant = T_Comments.COM_Id
	
WHERE T_CommentClosure.ancestor = 1 

-- ORDER BY comment_date 
