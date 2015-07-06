-- =============================================
-- Author:		Cosh_
-- Create date: 2014.9.13
-- Description:	Create simple user
-- =============================================
CREATE PROCEDURE [User].[uspAddFailedLogin]
	@UserID int = NULL,
	@FailedLoginID tinyint = NULL OUTPUT,
	@IPv4 varchar(15) = NULL,
	@IPv6 varchar(45) = NULL
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SET @FailedLoginID = (SELECT TOP 1 FailedLoginID FROM [User].FailedLogin WHERE UserID = @UserID ORDER BY FailedLoginID DESC)
	IF @FailedLoginID IS NULL OR @FailedLoginID > 99
	BEGIN
		SET @FailedLoginID = 0
	END
	SET @FailedLoginID = @FailedLoginID + 1
	DELETE FROM [User].FailedLogin WHERE FailedLoginID = @FailedLoginID
	INSERT INTO [User].FailedLogin (UserID,FailedLoginID,LoginDateTime,LoginIPv4,LoginIPv6)
	VALUES(@UserID, @FailedLoginID, GETDATE(),@IPv4,@IPv6)
END

