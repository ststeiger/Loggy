
DROP TABLE IF EXISTS bar;

CREATE TEMPORARY TABLE bar AS
WITH CTE AS
(
	INSERT INTO T_Comments (com_text)
	SELECT 'abc'
	UNION SELECT 'def' 
	RETURNING com_id, 'abc'::text as foo 
) 
SELECT * FROM CTE 

SELECT * FROM bar 

-- --------------------------------------------------

WITH CTE AS 
(
	INSERT INTO T_Comments_Paths(path_com_id)
	VALUES(__child)
	RETURNING path_id 
) 
-- __path_id := (SELECT path_id FROM CTE LIMIT 1); 
SELECT path_id into __path_id from CTE LIMIT 1;
