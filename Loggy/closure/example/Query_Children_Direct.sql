SELECT 
	 ancestor
	,descendant
	,depth
	,COM_Text
FROM T_Comments_Closure
LEFT JOIN T_Comments 
	ON T_Comments.COM_Id = descendant

WHERE ancestor = 1
AND depth = 1
