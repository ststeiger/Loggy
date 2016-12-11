
DECLARE @__strSQL nvarchar(4000) 
DECLARE @__in_closuretable nvarchar(300) 


SET @__in_closuretable = N'TreePaths'

-- SELECT * 
SET @__strSQL = N'
DELETE 
FROM ' + @__in_closuretable + '
WHERE ancestor IN 
(
	SELECT descendant FROM ' + @__in_closuretable + ' WHERE ancestor = 1 
)
';
PRINT @__strSQL; 
