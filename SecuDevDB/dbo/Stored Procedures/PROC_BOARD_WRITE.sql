CREATE PROCEDURE [dbo].[PROC_BOARD_WRITE]

	@CID INT = NULL,
	@UID VARCHAR(100) = NULL,
	@Title VARCHAR(200) = NULL,
	@Content VARCHAR(MAX) = NULL,
	@FileName VARCHAR(MAX) = NULL,
	@IPAddress VARCHAR(100) = NULL,
	@UUID VARCHAR(MAX) = NULL,
	@Extension VARCHAR(100) = NULL,
	@Type VARCHAR(100) = NULL
AS
BEGIN
	
	BEGIN TRY

		INSERT INTO Board 
		(
			[CID], [UID],
			[Title], [Content],
			[UUID],[FileName],
			[InsertDate], [InsertIP]
		) VALUES (
			@CID, @UID,
			@Title, @Content,
			@UUID, @FileName,
			GETDATE(), @IPAddress
		)

		RETURN SCOPE_IDENTITY()

	END TRY 
	BEGIN CATCH

		RETURN -1
		 
	END CATCH
END