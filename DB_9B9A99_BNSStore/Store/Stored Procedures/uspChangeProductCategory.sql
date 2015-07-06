-- =============================================
-- Author:		Cosh_
-- Create date: 2014.11.6
-- Description:	Add Translation
-- =============================================

CREATE PROCEDURE [Store].[uspChangeProductCategory]
    @ProductID smallint = NULL,
    @ProductName varchar(100) = NULL,
    @CategoryID tinyint = NULL,
    @CategoryName varchar(100) = NULL
AS
	BEGIN
	    -- SET NOCOUNT ON added to prevent extra result sets from
	    -- interfering with SELECT statements.

	    SET NOCOUNT ON;

	    -- Insert statements for procedure here

	    IF @ProductID IS NULL
	    BEGIN
		  EXEC Store.uspGetProductID @ProductID = @ProductID OUTPUT, @ProductName = @ProductName
	    END

	    IF @CategoryID IS NULL
	    BEGIN
		  EXEC Store.uspGetCategoryID @CategoryName = @CategoryName, @CategoryID = @CategoryID OUTPUT
	    END

	    UPDATE Store.Product SET CategoryID = @CategoryID WHERE ProductID = @ProductID
	END;
