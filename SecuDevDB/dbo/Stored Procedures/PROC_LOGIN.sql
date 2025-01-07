CREATE PROCEDURE [dbo].[PROC_LOGIN]
	@UID VARCHAR(100) = NULL,
	@Password VARCHAR(MAX) = NULL
AS
BEGIN

	SELECT 
		[UID], UserName, AuthorityLevel, Status,
		IIF(Status = 1, 'DELETE', 'OK' ) AS Result
	FROM Users
	WHERE UID = @UID 
	AND [Password] = @Password

	UPDATE Users SET
		LastLogin = GETDATE()
	WHERE UID = @UID AND [Password] = @Password

END