
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.T_Comments_Closure') AND type in (N'U'))
DROP TABLE dbo.T_Comments_Closure
GO


IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.T_Comments_Paths') AND type in (N'U'))
DROP TABLE dbo.T_Comments_Paths
GO


IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.T_Comments') AND type in (N'U'))
DROP TABLE dbo.T_Comments
GO




CREATE TABLE dbo.T_Comments
(
	 COM_Id bigint IDENTITY(1,1) NOT NULL
	,COM_Text national character varying(255) NULL
	,CONSTRAINT PK_T_Comments PRIMARY KEY(COM_Id)
);

GO



CREATE TABLE dbo.T_Comments_Paths
(
	 path_id bigint IDENTITY(1,1) NOT NULL
	,path_com_id bigint NOT NULL CONSTRAINT FK_T_Comments_Paths_T_Comments FOREIGN KEY(path_com_id) REFERENCES dbo.T_Comments(COM_Id) ON DELETE CASCADE 
	,CONSTRAINT PK_T_Comments_Paths PRIMARY KEY(path_id) 
);

GO



CREATE TABLE dbo.T_Comments_Closure
( 
	 path_id bigint NOT NULL CONSTRAINT FK_T_Comments_Closure_T_Comments_Paths FOREIGN KEY(path_id) REFERENCES dbo.T_Comments_Paths(path_id) ON DELETE CASCADE 
	,ancestor bigint NOT NULL
	,descendant bigint NOT NULL
	,depth int NULL
	,CONSTRAINT PK_T_Comments_Closure PRIMARY KEY(path_id, ancestor, descendant)
); 

GO

