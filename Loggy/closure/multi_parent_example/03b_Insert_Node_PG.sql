
CREATE OR REPLACE FUNCTION sp_DATA_InsertComment(__parent bigint, __child bigint)
  RETURNS void AS
$BODY$ 
DECLARE
	-- __maxStart timestamp without time zone;  
	-- __minEnd timestamp without time zone;  
	__path_id bigint; 
BEGIN 
	WITH CTE AS 
	(
		INSERT INTO T_Comments_Paths(path_com_id)
		VALUES(__child)
		RETURNING path_id 
	) 
	-- __path_id := (SELECT path_id FROM CTE LIMIT 1); 
	SELECT path_id into __path_id from CTE LIMIT 1;
	
	
	INSERT INTO T_Comments_Closure
	(
		 path_id 
		,ancestor
		,descendant
		,depth
	)
	SELECT 
		 __path_id AS path_id 
		,p.ancestor AS parent 
		,c.descendant AS child 
		,p.depth + c.depth + 1 AS calcDepth 
	FROM T_Comments_Closure AS p 
	
	INNER JOIN T_Comments_Closure AS c 
		ON p.descendant = __parent 
		AND c.ancestor = __child 
	;
END; 
$BODY$
  LANGUAGE plpgsql VOLATILE
  COST 100;

