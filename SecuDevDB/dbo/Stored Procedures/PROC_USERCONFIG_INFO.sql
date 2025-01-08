CREATE PROCEDURE PROC_USERCONFIG_INFO
	@UID VARCHAR(500) = ''
AS
BEGIN

	SELECT 
		UID, UserName, Email, u.AuthorityLevel, a.AuthorityName
	FROM Users u
	INNER JOIN Authority a ON u.AuthorityLevel = a.AuthorityLevel
	WHERE UID = @UID

END