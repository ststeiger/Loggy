
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_DATA_InsertComment]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_DATA_InsertComment]
GO





-- ============================================================= 
-- Author:		  Stefan Steiger 
-- E-Mail:        stefan.steiger [at] cor-management [dot] ch 
-- Create date:   12.12.2016 
-- Last modified: 12.12.2016 
-- Description:	  Insert comment-closure for graph  
-- ============================================================= 
CREATE PROCEDURE [dbo].[sp_DATA_InsertComment] 
	@__parent bigint, 
	@__child bigint 
AS
BEGIN
	SET NOCOUNT ON;
	
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
		ON p.descendant = @__parent 
		AND c.ancestor = @__child 
		
	WHERE dbo.CONCAT(CAST(p.ancestor AS varchar(20)), ',', CAST(c.descendant AS varchar(20))) NOT IN 
	(
		SELECT 
			dbo.CONCAT(CAST(ancestor AS varchar(20)), ',', CAST(descendant AS varchar(20))) 
		FROM T_Comments_Closure
	);
END


GO







DELETE FROM T_Comments;


SET IDENTITY_INSERT T_Comments ON

INSERT INTO T_Comments(COM_Id, COM_Text)
      SELECT 1 as id, 'Alle' as comment 
UNION SELECT 2 as id, 'Kunde A' as comment 
UNION SELECT 3 as id, 'Admin' as comment 
UNION SELECT 4 as id, 'Normal' as comment
UNION SELECT 5 as id, 'Stao 1&2' as comment
UNION SELECT 6 as id, 'Stao 1' as comment
UNION SELECT 7 as id, 'Stao 2' as comment
UNION SELECT 8 as id, 'Stao 3' as comment 
UNION SELECT 9 as id, 'Stao 4' as comment 
UNION SELECT 10 as id, 'Stao 1&3' as comment
ORDER BY id 
;

SET IDENTITY_INSERT T_Comments OFF


-- SELECT * FROM Comments 

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



EXEC sp_DATA_InsertComment 1, 2; 
EXEC sp_DATA_InsertComment 2, 3; 
EXEC sp_DATA_InsertComment 2, 4; 
EXEC sp_DATA_InsertComment 3, 5; 
EXEC sp_DATA_InsertComment 3, 6; 
EXEC sp_DATA_InsertComment 3, 7; 
EXEC sp_DATA_InsertComment 3, 8; 
EXEC sp_DATA_InsertComment 3, 9; 
EXEC sp_DATA_InsertComment 3, 10; 
EXEC sp_DATA_InsertComment 5, 6; 
EXEC sp_DATA_InsertComment 5, 7;

EXEC sp_DATA_InsertComment 10, 6; 
EXEC sp_DATA_InsertComment 10, 8; 



SELECT * FROM T_Comments_Closure;
