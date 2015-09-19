CREATE PROCEDURE [Store].uspChangeProductEmployeePrice
    @ProductID smallint = NULL,
    @ProductName varchar(100) = NULL,
    @EmployeePrice smallmoney = NULL
AS
BEGIN

	SET NOCOUNT ON

	IF @ProductID IS NULL
	BEGIN
		EXEC Store.uspGetProductID @ProductID = @ProductID OUTPUT, @ProductName = @ProductName
	END

	UPDATE Store.Product SET EmployeePrice = @EmployeePrice WHERE ProductID = @ProductID
END