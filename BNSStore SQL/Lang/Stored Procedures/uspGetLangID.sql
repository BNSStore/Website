CREATE PROCEDURE [Lang].[uspGetLangID]
	@LangID tinyint = NULL OUTPUT,
	@LangName varchar(100) = NULL
AS
BEGIN
	SET NOCOUNT ON;
	SET @LangID = (SELECT [LangID] FROM Lang.LangList WHERE LangName = @LangName)
END