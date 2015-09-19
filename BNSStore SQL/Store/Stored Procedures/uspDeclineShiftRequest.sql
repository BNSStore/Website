CREATE PROCEDURE [Store].[uspDeclineShiftRequest]
	  @SenderID int = NULL,
	  @RecieverID int = NULL,
	  @Date date = NULL
AS
BEGIN

	SET NOCOUNT ON
		   
	IF(SELECT [Date] FROM Store.ShiftRequest WHERE SenderID = @SenderID AND RecieverID = @RecieverID AND [Date] = @Date) IS NULL
	BEGIN
		RAISERROR('Cannot find ShiftRequest', 16, 1)
		RETURN;
	END
	DELETE FROM Store.ShiftRequest WHERE SenderID = @SenderID AND RecieverID = @RecieverID AND [Date] = @Date;
END
