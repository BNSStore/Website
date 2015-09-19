CREATE PROCEDURE [Store].uspChangeProductPrice
    @ProductID smallint = NULL,
    @ProductName varchar(100) = NULL,
    @ProductPrice smallmoney = NULL
AS
BEGIN

	SET NOCOUNT ON

	IF @ProductID IS NULL
	BEGIN
		EXEC Store.uspGetProductID @ProductID = @ProductID OUTPUT, @ProductName = @ProductName
	END

	UPDATE Store.Product SET ProductPrice = @ProductPrice WHERE ProductID = @ProductID
END
