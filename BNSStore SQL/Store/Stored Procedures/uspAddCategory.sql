CREATE PROCEDURE [Store].uspAddCategory
    @CategoryName varchar(100) = NULL
AS
BEGIN

	SET NOCOUNT ON;

	INSERT INTO Store.ProductCategory (CategoryName) VALUES (@CategoryName)
END