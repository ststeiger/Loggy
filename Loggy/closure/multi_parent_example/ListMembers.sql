
/*
SELECT 
	 COM_Id
	,COM_Text
FROM T_Comments 
*/


-- List all Descendants
SELECT 
	descendant AS descendants
FROM T_Comments_Closure
WHERE ancestor = 5 
AND descendant <> 5



-- List all ancestors
SELECT 
	 ancestor AS ancestors
FROM T_Comments_Closure
WHERE descendant = 5 
AND ancestor <> 5





-- List Members
SELECT 
	descendant AS members 
FROM T_Comments_Closure
WHERE ancestor = 5 

UNION 

SELECT 
	 ancestor AS members 
FROM T_Comments_Closure
WHERE descendant = 5 
