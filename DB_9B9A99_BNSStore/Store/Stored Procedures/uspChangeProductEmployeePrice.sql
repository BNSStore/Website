﻿-- =============================================
-- Author:		Cosh_
-- Create date: 2014.11.6
-- Description:	Add Translation
-- =============================================

CREATE PROCEDURE [Store].uspChangeProductEmployeePrice
    @ProductID smallint = NULL,
    @ProductName varchar(100) = NULL,
    @EmployeePrice smallmoney = NULL
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

	    UPDATE Store.Product SET EmployeePrice = @EmployeePrice WHERE ProductID = @ProductID
	END;
