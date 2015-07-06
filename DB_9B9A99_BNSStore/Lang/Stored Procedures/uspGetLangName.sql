-- =============================================
-- Author:		Cosh_
-- Create date: 2014.9.13
-- Description:	Create simple user
-- =============================================
CREATE PROCEDURE [Lang].[uspGetLangName]
	@LangID tinyint = NULL,
	@LangName varchar(100) = NULL OUTPUT

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here

	IF @LangID IS NOT NULL
	BEGIN
		SET @LangName = (SELECT TOP 1 LangName FROM Lang.LangList WHERE LangID = @LangID)
	END
	ELSE
	BEGIN
		SELECT LangName FROM Lang.LangList
	END
END

GO
GRANT EXECUTE
    ON OBJECT::[Lang].[uspGetLangName] TO [db_executor]
    AS [dbo];

