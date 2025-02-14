CREATE PROCEDURE [dbo].[PROC_IF_PROJECT_LIST]
	@LocationID INT = ''
AS
BEGIN
	
	-- Table 0
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
	WHERE l.LocationID = @LocationID AND p.[Status] = 0
	ORDER BY InstallDate DESC

	-- Table 1
	SELECT 
		ContactID,
		ContactName, 
		Grade,
		Company,
		Tel,
		Email
	FROM Contact c
	WHERE c.ContactID IN (
		SELECT VALUE FROM dbo.fnSplit((SELECT ContactIDs FROM Location WHERE LocationID = @LocationID), '|') 
	)

END