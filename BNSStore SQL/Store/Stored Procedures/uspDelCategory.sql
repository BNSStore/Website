CREATE PROCEDURE [Store].[uspDelCategory]
    @CategoryID tinyint = NULL,
    @CategoryName varchar(100) = NULL
AS
BEGIN

	SET NOCOUNT ON

	IF @CategoryID IS NULL
	BEGIN
		EXEC Store.uspGetCategoryID @CategoryID = @CategoryID OUTPUT, @CategoryName = @CategoryName;
	END

	DELETE FROM Store.ProductCategory WHERE CategoryID = @CategoryID
END
