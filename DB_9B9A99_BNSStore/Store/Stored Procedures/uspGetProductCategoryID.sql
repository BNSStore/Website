-- =============================================
-- Author:		Cosh_
-- Create date: 2014.11.6
-- Description:	Add Translation

-- =============================================
CREATE PROCEDURE [Store].[uspGetProductCategoryID]
	@ProductID smallint = NULL,
	@CategoryID tinyint = NULL OUTPUT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here

	SET @CategoryID = (SELECT CategoryID FROM Store.Product WHERE ProductID = @ProductID)

END

