CREATE FUNCTION [dbo].[fnLocationDepth]
()
RETURNS @LocationDepth TABLE (
		LocationID INT, 
		CustomerTypeID INT,
		CustomerTypeName VARCHAR(500), 
		MasterLocationID INT, 
		MasterLocationName VARCHAR(500), 
		Depth VARCHAR(MAX)
)
AS
BEGIN
	;with CT
	AS (
		SELECT 
			LocationID,
			ParentLocationID,
			MasterLocationID,
			LocationName,
			CONVERT(VARCHAR(500), LocationName) AS Depth
		FROM Location
		WHERE ParentLocationID IS NULL
		UNION ALL
		SELECT
			l.LocationID,
			l.ParentLocationID,
			l.MasterLocationID,
			l.LocationName,
			CONVERT(VARCHAR(500), CONVERT(VARCHAR, ct.Depth) + N' > ' + CONVERT(VARCHAR(500), l.LocationName))
		FROM Location l, CT ct
		WHERE l.ParentLocationID = ct.LocationID
	)

	INSERT INTO @LocationDepth
	SELECT 
		c.LocationID,
		ct.CustomerTypeID, ct.CustomerTypeName,
		ml.LocationID AS MasterLocationID, 
		ml.LocationName AS MasterLocationName, 
		Depth 
	FROM CT c 
	INNER JOIN Location ml ON c.MasterLocationID = ml.LocationID
	INNER JOIN CustomerType ct ON ct.CustomerTypeID = ml.CustomerTypeID
	ORDER BY c.MasterLocationID ASC

	RETURN
	
END