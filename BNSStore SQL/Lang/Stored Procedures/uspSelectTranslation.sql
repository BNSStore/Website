CREATE TYPE [Lang].Keywords AS TABLE
(
	Keyword varchar(100) NOT NULL
)
GO
CREATE PROCEDURE [Lang].[uspSelectTranslation]
	@LangID tinyint = NULL,
	@LangName varchar(100) = NULL,
	@Keywords [Lang].Keywords READONLY
AS
BEGIN
	SET NOCOUNT ON;

	IF @LangID IS NULL
	BEGIN
		EXEC Lang.uspGetLangID @LangName = @LangName, @LangID = @LangID OUTPUT
	END

	DECLARE @Translations TABLE(
		Keyword varchar(MAX) NOT NULL,
		Context nvarchar(MAX)
	)
	DECLARE @Cursor CURSOR
	DECLARE @Keyword varchar(100)
	SET @Cursor = CURSOR FOR SELECT Keyword FROM @Keywords
	OPEN @Cursor

	FETCH NEXT FROM @Cursor INTO @Keyword
	WHILE @@FETCH_STATUS = 0
	BEGIN
		
		DECLARE @Context nvarchar(MAX)
		EXEC Lang.uspGetTranslation @LangID = @LangID, @LangName = @LangName, @Keyword = @Keyword, @Context = @Context OUTPUT
		INSERT INTO @Translations (Keyword, Context) VALUES (@Keyword, @Context)

		FETCH NEXT FROM @Cursor INTO @Keyword
	END

	CLOSE @Cursor
	DEALLOCATE @Cursor

	SELECT Keyword, Context FROM @Translations
END
