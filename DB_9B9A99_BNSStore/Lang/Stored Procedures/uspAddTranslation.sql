-- =============================================
-- Author:		Cosh_
-- Create date: 2014.11.6
-- Description:	Add Translation
-- =============================================
CREATE PROCEDURE [Lang].uspAddTranslation
	@LangID tinyint = NULL,
	@LangName varchar(100) = NULL,
	@ProviderID int = NULL,
	@ProviderName nvarchar(100) = NULL,
	@Keyword varchar(100) = NULL,
	@Context nvarchar(MAX) = NULL
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here

	IF @LangID IS NULL
	BEGIN
		EXEC Lang.uspGetLangID @LangName = @LangName, @LangID = @LangID OUTPUT
	END

	IF @ProviderID IS NULL
	BEGIN
		EXEC Svc.uspGetProviderID @ProviderName = @ProviderName, @ProviderID = @ProviderID OUTPUT
	END

	DELETE FROM Lang.Translation WHERE [LangID] = @LangID AND ProviderID = @ProviderID AND Keyword = @Keyword

	INSERT INTO Lang.Translation ([LangID], ProviderID, Keyword, Context) VALUES (@LangID, @ProviderID, @Keyword, @Context)

END

GO
GRANT EXECUTE
    ON OBJECT::[Lang].[uspAddTranslation] TO [db_executor]
    AS [dbo];

