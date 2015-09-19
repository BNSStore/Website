CREATE PROCEDURE [Store].[uspChangeCategoryName]
    @CategoryID tinyint = NULL,
    @CategoryName varchar(100) = NULL,
    @NewCategoryName varchar(100) = NULL
AS
BEGIN
	SET NOCOUNT ON

	IF @CategoryID IS NULL
	BEGIN
		EXEC Store.uspGetCategoryID @CategoryName = @CategoryName, @CategoryID = @CategoryID OUTPUT
	END

	UPDATE Store.ProductCategory SET CategoryName = @NewCategoryName WHERE CategoryID = @CategoryID
END
