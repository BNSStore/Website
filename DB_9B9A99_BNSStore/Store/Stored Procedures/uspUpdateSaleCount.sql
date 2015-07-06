-- =============================================
-- Author:		Cosh_
-- Create date: 2014.11.6
-- Description:	Add Translation

-- =============================================
CREATE PROCEDURE [Store].[uspUpdateSaleCount]
	@Store char = NULL,
	@ProductID smallint = NULL,
	@Count smallint = NULL,
	@EmployeeCount smallint = NULL
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here

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

GO
GRANT EXECUTE
    ON OBJECT::[Store].[uspUpdateSaleCount] TO [db_executor];

