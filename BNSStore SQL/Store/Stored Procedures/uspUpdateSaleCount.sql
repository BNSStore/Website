CREATE PROCEDURE [Store].[uspUpdateSaleCount]
	@Store char = NULL,
	@ProductID smallint = NULL,
	@Count smallint = NULL,
	@EmployeeCount smallint = NULL
AS
BEGIN
	SET NOCOUNT ON

	IF (NOT EXISTS(SELECT * FROM Store.Sale WHERE [Date] = CONVERT(date, GETDATE()) AND Store = @Store AND @ProductID = ProductID))
	BEGIN
		DECLARE @ProductPrice smallmoney,
		@EmployeePrice smallmoney
		EXEC Store.uspGetProductPrice @ProductID = @ProductID, @ProductPrice = @ProductPrice OUTPUT
		EXEC Store.uspGetProductEmployeePrice @ProductID = @ProductID, @EmployeePrice = @EmployeePrice OUTPUT
		INSERT INTO Store.Sale ([Date], Store, ProductID, ProductPrice, [Count], EmployeePrice, EmployeeCount)VALUES 
		(CONVERT(date, GETDATE()), @Store, @ProductID, @ProductPrice, @Count, @EmployeePrice, @EmployeeCount)
	END
	ELSE
	BEGIN
		UPDATE Store.Sale SET [Count] = @Count, [EmployeeCount] = @EmployeeCount WHERE [Date] = CONVERT(date, GETDATE()) AND Store = @Store AND @ProductID = ProductID
	END

END