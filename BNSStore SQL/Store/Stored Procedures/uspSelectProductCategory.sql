CREATE PROCEDURE [Store].[uspSelectProductCategories]
AS
BEGIN

	SET NOCOUNT ON

	SELECT * FROM Store.ProductCategory ORDER BY CategoryID
END