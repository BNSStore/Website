-- =============================================
-- Author:		Cosh_
-- Create date: 2014.11.6
-- Description:	Add Translation
-- =============================================

CREATE PROCEDURE [Store].[uspGetProductID]
	 @ProductID SMALLINT = NULL OUTPUT,
	 @ProductName VARCHAR(100) = NULL
AS
	BEGIN
	    -- SET NOCOUNT ON added to prevent extra result sets from
	    -- interfering with SELECT statements.

	    SET NOCOUNT ON;

	    -- Insert statements for procedure here

	    SET @ProductID = (SELECT [ProductID] FROM [Store].[Product]
					  WHERE [ProductName] = @ProductName);
	    IF @ProductID IS NULL
		   BEGIN
			  RAISERROR('Cannot find Product', 16, 1);
			  RETURN;
		   END;
	END;

