CREATE PROCEDURE [dbo].[PROC_BOARD_LIST]
	@CID INT = '',
	@Title VARCHAR(500) = '',
	@UserName VARCHAR(500) = ''
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
	WHERE 
		( @CID = '' OR @CID IS NULL OR c.CID = @CID )
		AND ( @Title = '' OR @Title IS NULL OR Title LIKE '%' + @Title + '%' )
		AND ( @UserName = '' OR @UserName IS NULL OR UserName LIKE '%' + @UserName + '%' )
		AND b.Status = 0
	ORDER BY BID DESC
END