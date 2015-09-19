CREATE PROCEDURE [Lang].uspGetTranslation
	@LangID tinyint = NULL,
	@LangName varchar(100) = NULL,
	@Keyword varchar(100) = NULL,
	@Failback bit = 1,
	@Context nvarchar(MAX) = NULL OUTPUT
AS
BEGIN
	SET NOCOUNT ON;

	IF @LangID IS NULL
	BEGIN
		EXEC Lang.uspGetLangID @LangName = @LangName, @LangID = @LangID OUTPUT
	END

	SET @Context = (SELECT Context FROM Lang.Translation WHERE [LangID] = @LangID AND Keyword = @Keyword);
	IF @Context IS NULL
	BEGIN
		IF @LangName IS NULL
		BEGIN
			EXEC Lang.uspGetLangName @LangID = @LangID, @LangName = @LangName OUTPUT
		END
		IF @LangName NOT LIKE 'English' AND @Failback = 1
		BEGIN
			EXEC Lang.uspGetTranslation @LangName = 'English', @Keyword = @Keyword, @Context = @Context OUTPUT
		END
		ELSE
		BEGIN
			SET @Context = '{Missing Context}'
		END
	END
END