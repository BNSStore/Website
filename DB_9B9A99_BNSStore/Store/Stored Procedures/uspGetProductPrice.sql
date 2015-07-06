-- =============================================
-- Author:		Cosh_
-- Create date: 2014.11.6
-- Description:	Add Translation

-- =============================================
CREATE PROCEDURE [Store].[uspGetProductPrice]
	@ProductID smallint = NULL,
	@ProductPrice smallmoney = NULL OUTPUT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here

	SET @ProductPrice = (SELECT ProductPrice FROM Store.Product WHERE ProductID = @ProductID)

END

