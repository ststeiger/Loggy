
USE TestDB




-- SELECT * 
DELETE 
FROM TreePaths
WHERE ancestor IN 
(
	SELECT descendant FROM TreePaths WHERE ancestor = 1 
)
