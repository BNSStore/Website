CREATE PROCEDURE [Lang].[uspGetLangCode]
	@LangID tinyint = NULL,
	@LangName varchar(100) = NULL,
	@LangCode varchar(7) = NULL OUTPUT
AS
BEGIN
	SET NOCOUNT ON;

	IF @LangID IS NULL
	BEGIN
		EXEC Lang.uspGetLangID @LangName = @LangName, @LangID = @LangID OUTPUT
	END

	SET @LangCode = (SELECT LangCode FROM Lang.LangList WHERE LangID = @LangID)
END
