CREATE PROCEDURE PROC_NOTE_UPDATE
	@LocationID INT = '',
	@Note VARCHAR(500) = ''
AS
BEGIN

	UPDATE Location SET Note = @Note WHERE LocationID = @LocationID

	RETURN 1

END