CREATE PROCEDURE [dbo].[PROC_PROJECT_LOCATION_LIST]
	@Type VARCHAR(100) = '',
	@SchText VARCHAR(500) = ''
AS
BEGIN
	
	SELECT 
		t.TeamID, TeamName, ct.CustomerTypeID, CustomerTypeName, LocationID, ParentLocationID, LocationName
	INTO #tmp
	FROM Location l 
	INNER JOIN CustomerType ct ON ct.CustomerTypeID = l.CustomerTypeID
	INNER JOIN Team t ON t.TeamID = ct.TeamID


	IF @Type = 'MasterLocationByTeam'
	BEGIN
		
		SELECT 
			LocationID, LocationName
		FROM #tmp
		WHERE ParentLocationID IS NULL 
		AND TeamID = @SchText

	END

	IF @Type = 'LocationByID'
	BEGIN
		
		SELECT 
			LocationID, LocationName
		FROM #tmp
		WHERE ParentLocationID = @SchText

	END

END