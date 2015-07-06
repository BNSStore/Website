-- =============================================
-- Author:		Cosh_
-- Create date: 2014.11.6
-- Description:	Add Translation
-- =============================================

CREATE PROCEDURE [Store].[uspGetProductName]
    @ProductID smallint = NULL ,
    @ProductName varchar(100) = NULL OUTPUT
AS
	BEGIN
	    -- SET NOCOUNT ON added to prevent extra result sets from
	    -- interfering with SELECT statements.

	    SET NOCOUNT ON;

	    -- Insert statements for procedure here

	    SET @ProductName = (SELECT ProductName FROM Store.Product WHERE ProductID = @ProductID)
	END;

