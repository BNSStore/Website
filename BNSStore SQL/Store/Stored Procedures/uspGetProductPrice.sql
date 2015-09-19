CREATE PROCEDURE [Store].[uspGetProductPrice]
	@ProductID smallint = NULL,
	@ProductPrice smallmoney = NULL OUTPUT
AS
BEGIN

	SET NOCOUNT ON

	SET @ProductPrice = (SELECT ProductPrice FROM Store.Product WHERE ProductID = @ProductID)

END

