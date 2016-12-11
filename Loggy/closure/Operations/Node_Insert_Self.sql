
DECLARE @__self bigint
SET @__self = 3


INSERT INTO Closure
(
	 Parent_Id 
	,Child_Id 
	,Dept 
)
VALUES
(
	 @__self --<Parent_Id, bigint,>
	,@__self --<Child_Id, bigint,>
	,0 --<Dept, int,>
)
; 
