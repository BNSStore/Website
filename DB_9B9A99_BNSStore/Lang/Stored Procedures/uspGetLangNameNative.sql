-- =============================================
-- Author:		Cosh_
-- Create date: 2014.11.6
-- Description:	Add Translation
-- =============================================
CREATE PROCEDURE [Lang].[uspGetLangNameNative]
	@LangID tinyint = NULL,
	@LangName varchar(100) = NULL,
	@LangNameNative nvarchar(MAX) = NULL OUTPUT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	
    -- Insert statements for procedure here

	SET NOCOUNT ON;
	IF @LangID IS NULL
	BEGIN
		EXEC Lang.uspGetLangID @LangName = @LangName, @LangID = @LangID OUTPUT
	END

	EXEC Lang.uspGetTranslation @LangID = @LangID, @ProviderID = 2, @Keywords = 'LangName', @Context = @LangNameNative OUTPUT
END

GO
GRANT EXECUTE
    ON OBJECT::[Lang].[uspGetLangNameNative] TO [db_executor]
    AS [dbo];

