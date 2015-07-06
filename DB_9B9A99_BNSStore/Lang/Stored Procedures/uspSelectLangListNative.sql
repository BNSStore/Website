-- =============================================
-- Author:		Cosh_
-- Create date: 2014.11.6
-- Description:	Add Translation
-- =============================================
CREATE PROCEDURE [Lang].[uspSelectLangListNative]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here

	DECLARE @LangNameTable TABLE(LangName varchar(100));
	INSERT INTO @LangNameTable EXEC Lang.uspGetLangName;

	DECLARE @LangName varchar(100);

	DECLARE @LangNameTableNative TABLE(LangName varchar(100), LangNameNative nvarchar(MAX));
	DECLARE @LangNameNative nvarchar(MAX);

	DECLARE cur CURSOR 
	LOCAL SCROLL STATIC
	FOR SELECT LangName FROM @LangNameTable
	OPEN cur;
	FETCH NEXT FROM cur INTO @LangName;

	WHILE @@FETCH_STATUS = 0 BEGIN
		
		EXEC Lang.uspGetTranslation @LangName = @LangName, @ProviderID = 2, @Keywords = 'LangName', @Context = @LangNameNative OUTPUT
		IF @LangNameNative NOT LIKE '{NULL}'
		BEGIN
			INSERT INTO @LangNameTableNative (LangName, LangNameNative)VALUES (@LangName, @LangNameNative)
		END
		SET @LangNameNative = NULL;
		FETCH NEXT FROM cur INTO @LangName;
	END

	CLOSE cur;    
	DEALLOCATE cur;

	SELECT LangName AS LangName, LangNameNative AS LangNameNative FROM @LangNameTableNative;
	
END

GO
GRANT EXECUTE
    ON OBJECT::[Lang].[uspSelectLangListNative] TO [db_executor]
    AS [dbo];

