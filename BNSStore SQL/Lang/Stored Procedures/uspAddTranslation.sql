CREATE PROCEDURE [Lang].uspAddTranslation
	@LangID tinyint = NULL,
	@LangName varchar(100) = NULL,
	@Keyword varchar(100) = NULL,
	@Context nvarchar(MAX) = NULL
AS
BEGIN
	SET NOCOUNT ON;

	IF @LangID IS NULL
	BEGIN
		EXEC Lang.uspGetLangID @LangName = @LangName, @LangID = @LangID OUTPUT
	END

	DELETE FROM Lang.Translation WHERE [LangID] = @LangID AND Keyword = @Keyword

	INSERT INTO Lang.Translation ([LangID], Keyword, Context) VALUES (@LangID, @Keyword, @Context)
END