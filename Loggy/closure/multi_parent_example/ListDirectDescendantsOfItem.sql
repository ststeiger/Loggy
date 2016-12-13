
-- List Descendants
SELECT 
	 descendant AS descendants
	,COM_Text
	,*
FROM T_Comments_Closure

LEFT JOIN T_Comments
	ON COM_Id = descendant 

WHERE ancestor = 2
AND descendant <> 2
and depth = 1 
