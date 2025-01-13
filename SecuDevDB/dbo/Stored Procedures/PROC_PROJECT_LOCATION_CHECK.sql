CREATE PROCEDURE [dbo].[PROC_PROJECT_LOCATION_CHECK]
	@LocationID INT = ''
AS
BEGIN
	
	DECLARE @Rtn INT = -1

	-- 이력이 있는지 확인
	IF EXISTS(SELECT * FROM Project WHERE LocationID = @LocationID)
	BEGIN
		SET @Rtn = 1
	END

	-- 하위 조직이 있는지 확인
	IF EXISTS(SELECT * FROM Location WHERE ParentLocationID = @LocationID)
	BEGIN
		SET @Rtn = 2
	END

	RETURN @Rtn

END