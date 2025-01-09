CREATE PROCEDURE PROC_IF_PROJECT_WRITE
	@SoftwareID VARCHAR(500) = '',
	@InstallTypeID VARCHAR(500) = '',
	@LocationID INT = '',
	@UID VARCHAR(500) = '',
	@Content VARCHAR(MAX) = '',
	@Version VARCHAR(500) = '',
	@UUID VARCHAR(MAX) = '',
	@FileName VARCHAR(MAX) = ''
AS
BEGIN
	
	-- CustomerTypeID 가져오기
	DECLARE @CustomerTypeID INT 

	SELECT @CustomerTypeID = CustomerTypeID FROM Location WHERE LocationID = @LocationID

	-- Software 신규 등록
	IF NOT EXISTS (SELECT SoftwareID FROM Software WHERE CONVERT(VARCHAR(100), SoftwareID) = @SoftwareID)
	BEGIN

		INSERT INTO Software (SoftwareName) VALUES (@SoftwareID)

		SET @SoftwareID = SCOPE_IDENTITY()

	END

	-- InstallType 신규등록
	IF NOT EXISTS (SELECT InstallTypeID FROM InstallType WHERE CONVERT(VARCHAR(100), InstallTypeID) = @InstallTypeID)
	BEGIN

		INSERT INTO InstallType (InstallTypeName) VALUES (@InstallTypeID)

		SET @InstallTypeID = SCOPE_IDENTITY()

	END

	-- 개발 이력 저장
	INSERT INTO Project (
		CustomerTypeID,
		LocationID,
		SoftwareID,
		InstallTypeID,
		ProjectManagerID,
		Content,
		Version,
		UUID,
		FileName,
		InstallDate
	)
	VALUES
	(
		@CustomerTypeID,
		@LocationID,
		@SoftwareID,
		@InstallTypeID,
		@UID,
		@Content,
		@Version,
		@UUID,
		@FileName,
		GETDATE()
	)

	RETURN 1

END