
DECLARE @__nodeid AS bigint
SET @__nodeid = 6

--DELETE q 
SELECT * 
FROM TreePaths q 
 --NATURAL JOIN 
INNER JOIN 
(
	SELECT a.ancestor, d.descendant
	FROM TreePaths a
	CROSS JOIN  TreePaths d
	WHERE a.descendant = @__nodeid 
	AND a.ancestor != @__nodeid 
	AND d.ancestor= @__nodeid 
) AS t
	ON t.ancestor = q.ancestor 
	AND t.descendant = q.descendant
