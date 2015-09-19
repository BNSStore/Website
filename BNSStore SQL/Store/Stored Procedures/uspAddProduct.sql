CREATE PROCEDURE [Store].[uspAddProduct]
    @ProductName varchar(100) = NULL,
    @ProductPrice smallmoney = NULL,
    @EmployeePrice smallmoney = NULL,
    @CategoryID tinyint = NULL,
    @CategoryName varchar(100) = NULL,
    @Online bit = NULL
AS
BEGIN
	SET NOCOUNT ON;

	IF @CategoryID IS NULL
	BEGIN
		EXEC Store.uspGetCategoryID @CategoryName = @CategoryName, @CategoryID = @CategoryID OUTPUT
	END

	INSERT INTO Store.Product (ProductName, ProductPrice, EmployeePrice, CategoryID, [Online], OnSalePrice)VALUES
	(@ProductName, @ProductPrice, @EmployeePrice, @CategoryID, @Online, NULL) 
END;
