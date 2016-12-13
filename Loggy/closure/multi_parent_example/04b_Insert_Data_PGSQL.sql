

DELETE FROM T_Comments_Closure; 
DELETE FROM T_Comments_Paths; 
DELETE FROM T_Comments;

-- ALTER SEQUENCE <tablename>_<id>_seq RESTART WITH 1
-- ALTER SEQUENCE T_Comments_Paths_path_id_seq RESTART WITH 1
-- ALTER SEQUENCE T_Comments_com_id_seq RESTART WITH 1

SELECT setval('T_Comments_Paths_path_id_seq', (SELECT COALESCE(MAX(path_id), 1) FROM T_Comments_Paths) ); 
SELECT setval('T_Comments_com_id_seq', (SELECT COALESCE(MAX(com_id), 1) FROM T_Comments) ); 





INSERT INTO T_Comments(COM_Id, COM_Text) VALUES (1, N'Alle');
INSERT INTO T_Comments(COM_Id, COM_Text) VALUES (2, N'Kunde A');
INSERT INTO T_Comments(COM_Id, COM_Text) VALUES (3, N'Admin');
INSERT INTO T_Comments(COM_Id, COM_Text) VALUES (4, N'Normal');
INSERT INTO T_Comments(COM_Id, COM_Text) VALUES (5, N'Stao 1&2');
INSERT INTO T_Comments(COM_Id, COM_Text) VALUES (6, N'Stao 1');
INSERT INTO T_Comments(COM_Id, COM_Text) VALUES (7, N'Stao 2');
INSERT INTO T_Comments(COM_Id, COM_Text) VALUES (8, N'Stao 3');
INSERT INTO T_Comments(COM_Id, COM_Text) VALUES (9, N'Stao 4');
INSERT INTO T_Comments(COM_Id, COM_Text) VALUES (10, N'Stao 1&3');







-- EXEC sp_DATA_SelfInsertComment;
INSERT INTO T_Comments_Paths (path_com_id)
SELECT COM_Id FROM T_Comments; 


INSERT INTO T_Comments_Closure
(
	 path_id
	,ancestor 
	,descendant 
	,depth 
)
SELECT 
	 path_id -- path_id
	,path_com_id -- ancestor, bigint 
	,path_com_id -- descendant, bigint 
	,0 -- depth, int 
FROM T_Comments_Paths  
; 




SELECT sp_DATA_InsertComment(1, 2); 
SELECT sp_DATA_InsertComment(2, 3); 
SELECT sp_DATA_InsertComment(2, 4); 
SELECT sp_DATA_InsertComment(3, 5); 
SELECT sp_DATA_InsertComment(3, 6); 
SELECT sp_DATA_InsertComment(3, 7); 
SELECT sp_DATA_InsertComment(3, 8); 
SELECT sp_DATA_InsertComment(3, 9); 
SELECT sp_DATA_InsertComment(3, 10); 
SELECT sp_DATA_InsertComment(5, 6); 
SELECT sp_DATA_InsertComment(5, 7);

SELECT sp_DATA_InsertComment(10, 6); 
SELECT sp_DATA_InsertComment(10, 8); 



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
