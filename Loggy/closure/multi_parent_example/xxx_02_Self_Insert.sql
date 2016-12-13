
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_DATA_SelfInsertComment]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_DATA_SelfInsertComment]
GO




-- ============================================================= 
-- Author:		  Stefan Steiger 
-- E-Mail:        stefan.steiger [at] cor-management [dot] ch 
-- Create date:   12.12.2016 
-- Last modified: 12.12.2016 
-- Description:	  Insert comment self-closure for graph  
-- ============================================================= 
CREATE PROCEDURE [dbo].[sp_DATA_SelfInsertComment] 
AS
BEGIN
	SET NOCOUNT ON;
	
	
	-- DELETE FROM T_Comments_Closure 
	-- DELETE FROM T_Comments_Paths 
	
	
	DECLARE @outputTable TABLE (path_id bigint, com_id bigint); 
	DECLARE @__com_id AS bigint 

	DECLARE CommentCursor CURSOR FOR
	SELECT Com_Id FROM T_Comments 
	OPEN CommentCursor
	FETCH NEXT FROM CommentCursor INTO @__com_id

	WHILE (@@FETCH_STATUS = 0)
	BEGIN
		-- PRINT @__com_id 
		INSERT INTO T_Comments_Paths 
		OUTPUT INSERTED.path_id, @__com_id 
		INTO @outputTable DEFAULT VALUES; 
		
		FETCH NEXT FROM CommentCursor INTO @__com_id 
	END

	CLOSE CommentCursor
	DEALLOCATE CommentCursor

	SET NOCOUNT OFF


	INSERT INTO T_Comments_Closure
	(
		 path_id 
		,ancestor 
		,descendant 
		,depth 
	)
	SELECT 
		 tOutput.path_id 
		,T_Comments.COM_Id AS ancestor 
		,T_Comments.COM_Id AS descendant 
		,0 AS depth 
	FROM T_Comments 
	LEFT JOIN @outputTable AS tOutput 
		ON tOutput.com_id = T_Comments.COM_Id 
	;

END


GO

