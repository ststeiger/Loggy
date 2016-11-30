

USE TestDB

--INSERT INTO TreePaths (ancestor, descendant, depth)
SELECT 
	 supertree.ancestor 
	,subtree.descendant 
	,supertree.depth + subtree.depth + 1 
FROM TreePaths AS supertree 

INNER JOIN TreePaths AS subtree 
	ON subtree.ancestor = 11 --'D' 
	AND supertree.descendant = 9 -- 'B' 

