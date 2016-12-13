
CREATE TABLE IF NOT EXISTS T_Comments
(
	 COM_Id bigserial NOT NULL
	,COM_Text national character varying(255) NULL
	,CONSTRAINT PK_T_Comments PRIMARY KEY(COM_Id)
);




CREATE TABLE IF NOT EXISTS T_Comments_Paths
(
	 path_id bigserial NOT NULL
	,path_com_id bigint NOT NULL CONSTRAINT FK_T_Comments_Paths_T_Comments REFERENCES T_Comments(COM_Id) ON DELETE CASCADE 
	,CONSTRAINT PK_T_Comments_Paths PRIMARY KEY(path_id) 
);




CREATE TABLE IF NOT EXISTS T_Comments_Closure
( 
	 path_id bigint NOT NULL CONSTRAINT FK_T_Comments_Closure_T_Comments_Paths REFERENCES T_Comments_Paths(path_id) ON DELETE CASCADE 
	,ancestor bigint NOT NULL
	,descendant bigint NOT NULL
	,depth int NULL
	,CONSTRAINT PK_T_Comments_Closure PRIMARY KEY(path_id, ancestor, descendant)
); 
