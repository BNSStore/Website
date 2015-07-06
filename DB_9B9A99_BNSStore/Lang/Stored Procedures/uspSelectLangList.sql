-- =============================================
-- Author:		Cosh_
-- Create date: 2014.11.6
-- Description:	Add Translation
-- =============================================
CREATE PROCEDURE [Lang].[uspSelectLangList]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here

	SELECT LangName FROM Lang.LangList;

END

GO
GRANT EXECUTE
    ON OBJECT::[Lang].[uspSelectLangList] TO [db_executor]
    AS [dbo];

