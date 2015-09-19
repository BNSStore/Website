CREATE PROCEDURE [Store].[uspGetProductOnSalePrice]
	@ProductID smallint = NULL,
	@OnSalePrice smallmoney = NULL OUTPUT
AS
BEGIN

	SET NOCOUNT ON

	SET @OnSalePrice = (SELECT OnSalePrice FROM Store.Product WHERE ProductID = @ProductID)

END

