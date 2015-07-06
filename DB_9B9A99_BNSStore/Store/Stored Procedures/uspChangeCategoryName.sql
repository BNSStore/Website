-- =============================================
-- Author:		Cosh_
-- Create date: 2014.11.6
-- Description:	Add Translation
-- =============================================

CREATE PROCEDURE [Store].[uspChangeCategoryName]
    @CategoryID tinyint = NULL,
    @CategoryName varchar(100) = NULL,
    @NewCategoryName varchar(100) = NULL
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

	    UPDATE Store.ProductCategory SET CategoryName = @NewCategoryName WHERE CategoryID = @CategoryID
	END;
