-- =============================================
-- Author:		Cosh_
-- Create date: 2014.11.6
-- Description:	Add Translation

-- =============================================
CREATE PROCEDURE [Store].[uspGetCurrentShifts]
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT [Date], UserID, Store FROM Store.Schedule WHERE [Date] = CONVERT(date,GETDATE());
END

GO
GRANT EXECUTE
    ON OBJECT::[Store].[uspGetCurrentShifts] TO [db_executor];

