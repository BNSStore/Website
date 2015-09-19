CREATE PROCEDURE [Lang].[uspSelectLangNameNative]
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @LangNameTable TABLE(LangName varchar(100));
	INSERT INTO @LangNameTable EXEC Lang.uspSelectLangName;

	DECLARE @LangNameTableNative TABLE(LangName varchar(100), LangNameNative nvarchar(MAX));

	DECLARE @Cursor CURSOR 
	DECLARE @LangName varchar(100);
	DECLARE @LangNameNative nvarchar(MAX);

	SET @Cursor = CURSOR FOR SELECT LangName FROM @LangNameTable
	OPEN @Cursor;
	FETCH NEXT FROM @Cursor INTO @LangName;

	WHILE @@FETCH_STATUS = 0 BEGIN
		
		EXEC Lang.uspGetTranslation @LangName = @LangName, @Keyword = 'LangName',@Failback = 0, @Context = @LangNameNative OUTPUT
		IF @LangNameNative NOT LIKE '{Missing Context}'
		BEGIN
			INSERT INTO @LangNameTableNative (LangName, LangNameNative)VALUES (@LangName, @LangNameNative)
		END
		SET @LangNameNative = NULL;
		FETCH NEXT FROM @Cursor INTO @LangName;
	END

	CLOSE @Cursor;    
	DEALLOCATE @Cursor;

	SELECT LangName AS LangName, LangNameNative AS LangNameNative FROM @LangNameTableNative;
	
END