
SELECT 
	 COM_Id 
	,COM_Text 
	,T_Comments_Closure.* 
	,leaves.* 
FROM T_Comments_Closure 

INNER JOIN T_Comments 
	ON T_Comments.COM_Id = T_Comments_Closure.descendant 
	AND  T_Comments_Closure.ancestor = 4 -- Only descendants of this comment 
	AND  T_Comments_Closure.depth > 0 -- Don't return self 
	-- AND T_Comments_Closure.depth = 1 -- Direct leaves 

LEFT JOIN T_Comments_Closure AS leaves 
	ON leaves.ancestor = T_Comments.COM_Id 
	AND leaves.depth > 0 
	
WHERE (1=1) 
AND leaves.descendant IS NULL 
