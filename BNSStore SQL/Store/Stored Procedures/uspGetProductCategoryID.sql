CREATE PROCEDURE [Store].[uspGetProductCategoryID]
	@ProductID smallint = NULL,
	@CategoryID tinyint = NULL OUTPUT
AS
BEGIN

	SET NOCOUNT ON

	SET @CategoryID = (SELECT CategoryID FROM Store.Product WHERE ProductID = @ProductID)

END

