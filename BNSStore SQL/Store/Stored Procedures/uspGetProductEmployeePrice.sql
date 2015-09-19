CREATE PROCEDURE [Store].[uspGetProductEmployeePrice]
	@ProductID smallint = NULL,
	@EmployeePrice smallmoney = NULL OUTPUT
AS
BEGIN

	SET NOCOUNT ON

	SET @EmployeePrice = (SELECT EmployeePrice FROM Store.Product WHERE ProductID = @ProductID)

END
