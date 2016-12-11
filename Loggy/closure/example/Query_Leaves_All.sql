
SELECT 
	 T_Comments.*
	,T_Comments_Closure.* 
FROM T_Comments 
LEFT JOIN T_Comments_Closure
	ON T_Comments_Closure.ancestor = T_Comments.COM_Id
	AND T_Comments_Closure.depth > 0

WHERE T_Comments.COM_Id < 8 
AND T_Comments_Closure.descendant IS NULL 
