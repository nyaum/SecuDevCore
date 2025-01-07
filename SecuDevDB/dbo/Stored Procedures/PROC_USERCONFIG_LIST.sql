CREATE PROCEDURE [dbo].[PROC_USERCONFIG_LIST]
AS
BEGIN
	SELECT
		UID, UserName, u.AuthorityLevel, a.AuthorityName, InsertDate, LastLogin, Status
	FROM Users u
	INNER JOIN Authority a ON a.AuthorityLevel = u.AuthorityLevel
	ORDER BY Status, InsertDate ASC
END