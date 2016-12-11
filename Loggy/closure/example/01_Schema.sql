

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.T_Comments') AND type in (N'U'))
BEGIN
CREATE TABLE dbo.T_Comments
(
	COM_Id int IDENTITY(1,1) NOT NULL,
	COM_Text nvarchar(255) NULL,
	CONSTRAINT PK_T_Comments PRIMARY KEY(COM_Id) 
); 
END
GO



IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.T_Comments_Closure') AND type in (N'U'))
BEGIN
CREATE TABLE dbo.T_Comments_Closure
(
	 ancestor bigint NOT NULL
	,descendant bigint NOT NULL
	,depth int NULL
	,CONSTRAINT PK_T_Comments_Closure PRIMARY KEY(ancestor, descendant)
);
END
GO


