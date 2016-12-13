
SELECT 
	 COM_Id
	,COM_Text
FROM T_Comments 
WHERE COM_Id NOT IN 
(
	SELECT 
		 descendant 
	FROM T_Comments_Closure
	WHERE ancestor <> descendant 
)
