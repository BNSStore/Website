-- =============================================
-- Author:		Cosh_
-- Create date: 2014.11.6
-- Description:	Add Translation
-- =============================================

CREATE PROCEDURE [Store].[uspSelectSales]
	  @StartDate date = NULL,
	  @EndDate date = NULL
AS
	BEGIN
	    -- SET NOCOUNT ON added to prevent extra result sets from
	    -- interfering with SELECT statements.

	    SET NOCOUNT ON;

	    -- Insert statements for procedure here

	    IF @StartDate IS NULL
		   BEGIN
			  SET @StartDate = CONVERT(date, GETDATE());
		   END;

	    IF @EndDate IS NULL
		   BEGIN
			  SET @EndDate = CONVERT(date, GETDATE());
		   END;

	    SELECT [Date], [Store], (
						    SELECT [ProductName] FROM [Store].[Product] [p]
						    WHERE [p].[ProductID] = [s].[ProductID]) AS [ProductName], [Count], [EmployeeCount] FROM [Store].[Sale] [s]
	    WHERE [s].[Date] >= @StartDate AND
			[s].[Date] <= @EndDate
	    ORDER BY [Date];
	END;
