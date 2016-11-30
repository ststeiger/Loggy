
USE TestDB	


DECLARE @__in_closuretable nvarchar(300) 
DECLARE @__iNodeId AS integer 
DECLARE @__iTargetId AS integer 

SET @__in_closuretable = N'TreePaths'
SET @__iNodeId =  123 
SET @__iTargetId = 456 




DECLARE @__strSQL nvarchar(4000) 
DECLARE @__strNodeId nvarchar(36) 
DECLARE @__strTargetId nvarchar(36) 

SET @__strNodeId = CAST(@__iNodeId AS nvarchar(36))
SET @__strTargetId = CAST(@__iTargetId AS nvarchar(36))

	
SET @__strSQL = N'DELETE a FROM ' + @__in_closuretable + N' AS a ';
SET @__strSQL = @__strSQL + N'INNER JOIN ' + @__in_closuretable + N' AS d ON a.descendant = d.descendant ';
SET @__strSQL = @__strSQL + N'LEFT JOIN ' + @__in_closuretable + N' AS x ';
SET @__strSQL = @__strSQL + N'ON x.ancestor = d.ancestor AND x.descendant = a.ancestor ';
SET @__strSQL = @__strSQL + N'WHERE d.ancestor = ' + @__strNodeId + N'  AND x.ancestor IS NULL';
 
PRINT @__strSQL
--DB::query(Database::DELETE, @__strSQL)->execute();
 
SET @__strSQL = N'INSERT INTO ' + @__in_closuretable + N' (ancestor, descendant, depth) ';
SET @__strSQL = @__strSQL + N'SELECT a.ancestor, b.descendant, a.depth + b.depth + 1 ';
SET @__strSQL = @__strSQL + N'FROM ' + @__in_closuretable + N' AS a ';
SET @__strSQL = @__strSQL + N'INNER JOIN ' + @__in_closuretable + N' AS b ';
SET @__strSQL = @__strSQL + N'ON b.ancestor = ' + @__strNodeId + N' '
SET @__strSQL = @__strSQL + N'AND a.descendant = ' + @__strTargetId;
PRINT @__strSQL

