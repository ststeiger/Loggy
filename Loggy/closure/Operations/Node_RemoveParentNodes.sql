
DECLARE @__nodeid AS bigint
SET @__nodeid = 6


--DELETE a 
SELECT a.* 
FROM TreePaths AS a 

INNER JOIN TreePaths AS d 
	ON a.descendant = d.descendant 

LEFT JOIN TreePaths AS x 
	ON x.ancestor = d.ancestor 
	AND x.descendant = a.ancestor 

WHERE d.ancestor = @__nodeid 
AND x.ancestor IS NULL 
