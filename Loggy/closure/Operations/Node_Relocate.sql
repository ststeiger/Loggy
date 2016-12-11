
DECLARE @__nodeid AS bigint
SET @__nodeid = 9 -- B
SET @__nodeid = 13 -- F


DELETE a 
--SELECT a.* 
FROM TreePaths AS a 

INNER JOIN TreePaths AS d 
	ON a.descendant = d.descendant 

LEFT JOIN TreePaths AS x 
	ON x.ancestor = d.ancestor AND x.descendant = a.ancestor 

WHERE d.ancestor = @__nodeid 
AND x.ancestor IS NULL 
;


DECLARE @__newancestor AS bigint
SET @__newancestor = 11 -- D
SET @__newancestor = 10 -- C

DECLARE @__newdescendant AS bigint
--SET @__newdescendant = 9 -- B
SET @__newdescendant = 13 -- F

INSERT INTO TreePaths (ancestor, descendant, depth) 
SELECT 
	 supertree.ancestor 
	,subtree.descendant 
	,supertree.depth + subtree.depth + 1 
FROM TreePaths AS supertree 

INNER JOIN TreePaths AS subtree 
	ON subtree.ancestor = @__newdescendant 
	AND supertree.descendant = @__newancestor 
;
