USE [BNSStore]
GO
/****** Object:  StoredProcedure [Store].[uspGetProductEmployeePrice]    Script Date: 4/28/2015 2:29:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Cosh_
-- Create date: 2014.11.6
-- Description:	Add Translation

-- =============================================
CREATE PROCEDURE [Store].[uspGetProductEmployeePrice]
	@ProductID smallint = NULL,
	@EmployeePrice smallmoney = NULL OUTPUT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here

	SET @EmployeePrice = (SELECT EmployeePrice FROM Store.Product WHERE ProductID = @ProductID)

END

GO
