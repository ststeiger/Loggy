


DROP FUNCTION IF EXISTS InsertComment(__parent bigint, __child bigint);

CREATE OR REPLACE FUNCTION InsertComment(__parent bigint, __child bigint)
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
END; 
$BODY$
  LANGUAGE plpgsql VOLATILE
;




DELETE FROM T_Comments;


-- SET IDENTITY_INSERT T_Comments ON

INSERT INTO T_Comments(COM_Id, COM_Text)
      SELECT 1 as id, 'Alle' as comment 
UNION SELECT 2 as id, 'Kunde' as comment 
UNION SELECT 3 as id, 'Admin' as comment 
UNION SELECT 4 as id, 'Normal' as comment
UNION SELECT 5 as id, 'Stao 1&2' as comment
UNION SELECT 6 as id, 'Stao 1' as comment
UNION SELECT 7 as id, 'Stao 2' as comment
UNION SELECT 8 as id, 'Stao3' as comment 
ORDER BY id 
;

-- SET IDENTITY_INSERT T_Comments OFF


DELETE FROM T_Comments_Closure;

INSERT INTO T_Comments_Closure
(
	 ancestor 
	,descendant 
	,depth 
) 
SELECT 
	 COM_Id AS ancestor
	,COM_Id AS descendant
	,0 AS depth
FROM T_Comments
;

      SELECT InsertComment(1,2) 
UNION SELECT InsertComment(2,3)
UNION SELECT InsertComment(2,4)
UNION SELECT InsertComment(3,5)
UNION SELECT InsertComment(3,6)
UNION SELECT InsertComment(3,7)
UNION SELECT InsertComment(3,8)
UNION SELECT InsertComment(5,6)
UNION SELECT InsertComment(5,7)
;


SELECT * FROM T_Comments_Closure;



