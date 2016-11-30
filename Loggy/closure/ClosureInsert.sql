
USE [TestDB]
GO

-- SELECT * FROM Comments 


DECLARE @__Parent bigint
DECLARE @__Child bigint

SET @__Parent = 20
SET @__Child = 23


INSERT INTO TreePaths
(
	 ancestor
	,descendant
	,depth
)
SELECT 
	 p.ancestor AS Parent 
	,c.descendant AS Child 
	,p.depth + c.depth + 1 AS Calcdepth 
FROM TreePaths AS p 

-- ,TreePaths AS c  -- == CROSS JOIN TreePaths AS c 

INNER JOIN TreePaths AS c 
	ON p.descendant = @__Parent AND c.ancestor = @__Child

--WHERE p.descendant = @__Parent AND c.ancestor = @__Child 
