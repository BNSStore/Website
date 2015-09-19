CREATE PROCEDURE [Lang].[uspGetLangNameNative]
	@LangID tinyint = NULL,
	@LangName varchar(100) = NULL,
	@LangNameNative nvarchar(MAX) = NULL OUTPUT
AS
BEGIN
	SET NOCOUNT ON;

	IF @LangID IS NULL
	BEGIN
		EXEC Lang.uspGetLangID @LangName = @LangName, @LangID = @LangID OUTPUT
	END

	EXEC Lang.uspGetTranslation @LangID = @LangID, @Keyword = 'LangName', @Context = @LangNameNative OUTPUT
END