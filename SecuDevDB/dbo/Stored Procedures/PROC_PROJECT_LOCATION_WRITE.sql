CREATE PROCEDURE PROC_PROJECT_LOCATION_WRITE
	@LocationID INT = '',
	@ParentLocationID INT = '',
	@LocationName VARCHAR(500) = ''
AS
BEGIN
	
	DECLARE @Rtn INT = -1

	IF EXISTS (SELECT LocationID FROM Location WHERE LocationID = @LocationID)
	BEGIN

		UPDATE Location SET LocationName = @LocationName WHERE LocationID = @LocationID

		RETURN @LocationID

	END
	ELSE BEGIN
		
		INSERT INTO Location
		(
			CustomerTypeID,
			MasterLocationID,
			ParentLocationID,
			LocationName,
			InsertDate
		)
		VALUES
		(
			1, -- 추후 수정
			1, -- 추후 수정
			@ParentLocationID,
			@LocationName,
			GETDATE()
		)
		
		RETURN SCOPE_IDENTITY()

	END

END