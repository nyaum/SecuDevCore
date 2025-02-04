CREATE PROCEDURE [dbo].[PROC_IF_PROJECT_CONTACT_WRITE]
	@LocationID INT = '',
	@ContactName VARCHAR(500) = '',
	@Grade VARCHAR(500) = '',
	@Company VARCHAR(500) = '',
	@Tel VARCHAR(500) = '',
	@Email VARCHAR(500) = ''
AS
BEGIN
	
	DECLARE @ContactID INT = ''

	
	IF NOT EXISTS ( SELECT ContactID FROM Contact WHERE ContactName = @ContactName AND Tel = @Tel )
	BEGIN

		INSERT INTO Contact (
			ContactName,
			Grade,
			Company,
			Tel,
			Email
		) VALUES (
			@ContactName,
			@Grade,
			@Company,
			@Tel,
			@Email
		)

		SET @ContactID = SCOPE_IDENTITY()

	END
	ELSE BEGIN
		
		SELECT @ContactID = ContactID 
		FROM Contact 
		WHERE ContactName = @ContactName AND Tel = @Tel

		IF EXISTS (
			SELECT 
				VALUE 
			FROM dbo.fnSplit((SELECT ContactIDs FROM Location WHERE LocationID = @LocationID), '|')
			WHERE VALUE = @ContactID
		)
		BEGIN
			RETURN -2
		END

	END

	DECLARE @IDs VARCHAR(500) = ''

	SELECT
		@IDs = ISNULL(ContactIDs, '') + CONVERT(VARCHAR(500), @ContactID) + '|'
	FROM Location 
	WHERE LocationID = @LocationID

	UPDATE Location SET
		ContactIDs = @IDs
	WHERE LocationID = @LocationID

	RETURN @ContactID

END