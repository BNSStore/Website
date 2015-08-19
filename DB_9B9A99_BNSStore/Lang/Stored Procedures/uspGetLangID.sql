CREATE PROCEDURE [Lang].[uspGetLangID]
	@LangID tinyint = NULL OUTPUT,
	@LangName varchar(100) = NULL
AS
BEGIN
	SET NOCOUNT ON;

	IF @LangName IS NOT NULL
	BEGIN
		SET @LangID = (SELECT [LangID] FROM Lang.LangList WHERE LangName = @LangName)
	END
END