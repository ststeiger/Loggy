USE TestDB;
DELETE FROM dbo.Comments;
INSERT dbo.Comments(comment_id, bug_id, author, comment_date, comment) VALUES (1, 0, 0, CURRENT_TIMESTAMP, N'What''s the cause of this bug ?');
INSERT dbo.Comments(comment_id, bug_id, author, comment_date, comment) VALUES (2, 0, 0, CURRENT_TIMESTAMP, N'I think it''s a NULL-Pointer.');
INSERT dbo.Comments(comment_id, bug_id, author, comment_date, comment) VALUES (3, 0, 0, CURRENT_TIMESTAMP, N'No, I checked for that.');
INSERT dbo.Comments(comment_id, bug_id, author, comment_date, comment) VALUES (4, 0, 0, CURRENT_TIMESTAMP, N'We need to check for valid input.');
INSERT dbo.Comments(comment_id, bug_id, author, comment_date, comment) VALUES (5, 0, 0, CURRENT_TIMESTAMP, N'Yes, that''s a bug.');
INSERT dbo.Comments(comment_id, bug_id, author, comment_date, comment) VALUES (6, 0, 0, CURRENT_TIMESTAMP, N'Yes, please add a check.');
INSERT dbo.Comments(comment_id, bug_id, author, comment_date, comment) VALUES (7, 0, 0, CURRENT_TIMESTAMP, N'That fixed it.');
INSERT dbo.Comments(comment_id, bug_id, author, comment_date, comment) VALUES (8, 0, 0, CURRENT_TIMESTAMP, N'A');
INSERT dbo.Comments(comment_id, bug_id, author, comment_date, comment) VALUES (9, 0, 0, CURRENT_TIMESTAMP, N'B');
INSERT dbo.Comments(comment_id, bug_id, author, comment_date, comment) VALUES (10, 0, 0, CURRENT_TIMESTAMP, N'C');
INSERT dbo.Comments(comment_id, bug_id, author, comment_date, comment) VALUES (11, 0, 0, CURRENT_TIMESTAMP, N'D');
INSERT dbo.Comments(comment_id, bug_id, author, comment_date, comment) VALUES (12, 0, 0, CURRENT_TIMESTAMP, N'E');
INSERT dbo.Comments(comment_id, bug_id, author, comment_date, comment) VALUES (13, 0, 0, CURRENT_TIMESTAMP, N'F');
INSERT dbo.Comments(comment_id, bug_id, author, comment_date, comment) VALUES (14, 0, 0, CURRENT_TIMESTAMP, N'Electronics');
INSERT dbo.Comments(comment_id, bug_id, author, comment_date, comment) VALUES (15, 0, 0, CURRENT_TIMESTAMP, N'Televisions');
INSERT dbo.Comments(comment_id, bug_id, author, comment_date, comment) VALUES (16, 0, 0, CURRENT_TIMESTAMP, N'Portable Electronics');
INSERT dbo.Comments(comment_id, bug_id, author, comment_date, comment) VALUES (17, 0, 0, CURRENT_TIMESTAMP, N'CRT');
INSERT dbo.Comments(comment_id, bug_id, author, comment_date, comment) VALUES (18, 0, 0, CURRENT_TIMESTAMP, N'LCD');
INSERT dbo.Comments(comment_id, bug_id, author, comment_date, comment) VALUES (19, 0, 0, CURRENT_TIMESTAMP, N'Plasma');
INSERT dbo.Comments(comment_id, bug_id, author, comment_date, comment) VALUES (20, 0, 0, CURRENT_TIMESTAMP, N'MP3');
INSERT dbo.Comments(comment_id, bug_id, author, comment_date, comment) VALUES (21, 0, 0, CURRENT_TIMESTAMP, N'CD');
INSERT dbo.Comments(comment_id, bug_id, author, comment_date, comment) VALUES (22, 0, 0, CURRENT_TIMESTAMP, N'Radio');
INSERT dbo.Comments(comment_id, bug_id, author, comment_date, comment) VALUES (23, 0, 0, CURRENT_TIMESTAMP, N'Flash');



