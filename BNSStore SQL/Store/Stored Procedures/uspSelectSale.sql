CREATE PROCEDURE [Store].[uspSelectSale]
	@StartDate date = NULL,
	@EndDate date = NULL,
	@Store char(1) = NULL
AS
BEGIN

	SET NOCOUNT ON

	IF @StartDate IS NULL
	BEGIN
		SET @StartDate = CONVERT(date, GETDATE())
	END

	IF @EndDate IS NULL
	BEGIN
		SET @EndDate = CONVERT(date, GETDATE())
	END

	IF @Store IS NOT NULL
	BEGIN
		SELECT [Date], [Store], [ProductID], [Count], [EmployeeCount] FROM [Store].[Sale] [s] 
		WHERE [s].[Date] >= @StartDate AND [s].[Date] <= @EndDate AND [s].Store = @Store
		ORDER BY [Date]
	END
	ELSE
	BEGIN
		SELECT [Date], [Store], [ProductID], [Count], [EmployeeCount] FROM [Store].[Sale] [s] 
		WHERE [s].[Date] >= @StartDate AND [s].[Date] <= @EndDate
		ORDER BY [Date]
	END

	
END
