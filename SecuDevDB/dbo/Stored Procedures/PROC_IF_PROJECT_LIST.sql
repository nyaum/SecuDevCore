CREATE PROCEDURE PROC_IF_PROJECT_LIST
	@LocationID INT = ''
AS
BEGIN
	SELECT 
		ProjectID,
		LocationName,
		SoftwareName,
		InstallTypeName,
		UserName,
		InstallDate
	FROM Project p
	INNER JOIN Location l ON p.LocationID = l.LocationID
	INNER JOIN Software s ON s.SoftwareID = p.SoftwareID
	INNER JOIN InstallType it ON it.InstallTypeID = p.InstallTypeID
	INNER JOIN Users u ON u.UID = p.ProjectManagerID
	WHERE p.LocationID = @LocationID
	ORDER BY InstallDate DESC
END