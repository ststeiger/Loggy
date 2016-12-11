
DECLARE @__strSQL nvarchar(4000) 
DECLARE @__in_closuretable nvarchar(300) 
SET @__in_closuretable = N'T_CommentClosure'


DECLARE @__minval integer 
DECLARE @__maxval integer 
SET @__minval = 1
SET @__maxval = 7


SET @__strSQL = N'

DECLARE @__minval integer 
DECLARE @__maxval integer 
DECLARE @__counter integer 


SET @__minval = ' + CAST(@__minval AS nvarchar(20))+ '
SET @__maxval = ' + CAST(@__maxval AS nvarchar(20)) + '
SET @__counter = @__minval



WHILE(@__counter < @__maxval + 1)
BEGIN
	DECLARE @__self bigint
	SET @__self = @__counter 

	PRINT @__counter


	INSERT INTO ' + @__in_closuretable + '
	(
		 ancestor 
		,descendant 
		,depth 
	)
	VALUES
	(
		 @__self --<ancestor, bigint,>
		,@__self --<descendant, bigint,>
		,0 --<depth, int,>
	)
	;
	
SET @__counter = @__counter + 1;
END
'
PRINT @__strSQL 
