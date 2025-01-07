CREATE PROCEDURE [dbo].[PROC_BOARD_READ]
	@BID INT = NULL
AS
BEGIN
	SELECT 
		BID,
		b.UID, u.UserName,
		b.CID, c.CategoryName, c.BackgroundColor, c.FontColor,
		Title, Content,
		UUID, FileName,
		b.InsertDate, InsertIP 
	FROM Board b
	INNER JOIN Category c ON b.CID = c.CID
	INNER JOIN Users u ON u.UID = b.UID
	WHERE BID = @BID AND b.Status = 0
END
