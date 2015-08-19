CREATE PROCEDURE [Lang].[uspGetLangName]
	@LangID tinyint = NULL,
	@LangName varchar(100) = NULL OUTPUT
AS
BEGIN
	SET NOCOUNT ON;

	IF @LangID IS NOT NULL
	BEGIN
		SET @LangName = (SELECT TOP 1 LangName FROM Lang.LangList WHERE LangID = @LangID)
	END
	ELSE
	BEGIN
		SELECT LangName FROM Lang.LangList
	END
END