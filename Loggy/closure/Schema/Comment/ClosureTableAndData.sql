

CREATE TABLE dbo.Closure
(
	 Parent_Id bigint NOT NULL 
	,Child_Id bigint NOT NULL 
	,Dept int NULL 
	,CONSTRAINT PK_Closure PRIMARY KEY(Parent_Id, Child_Id) 
); 

GO



CREATE TABLE dbo.ClosureData
(
	 Id bigint IDENTITY(1,1) NOT NULL 
	,Name national character varying(20) NULL 
	,CONSTRAINT PK_Comments PRIMARY KEY(Id) 
);

GO




CREATE TABLE dbo.Comments 
( 
	 comment_id bigint NOT NULL 
	,bug_id bigint NULL 
	,author bigint NULL 
	,comment_date datetime NULL 
	,comment national character varying(max) NULL 
	,CONSTRAINT PK_Comments_1 PRIMARY KEY(comment_id) 
); 

GO




DELETE FROM Comments;
INSERT INTO Comments (comment_id, bug_id, author, comment_date, comment) VALUES (1, 0, 0, CAST(0x0000A1DA00000000 AS DateTime), N'What''s the cause of this bug ?');
INSERT INTO Comments (comment_id, bug_id, author, comment_date, comment) VALUES (2, 0, 0, CAST(0x0000A1DA00000000 AS DateTime), N'I think it''s a NULL-Pointer.');
INSERT INTO Comments (comment_id, bug_id, author, comment_date, comment) VALUES (3, 0, 0, CAST(0x0000A1DA00000000 AS DateTime), N'No, I checked for that.');
INSERT INTO Comments (comment_id, bug_id, author, comment_date, comment) VALUES (4, 0, 0, CAST(0x0000A1DA00000000 AS DateTime), N'We need to check for valid input.');
INSERT INTO Comments (comment_id, bug_id, author, comment_date, comment) VALUES (5, 0, 0, CAST(0x0000A1DA00000000 AS DateTime), N'Yes, that''s a bug.');
INSERT INTO Comments (comment_id, bug_id, author, comment_date, comment) VALUES (6, 0, 0, CAST(0x0000A1DA00000000 AS DateTime), N'Yes, please add a check.');
INSERT INTO Comments (comment_id, bug_id, author, comment_date, comment) VALUES (7, 0, 0, CAST(0x0000A1DA00000000 AS DateTime), N'That fixed it.');
INSERT INTO Comments (comment_id, bug_id, author, comment_date, comment) VALUES (8, 0, 0, CAST(0x0000A1DC00000000 AS DateTime), N'A');
INSERT INTO Comments (comment_id, bug_id, author, comment_date, comment) VALUES (9, 0, 0, CAST(0x0000A1DC00000000 AS DateTime), N'B');
INSERT INTO Comments (comment_id, bug_id, author, comment_date, comment) VALUES (10, 0, 0, CAST(0x0000A1DC00000000 AS DateTime), N'C');
INSERT INTO Comments (comment_id, bug_id, author, comment_date, comment) VALUES (11, 0, 0, CAST(0x0000A1DC00000000 AS DateTime), N'D');
INSERT INTO Comments (comment_id, bug_id, author, comment_date, comment) VALUES (12, 0, 0, CAST(0x0000A1DC00000000 AS DateTime), N'E');
INSERT INTO Comments (comment_id, bug_id, author, comment_date, comment) VALUES (13, 0, 0, CAST(0x0000A1DC00000000 AS DateTime), N'F');
INSERT INTO Comments (comment_id, bug_id, author, comment_date, comment) VALUES (14, 0, 0, CAST(0x0000A1E0012992BD AS DateTime), N'Electronics');
INSERT INTO Comments (comment_id, bug_id, author, comment_date, comment) VALUES (15, 0, 0, CAST(0x0000A1E00129A0F9 AS DateTime), N'Televisions');
INSERT INTO Comments (comment_id, bug_id, author, comment_date, comment) VALUES (16, 0, 0, CAST(0x0000A1E00129AF1A AS DateTime), N'Portable Electronics');
INSERT INTO Comments (comment_id, bug_id, author, comment_date, comment) VALUES (17, 0, 0, CAST(0x0000A1E00129BD1B AS DateTime), N'CRT');
INSERT INTO Comments (comment_id, bug_id, author, comment_date, comment) VALUES (18, 0, 0, CAST(0x0000A1E00129C361 AS DateTime), N'LCD');
INSERT INTO Comments (comment_id, bug_id, author, comment_date, comment) VALUES (19, 0, 0, CAST(0x0000A1E00129C9AF AS DateTime), N'Plasma');
INSERT INTO Comments (comment_id, bug_id, author, comment_date, comment) VALUES (20, 0, 0, CAST(0x0000A1E00129D316 AS DateTime), N'MP3');
INSERT INTO Comments (comment_id, bug_id, author, comment_date, comment) VALUES (21, 0, 0, CAST(0x0000A1E00129D7D1 AS DateTime), N'CD');
INSERT INTO Comments (comment_id, bug_id, author, comment_date, comment) VALUES (22, 0, 0, CAST(0x0000A1E00129DDC2 AS DateTime), N'Radio');
INSERT INTO Comments (comment_id, bug_id, author, comment_date, comment) VALUES (23, 0, 0, CAST(0x0000A1E00129EC25 AS DateTime), N'Flash');

GO
DELETE FROM ClosureData;
GO 

SET IDENTITY_INSERT ClosureData ON
INSERT INTO ClosureData (Id, Name) VALUES (1, N'A');
INSERT INTO ClosureData (Id, Name) VALUES (2, N'B');
INSERT INTO ClosureData (Id, Name) VALUES (3, N'C');
SET IDENTITY_INSERT ClosureData OFF

GO
DELETE FROM Closure;
INSERT INTO Closure (Parent_Id, Child_Id, Dept) VALUES (1, 1, 0);
INSERT INTO Closure (Parent_Id, Child_Id, Dept) VALUES (1, 2, 1);
INSERT INTO Closure (Parent_Id, Child_Id, Dept) VALUES (1, 3, 2);
INSERT INTO Closure (Parent_Id, Child_Id, Dept) VALUES (2, 2, 0);
INSERT INTO Closure (Parent_Id, Child_Id, Dept) VALUES (2, 3, 1);
INSERT INTO Closure (Parent_Id, Child_Id, Dept) VALUES (3, 3, 0);
INSERT INTO Closure (Parent_Id, Child_Id, Dept) VALUES (14, 14, 0);
INSERT INTO Closure (Parent_Id, Child_Id, Dept) VALUES (15, 15, 0);
INSERT INTO Closure (Parent_Id, Child_Id, Dept) VALUES (16, 16, 0);
INSERT INTO Closure (Parent_Id, Child_Id, Dept) VALUES (17, 17, 0);
INSERT INTO Closure (Parent_Id, Child_Id, Dept) VALUES (18, 18, 0);
INSERT INTO Closure (Parent_Id, Child_Id, Dept) VALUES (19, 19, 0);
INSERT INTO Closure (Parent_Id, Child_Id, Dept) VALUES (20, 20, 0);
INSERT INTO Closure (Parent_Id, Child_Id, Dept) VALUES (21, 21, 0);
INSERT INTO Closure (Parent_Id, Child_Id, Dept) VALUES (22, 22, 0);
INSERT INTO Closure (Parent_Id, Child_Id, Dept) VALUES (23, 23, 0);
