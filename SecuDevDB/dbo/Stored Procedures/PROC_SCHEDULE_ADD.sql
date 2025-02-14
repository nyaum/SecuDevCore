CREATE PROCEDURE PROC_SCHEDULE_ADD
	@StartDate VARCHAR(100) = '',
	@EndDate VARCHAR(100) = '',
	@ScheduleName VARCHAR(500) = ''
AS
BEGIN
	
	DECLARE @Rtn INT = -1

	BEGIN TRY
	
		INSERT INTO Schedule (
			ScheduleName,
			StartDate,
			EndDate
		)
		VALUES
		(
			@ScheduleName,
			@StartDate,
			@EndDate
		)

		SET @Rtn = 1

	END TRY
	BEGIN CATCH
		
		
		RETURN @Rtn

	END CATCH

	RETURN @Rtn

END