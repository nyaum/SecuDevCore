CREATE PROCEDURE [dbo].[PROC_BOARD_UPDATE]
	@BID INT = NULL,
	@CID INT = NULL,
	@UID VARCHAR(100) = NULL,
	@Title VARCHAR(200) = NULL,
	@Content VARCHAR(MAX) = NULL,
	@FileName VARCHAR(MAX) = NULL,
	@IPAddress VARCHAR(100) = NULL,
	@UUID VARCHAR(MAX) = NULL,
	@Type VARCHAR(100) = NULL
AS
BEGIN
	UPDATE Board SET
		CID = @CID,
		Title = @Title,
		Content = @Content,
		UUID = @UUID,
		FileName = @FileName,
		UpdateIP = @IPAddress,
		UpdateDate = GETDATE()
	WHERE BID = @BID

	RETURN @BID;
END