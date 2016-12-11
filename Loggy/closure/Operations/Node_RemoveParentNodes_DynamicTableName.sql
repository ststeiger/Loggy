
DECLARE @__strSQL nvarchar(4000) 
DECLARE @__in_closuretable nvarchar(300) 
DECLARE @__nodeid AS bigint


SET @__in_closuretable = N'TreePaths'
SET @__nodeid = 6

-- SELECT * 
SET @__strSQL = N'

DECLARE @__nodeid AS bigint
SET @__nodeid = ' + CAST(@__nodeid AS nvarchar(20)) + N'


--DELETE a 
SELECT a.* 
FROM ' + @__in_closuretable + ' AS a 

INNER JOIN ' + @__in_closuretable + ' AS d 
	ON a.descendant = d.descendant 

LEFT JOIN ' + @__in_closuretable + ' AS x 
	ON x.ancestor = d.ancestor 
	AND x.descendant = a.ancestor 

WHERE d.ancestor = @__nodeid 
AND x.ancestor IS NULL 
';
PRINT @__strSQL; 
