CREATE PROCEDURE PROC_CONFIG_CATEGORY
	@Type VARCHAR(100) = 'I',
	@CID INT = '',
	@CategoryName VARCHAR(100) = ''
AS
BEGIN
	
	IF (@Type = 'I')
	BEGIN
		INSERT INTO Category
		(
			CategoryName,
			InsertDate
		)
		VALUES
		(
			@CategoryName,
			GETDATE()
		)

		RETURN 1	
	END

	IF (@Type = 'E')
	BEGIN

		UPDATE Category SET
			CategoryName = @CategoryName,
			UpdateDate = GETDATE()
		WHERE CID = @CID

		RETURN 1
	END
END