
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.T_Comments') AND type in (N'U'))
DROP TABLE dbo.T_Comments
GO




CREATE TABLE dbo.T_Comments
(
	 COM_Id int IDENTITY(1,1) NOT NULL
	,COM_Text nvarchar(255) NULL
	,CONSTRAINT PK_T_Comments PRIMARY KEY(COM_Id)
);

GO




IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.T_Comments_Paths') AND type in (N'U'))
DROP TABLE dbo.T_Comments_Paths
GO



CREATE TABLE dbo.T_Comments_Paths
(
	 path_id bigint IDENTITY(1,1) NOT NULL
	,CONSTRAINT PK_T_Comments_Paths PRIMARY KEY(path_id) 
);
GO






IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.T_Comments_Closure') AND type in (N'U'))
DROP TABLE dbo.T_Comments_Closure
GO



CREATE TABLE dbo.T_Comments_Closure
(
	 path_id bigint NOT NULL
	,ancestor bigint NOT NULL
	,descendant bigint NOT NULL
	,depth int NULL
	,CONSTRAINT PK_T_Comments_Closure PRIMARY KEY(path_id, ancestor, descendant) 
);


GO

