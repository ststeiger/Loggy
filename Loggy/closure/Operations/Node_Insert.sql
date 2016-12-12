
-- SELECT * FROM Comments 


DECLARE @__parent bigint
DECLARE @__child bigint

SET @__parent = 20
SET @__child = 23


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
	ON p.descendant = @__parent 
	AND c.ancestor = @__child

--WHERE p.descendant = @__parent AND c.ancestor = @__child 
