
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
	
	DECLARE @__path_id bigint 
	
	-- With Value: 
	-- INSERT INTO T_Comments_Paths(a) OUTPUT INSERTED.path_id VALUES(123);
	
	-- Insert Without Value
	-- INSERT INTO T_Comments_Paths DEFAULT VALUES;  
	-- INSERT INTO T_Comments_Paths OUTPUT INSERTED.path_id DEFAULT VALUES;
	
	
	-- DECLARE @outputTable TABLE (id bigint); 
	-- INSERT INTO T_Comments_Paths OUTPUT INSERTED.path_id INTO @outputTable DEFAULT VALUES; 
	-- SET @__path_id = (SELECT TOP 1 id FROM @outputTable); 
	
	DECLARE @outputTable TABLE (id bigint); 
	INSERT INTO T_Comments_Paths(path_com_id)
	OUTPUT INSERTED.path_id INTO @outputTable 
	VALUES(@__child);
	SET @__path_id = (SELECT TOP 1 id FROM @outputTable); 
	
	
	INSERT INTO T_Comments_Closure
	(
		 path_id 
		,ancestor
		,descendant
		,depth
	)
	SELECT 
		 @__path_id AS path_id 
		,p.ancestor AS parent 
		,c.descendant AS child 
		,p.depth + c.depth + 1 AS calcDepth 
	FROM T_Comments_Closure AS p 
	
	INNER JOIN T_Comments_Closure AS c 
		ON p.descendant = @__parent 
		AND c.ancestor = @__child 
	;
END


GO

