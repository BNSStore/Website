CREATE PROCEDURE [Store].uspChangeProductName
    @ProductID smallint = NULL,
    @ProductName varchar(100) = NULL,
    @NewProductName varchar(100) = NULL,
    @OnSalePrice smallmoney = NULL
AS
BEGIN
	SET NOCOUNT ON

	IF @ProductID IS NULL
	BEGIN
		EXEC Store.uspGetProductID @ProductID = @ProductID OUTPUT, @ProductName = @ProductName
	END

	UPDATE Store.Product SET ProductName = @NewProductName WHERE ProductID = @ProductID
END
