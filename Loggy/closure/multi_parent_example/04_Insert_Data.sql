

DELETE FROM T_Comments_Closure; 
DELETE FROM T_Comments_Paths; 
DBCC CHECKIDENT ('T_Comments_Paths', RESEED, 0);


EXEC sp_DATA_SelfInsertComment;

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


SELECT 
	 T_Comments_Closure.* 
	,Ancestral.COM_Text 
	,Descending.COM_Text 
FROM T_Comments_Closure 
LEFT JOIN T_Comments AS Ancestral ON Ancestral.COM_ID = ancestor 
LEFT JOIN T_Comments AS Descending ON Descending.COM_ID = descendant  
--WHERE path_id = 1105
ORDER BY path_id, depth DESC, ancestor, descendant 
;