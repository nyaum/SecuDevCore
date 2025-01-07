CREATE PROCEDURE [dbo].[PROC_LIST]
	@Type VARCHAR(50) = ''
AS
BEGIN

	-- 카테고리 리스트
	IF @Type = 'Category'
	BEGIN
		SELECT 
			CID, CategoryName, BackgroundColor, FontColor
		FROM Category
	END

	-- 유저 리스트
	-- 2024.12.26 [PROC_USERCONFIG_LIST] 로 분리
	--IF @Type = 'User'
	--BEGIN
	--	SELECT
	--		UID, UserName, u.AuthorityLevel, a.AuthorityName, InsertDate, LastLogin
	--	FROM Users u
	--	INNER JOIN Authority a ON u.AuthorityLevel = a.AuthorityLevel
	--END	
	
	-- 권한 리스트
	IF @Type = 'Authority'
	BEGIN
		SELECT
			AuthorityLevel, AuthorityName
		FROM Authority
	END

	-- 팀 리스트
	IF @Type = 'Team'
	BEGIN
		SELECT
			TeamID, TeamName
		FROM Team
	END

END