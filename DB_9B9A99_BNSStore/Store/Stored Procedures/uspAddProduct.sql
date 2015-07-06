-- =============================================
-- Author:		Cosh_
-- Create date: 2014.11.6
-- Description:	Add Translation
-- =============================================

CREATE PROCEDURE [Store].[uspAddProduct]
    @ProductName varchar(100) = NULL,
    @ProductPrice smallmoney = NULL,
    @EmployeePrice smallmoney = NULL,
    @CategoryID tinyint = NULL,
    @CategoryName varchar(100) = NULL,
    @Online bit = NULL
AS
	BEGIN
	    -- SET NOCOUNT ON added to prevent extra result sets from
	    -- interfering with SELECT statements.

	    SET NOCOUNT ON;

	    -- Insert statements for procedure here

	    IF @CategoryID IS NULL
	    BEGIN
		  EXEC Store.uspGetCategoryID @CategoryName = @CategoryName, @CategoryID = @CategoryID OUTPUT
	    END

	    INSERT INTO Store.Product (ProductName, ProductPrice, EmployeePrice, CategoryID, [Online], OnSalePrice)VALUES
	    (@ProductName, @ProductPrice, @EmployeePrice, @CategoryID, @Online, NULL) 
	END;
