CREATE FUNCTION [dbo].[fnLocationDepth]
()
RETURNS @LocationDepth TABLE (
		LocationID INT, 
		LocationName VARCHAR(500),
		ParentLocationID INT,
		CustomerTypeID INT,
		CustomerTypeName VARCHAR(500), 
		MasterLocationID INT, 
		MasterLocationName VARCHAR(500), 
		Depth VARCHAR(MAX),
		Status INT
)
AS
BEGIN
	;with CT
	AS (
		SELECT 
			LocationID,
			LocationName,
			ParentLocationID,
			MasterLocationID,
			LocationName AS MasterLocationName,
			CONVERT(VARCHAR(500), LocationName) AS Depth,
			Status
		FROM Location
		WHERE ParentLocationID IS NULL OR ParentLocationID = 0
		UNION ALL
		SELECT
			l.LocationID,
			l.LocationName,
			l.ParentLocationID,
			l.MasterLocationID,
			l.LocationName AS MasterLocationName,
			CONVERT(VARCHAR(500), CONVERT(VARCHAR, ct.Depth) + ' > ' + CONVERT(VARCHAR(500), l.LocationName)),
			l.Status
		FROM Location l, CT ct
		WHERE l.ParentLocationID = ct.LocationID
	)

	INSERT INTO @LocationDepth
	SELECT 
		c.LocationID,
		c.LocationName,
		IIF(c.ParentLocationID = 0, NULL, c.ParentLocationID ) AS ParentLocationID,
		ct.CustomerTypeID, ct.CustomerTypeName,
		ml.LocationID AS MasterLocationID, 
		ml.LocationName AS MasterLocationName, 
		Depth, c.Status
	FROM CT c 
	INNER JOIN Location ml ON c.MasterLocationID = ml.LocationID
	INNER JOIN CustomerType ct ON ct.CustomerTypeID = ml.CustomerTypeID
	ORDER BY c.MasterLocationID ASC

	RETURN
	
END