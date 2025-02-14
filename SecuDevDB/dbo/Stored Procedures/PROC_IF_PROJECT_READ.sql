CREATE PROCEDURE [dbo].[PROC_IF_PROJECT_READ]
	@ProjectID INT = ''
AS
BEGIN
	SELECT 
		ProjectID,
		LocationID,
		SoftwareName,
		InstallTypeName,
		UserName,
		Content,
		Version,
		UUID,
		FileName
	FROM Project p
	INNER JOIN Software s ON p.SoftwareID = s.SoftwareID
	INNER JOIN InstallType i ON p.InstallTypeID = i.InstallTypeID
	INNER JOIN Users u ON p.ProjectManagerID = u.UID
	WHERE ProjectID = @ProjectID AND p.[Status] = 0
END