CREATE PROCEDURE [Store].[uspDelProduct]
    @ProductID smallint = NULL,
    @ProductName varchar(100) = NULL
AS
BEGIN

	SET NOCOUNT ON

	IF @ProductID IS NULL
	BEGIN
		EXEC Store.uspGetProductID @ProductID = @ProductID OUTPUT, @ProductName = @ProductName
	END

	DELETE FROM Store.Product WHERE ProductID = @ProductID
END
