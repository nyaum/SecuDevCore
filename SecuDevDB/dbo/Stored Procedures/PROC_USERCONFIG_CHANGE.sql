CREATE PROCEDURE [dbo].[PROC_USERCONFIG_CHANGE]
	@Type VARCHAR(100) = '',
	@UID VARCHAR(500) = ''
AS
BEGIN
	
	DECLARE @Rtn INT = -1

	-- 계정 삭제
	IF (@Type = 'D')
	BEGIN

		IF EXISTS (SELECT UID FROM Users WHERE UID = @UID AND Status = 0)
		BEGIN
	
			UPDATE Users SET Status = 1 WHERE UID = @UID AND Status = 0

			SET @Rtn = 1

		END
		ELSE BEGIN

			SET @Rtn = 0

		END

	END

	-- 계정 복구
	ELSE IF (@Type = 'A')
	BEGIN

		IF EXISTS (SELECT UID FROM Users WHERE UID = @UID AND Status = 1)
		BEGIN
	
			UPDATE Users SET Status = 0 WHERE UID = @UID AND Status = 1

			SET @Rtn = 1

		END
		ELSE BEGIN

			SET @Rtn = 0

		END

	END

	RETURN @Rtn;
END