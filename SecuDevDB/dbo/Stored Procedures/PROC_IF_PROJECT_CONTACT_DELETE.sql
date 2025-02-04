CREATE PROCEDURE PROC_IF_PROJECT_CONTACT_DELETE
	@LocationID INT = '',
	@ContactID INT = ''
AS
BEGIN

	DECLARE @IDs VARCHAR(500) = ''


	UPDATE Location SET
	ContactIDs = (
		SELECT 
			VALUE + '|'
		FROM dbo.fnSplit((SELECT ContactIDs FROM Location WHERE LocationID = @LocationID), '|')
		WHERE VALUE <> @ContactID
		FOR XML PATH('')
	)
	WHERE LocationID = @LocationID

	RETURN @LocationID

END