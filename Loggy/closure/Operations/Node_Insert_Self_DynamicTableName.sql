
DECLARE @__in_closuretable nvarchar(300) 
DECLARE @__self bigint

SET @__in_closuretable = N'TreePaths'
SET @__self = 3


DECLARE @__strSelf nvarchar(36) 
SET @__strSelf = CAST(@__self AS nvarchar(36))

DECLARE @__strSQL nvarchar(4000) 


SET @__strSQL = N'
INSERT INTO ' + @__in_closuretable + '
(
	 Parent_Id 
	,Child_Id 
	,Dept 
)
VALUES
(
	 ' + @__strSelf + ' -- <Parent_Id, bigint,>
	,' + @__strSelf + ' -- <Child_Id, bigint,>
	,0 -- <Dept, int,>
);

';

PRINT @__strSQL


