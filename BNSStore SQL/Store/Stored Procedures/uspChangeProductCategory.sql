CREATE PROCEDURE [Store].[uspChangeProductCategory]
    @ProductID smallint = NULL,
    @ProductName varchar(100) = NULL,
    @CategoryID tinyint = NULL,
    @CategoryName varchar(100) = NULL
AS
BEGIN

	SET NOCOUNT ON

	IF @ProductID IS NULL
	BEGIN
		EXEC Store.uspGetProductID @ProductID = @ProductID OUTPUT, @ProductName = @ProductName
	END

	IF @CategoryID IS NULL
	BEGIN
		EXEC Store.uspGetCategoryID @CategoryName = @CategoryName, @CategoryID = @CategoryID OUTPUT
	END

	UPDATE Store.Product SET CategoryID = @CategoryID WHERE ProductID = @ProductID
END
