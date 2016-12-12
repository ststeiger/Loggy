

DECLARE @__Parent bigint
DECLARE @__Child bigint

SET @__Parent = 5
SET @__Child = 6


INSERT INTO T_Comments_Closure
(
	 ancestor
	,descendant
	,depth
)
SELECT 
	 p.ancestor AS Parent 
	,c.descendant AS Child 
	,p.depth + c.depth + 1 AS Calcdepth 
FROM T_Comments_Closure AS p 

-- ,TreePaths AS c  -- == CROSS JOIN TreePaths AS c 

INNER JOIN T_Comments_Closure AS c 
	ON p.descendant = @__Parent 
	AND c.ancestor = @__Child 
	
WHERE dbo.CONCAT(CAST(p.ancestor AS varchar(20)), ',', CAST(c.descendant AS varchar(20))) NOT IN 
(
	SELECT 
		dbo.CONCAT(CAST(ancestor AS varchar(20)), ',', CAST(descendant AS varchar(20))) 
	FROM T_Comments_Closure
)
