CREATE PROCEDURE [dbo].[PROC_USERCONFIG_REG]
	@UserName VARCHAR(500) = '',
	@UID VARCHAR(500) = '',
	@Password VARCHAR(MAX) = '',
	@Email VARCHAR(500) = '',
	@AuthorityLevel INT = 1
AS
BEGIN

	DECLARE @Rtn INT = 0

	BEGIN TRY

		IF NOT EXISTS ( SELECT UID FROM Users WHERE UID = @UID ) 
		BEGIN

			INSERT INTO Users
			(
				UID,
				UserName,
				Password,
				Email,
				AuthorityLevel,
				InsertDate
			)
			VALUES
			(
				@UID,
				@UserName,
				@Password,
				@Email,
				@AuthorityLevel,
				GETDATE()
			)

			SET @Rtn = 1

		END
		ELSE BEGIN

			SET @Rtn = 0

		END
		
	END TRY
	BEGIN CATCH
	
		-- CATCH

	END CATCH

	RETURN @Rtn;

END