
USE TestDB;


DELETE FROM dbo.TreePaths;

INSERT dbo.TreePaths(ancestor, descendant, depth) VALUES (1, 1, 0);
INSERT dbo.TreePaths(ancestor, descendant, depth) VALUES (1, 2, 1);
INSERT dbo.TreePaths(ancestor, descendant, depth) VALUES (1, 3, 2);
INSERT dbo.TreePaths(ancestor, descendant, depth) VALUES (1, 4, 1);
INSERT dbo.TreePaths(ancestor, descendant, depth) VALUES (1, 5, 2);
INSERT dbo.TreePaths(ancestor, descendant, depth) VALUES (1, 6, 2);
INSERT dbo.TreePaths(ancestor, descendant, depth) VALUES (1, 7, 3);
INSERT dbo.TreePaths(ancestor, descendant, depth) VALUES (2, 2, 0);
INSERT dbo.TreePaths(ancestor, descendant, depth) VALUES (2, 3, 1);
INSERT dbo.TreePaths(ancestor, descendant, depth) VALUES (3, 3, 0);
INSERT dbo.TreePaths(ancestor, descendant, depth) VALUES (4, 4, 0);
INSERT dbo.TreePaths(ancestor, descendant, depth) VALUES (4, 5, 1);
INSERT dbo.TreePaths(ancestor, descendant, depth) VALUES (4, 6, 1);
INSERT dbo.TreePaths(ancestor, descendant, depth) VALUES (4, 7, 2);
INSERT dbo.TreePaths(ancestor, descendant, depth) VALUES (5, 5, 0);
INSERT dbo.TreePaths(ancestor, descendant, depth) VALUES (6, 6, 0);
INSERT dbo.TreePaths(ancestor, descendant, depth) VALUES (6, 7, 1);
INSERT dbo.TreePaths(ancestor, descendant, depth) VALUES (7, 7, 0);
INSERT dbo.TreePaths(ancestor, descendant, depth) VALUES (8, 8, 0);
INSERT dbo.TreePaths(ancestor, descendant, depth) VALUES (8, 9, 1);
INSERT dbo.TreePaths(ancestor, descendant, depth) VALUES (8, 10, 2);
INSERT dbo.TreePaths(ancestor, descendant, depth) VALUES (8, 11, 2);
INSERT dbo.TreePaths(ancestor, descendant, depth) VALUES (8, 12, 3);
INSERT dbo.TreePaths(ancestor, descendant, depth) VALUES (8, 13, 1);
INSERT dbo.TreePaths(ancestor, descendant, depth) VALUES (9, 9, 0);
INSERT dbo.TreePaths(ancestor, descendant, depth) VALUES (9, 10, 1);
INSERT dbo.TreePaths(ancestor, descendant, depth) VALUES (10, 10, 0);
INSERT dbo.TreePaths(ancestor, descendant, depth) VALUES (11, 11, 0);
INSERT dbo.TreePaths(ancestor, descendant, depth) VALUES (11, 12, 1);
INSERT dbo.TreePaths(ancestor, descendant, depth) VALUES (12, 12, 0);
INSERT dbo.TreePaths(ancestor, descendant, depth) VALUES (13, 11, 1);
INSERT dbo.TreePaths(ancestor, descendant, depth) VALUES (13, 12, 2);
INSERT dbo.TreePaths(ancestor, descendant, depth) VALUES (13, 13, 0);
INSERT dbo.TreePaths(ancestor, descendant, depth) VALUES (14, 14, 0);
INSERT dbo.TreePaths(ancestor, descendant, depth) VALUES (14, 15, 1);
INSERT dbo.TreePaths(ancestor, descendant, depth) VALUES (14, 16, 1);
INSERT dbo.TreePaths(ancestor, descendant, depth) VALUES (14, 17, 2);
INSERT dbo.TreePaths(ancestor, descendant, depth) VALUES (14, 18, 2);
INSERT dbo.TreePaths(ancestor, descendant, depth) VALUES (14, 19, 2);
INSERT dbo.TreePaths(ancestor, descendant, depth) VALUES (14, 20, 2);
INSERT dbo.TreePaths(ancestor, descendant, depth) VALUES (14, 21, 2);
INSERT dbo.TreePaths(ancestor, descendant, depth) VALUES (14, 22, 2);
INSERT dbo.TreePaths(ancestor, descendant, depth) VALUES (14, 23, 3);
INSERT dbo.TreePaths(ancestor, descendant, depth) VALUES (15, 15, 0);
INSERT dbo.TreePaths(ancestor, descendant, depth) VALUES (15, 17, 1);
INSERT dbo.TreePaths(ancestor, descendant, depth) VALUES (15, 18, 1);
INSERT dbo.TreePaths(ancestor, descendant, depth) VALUES (15, 19, 1);
INSERT dbo.TreePaths(ancestor, descendant, depth) VALUES (16, 16, 0);
INSERT dbo.TreePaths(ancestor, descendant, depth) VALUES (16, 20, 1);
INSERT dbo.TreePaths(ancestor, descendant, depth) VALUES (16, 21, 1);
INSERT dbo.TreePaths(ancestor, descendant, depth) VALUES (16, 22, 1);
INSERT dbo.TreePaths(ancestor, descendant, depth) VALUES (16, 23, 2);
INSERT dbo.TreePaths(ancestor, descendant, depth) VALUES (17, 17, 0);
INSERT dbo.TreePaths(ancestor, descendant, depth) VALUES (18, 18, 0);
INSERT dbo.TreePaths(ancestor, descendant, depth) VALUES (19, 19, 0);
INSERT dbo.TreePaths(ancestor, descendant, depth) VALUES (20, 20, 0);
INSERT dbo.TreePaths(ancestor, descendant, depth) VALUES (20, 23, 1);
INSERT dbo.TreePaths(ancestor, descendant, depth) VALUES (21, 21, 0);
INSERT dbo.TreePaths(ancestor, descendant, depth) VALUES (22, 22, 0);
INSERT dbo.TreePaths(ancestor, descendant, depth) VALUES (23, 23, 0);


