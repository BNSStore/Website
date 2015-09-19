CREATE PROCEDURE [Store].[uspGetSaleTotal]
	@Store char(1) = NULL,
	@Date date = NULL,
	@Total smallmoney = NULL OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	IF @Date IS NULL
	BEGIN
		SET @Date = CONVERT(date, GETDATE());
	END

	SET @Total = (SELECT SUM(((ProductPrice * [Count]) + (EmployeePrice * EmployeeCount))) AS Total FROM Store.Sale WHERE [Date] = @Date AND Store = @Store);
	IF @Total IS NULL
	BEGIN
		SET @Total = 0
	END

END