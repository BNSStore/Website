CREATE PROCEDURE [Lang].[uspGetLangName]
	@LangID tinyint = NULL,
	@LangName varchar(100) = NULL OUTPUT
AS
BEGIN
	SET NOCOUNT ON;
	SET @LangName = (SELECT TOP 1 LangName FROM Lang.LangList WHERE LangID = @LangID)
END