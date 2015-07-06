-- =============================================
-- Author:		Cosh_
-- Create date: 2014.11.6
-- Description:	Add Translation
-- =============================================
CREATE PROCEDURE [Store].[uspSelectProductCategories]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM Store.ProductCategory ORDER BY CategoryID;
END

GO
GRANT EXECUTE
    ON OBJECT::[Store].[uspSelectProductCategories] TO [db_executor];

