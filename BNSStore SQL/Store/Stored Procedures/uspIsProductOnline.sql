CREATE PROCEDURE [Store].[uspIsProductOnline]
	@ProductID smallint = NULL,
	@Online bit = NULL OUTPUT
AS
BEGIN

	SET NOCOUNT ON

	SET @Online = (SELECT [Online] FROM Store.Product WHERE ProductID = @ProductID)

END