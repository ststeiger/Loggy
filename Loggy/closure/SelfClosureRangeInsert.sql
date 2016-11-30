
USE [TestDB]
GO



DECLARE @__minval integer 
DECLARE @__maxval integer 
DECLARE @__counter integer 


SET @__minval = 14
SET @__maxval = 23
SET @__counter = @__minval



WHILE(@__counter < @__maxval + 1)
BEGIN
	DECLARE @__self bigint
	SET @__self = @__counter 

	PRINT @__counter


	INSERT INTO TreePaths
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
