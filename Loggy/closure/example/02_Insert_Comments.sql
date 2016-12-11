

SET IDENTITY_INSERT dbo.T_Comments ON 


INSERT INTO T_Comments(COM_Id, COM_Text)
      SELECT 1 as id, 'What''s the cause of this bug ?' as comment 
UNION SELECT 2 as id, 'I think it''s a NULL-Pointer.' as comment 
UNION SELECT 3 as id, 'No, I checked for that.' as comment 
UNION SELECT 4 as id, 'We need to check for valid input.' as comment 
UNION SELECT 5 as id, 'Yes, that''s a bug.' as comment 
UNION SELECT 6 as id, 'Yes, please add a check.' as comment 
UNION SELECT 7 as id, 'That fixed it.' as comment 
UNION SELECT 8 as id, 'A' as comment 
UNION SELECT 9 as id, 'B' as comment 
UNION SELECT 10 as id, 'C' as comment 
UNION SELECT 11 as id, 'D' as comment 
UNION SELECT 12 as id, 'E' as comment 
UNION SELECT 13 as id, 'F' as comment 
UNION SELECT 14 as id, 'Electronics' as comment 
UNION SELECT 15 as id, 'Televisions' as comment 
UNION SELECT 16 as id, 'Portable Electronics' as comment 
UNION SELECT 17 as id, 'CRT' as comment 
UNION SELECT 18 as id, 'LCD' as comment 
UNION SELECT 19 as id, 'Plasma' as comment 
UNION SELECT 20 as id, 'MP3' as comment 
UNION SELECT 21 as id, 'CD' as comment 
UNION SELECT 22 as id, 'Radio' as comment 
UNION SELECT 23 as id, 'Flash' as comment 


SET IDENTITY_INSERT dbo.T_Comments OFF
GO
