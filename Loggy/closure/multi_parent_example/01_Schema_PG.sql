
CREATE TABLE IF NOT EXISTS T_Comments
(
	 COM_Id bigserial NOT NULL
	,COM_Text national character varying(255) NULL
	,CONSTRAINT PK_T_Comments PRIMARY KEY(COM_Id)
);

GO



CREATE TABLE IF NOT EXISTS T_Comments_Paths
(
	 path_id bigserial NOT NULL
	,path_com_id bigint NOT NULL CONSTRAINT FK_T_Comments_Paths_T_Comments REFERENCES T_Comments(COM_Id) ON DELETE CASCADE 
	,CONSTRAINT PK_T_Comments_Paths PRIMARY KEY(path_id) 
);

GO



CREATE TABLE IF NOT EXISTS T_Comments_Closure
( 
	 path_id bigint NOT NULL CONSTRAINT FK_T_Comments_Closure_T_Comments_Paths REFERENCES T_Comments_Paths(path_id) ON DELETE CASCADE 
	,ancestor bigint NOT NULL
	,descendant bigint NOT NULL
	,depth int NULL
	,CONSTRAINT PK_T_Comments_Closure PRIMARY KEY(path_id, ancestor, descendant)
); 

GO

-- Function: insertcomment(bigint, bigint)

-- DROP FUNCTION insertcomment(bigint, bigint);

CREATE OR REPLACE FUNCTION insertcomment(__parent bigint, __child bigint)
  RETURNS void AS
$BODY$ 
DECLARE
	__maxStart timestamp without time zone;  
	__minEnd timestamp without time zone;  
	__interval int; 
BEGIN 
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

	INNER JOIN T_Comments_Closure AS c 
		-- ON p.descendant = @__parent AND c.ancestor = @__child 
		ON p.descendant = __parent AND c.ancestor = __child 
	;
	-- RETURN 0;
END; 
$BODY$
  LANGUAGE plpgsql VOLATILE
  COST 100;
ALTER FUNCTION insertcomment(bigint, bigint)
  OWNER TO postgres;




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
