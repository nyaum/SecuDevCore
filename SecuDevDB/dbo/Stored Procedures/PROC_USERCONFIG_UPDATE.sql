CREATE PROCEDURE [dbo].[PROC_USERCONFIG_UPDATE]
	@UserName VARCHAR(500) = '',
	@UID VARCHAR(500) = '',
	@Password VARCHAR(MAX) = '',
	@Email VARCHAR(500) = '',
	@AuthorityLevel INT = 1
AS
BEGIN

	DECLARE @Rtn INT = 0
	
	BEGIN TRY

		UPDATE Users SET
			UserName = @UserName,
			Password = IIF(@Password = '', Password, @Password),
			Email = @Email,
			AuthorityLevel = @AuthorityLevel
		WHERE UID = @UID

		SET @Rtn = 1

	END TRY
	BEGIN CATCH
		
		-- CATCH

	END CATCH

	RETURN @Rtn;

END