﻿CREATE PROCEDURE PROC_BOARD_DELETE
	@BID INT = NULL,
	@UpdateIP VARCHAR(100) = NULL,
	@UpdateUID VARCHAR(100) = NULL
AS
BEGIN
	
	UPDATE Board SET 
		Status = 1, 
		UpdateUID = @UpdateUID,
		UpdateIP = @UpdateIP,
		UpdateDate = GETDATE()
	WHERE BID = @BID

	RETURN 1;

END