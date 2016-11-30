
USE TestDB


DECLARE @rdbms as varchar(200)
SET @rdbms = 'Firebird'


SELECT 
	 comment_id
	,comment
	,CASE WHEN ROW_NUMBER() OVER (ORDER BY comment_id) = 1 THEN N'' ELSE N'UNION ' END + 
	 N'SELECT ' + CAST(comment_id AS nvarchar(36)) + N' AS id, ''' + REPLACE(comment, N'''', N'''''') + N''' AS comment ' 
	 + 
		 CASE @rdbms 
			WHEN 'Firebird'
				THEN N'FROM RDB$DATABASE '
			ELSE N''
		END
	 AS cmd 
FROM Comments
ORDER BY comment_id 

  
SELECT 
	 ancestor
	,descendant
	,depth
	,CASE WHEN ROW_NUMBER() OVER (ORDER BY ancestor, descendant) = 1 THEN N'' ELSE N'UNION ' END + 
	 N'SELECT ' + CAST(ancestor AS nvarchar(36)) + N' AS ancestor, ' + CAST(descendant AS nvarchar(36)) + N' AS descendant, ' + CAST(depth AS nvarchar(36)) + N' AS depth ' 
		 + 
		 CASE @rdbms 
			WHEN 'Firebird'
				THEN N'FROM RDB$DATABASE '
			ELSE N''
		END 
	AS cmd
FROM TreePaths
