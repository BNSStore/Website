-- =============================================
-- Author:		Cosh_
-- Create date: 2014.9.13
-- Description:	Create simple user
-- =============================================
CREATE PROCEDURE [User].[uspAddSession]
	@UserID int = NULL,
	@IPv4 varchar(15) = NULL,
	@IPv6 varchar(45) = NULL,
	@SessionToken char(32) = NULL OUTPUT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	DECLARE @SessionCount tinyint = 0,
	@MaxSessionCount tinyint = 20
	SET @SessionCount =(SELECT Count(*) FROM [User].[Session] WHERE UserID = @UserID)

	IF @SessionCount >= @MaxSessionCount
	BEGIN
		WITH  OldestSession AS
        (
			SELECT TOP (@SessionCount - @MaxSessionCount + 1) * FROM [User].[Session] WHERE UserID = @UserID ORDER BY LoginDateTime
        )
		DELETE
		FROM    OldestSession
	END

	EXEC dbo.uspGenerateRandomString 32, @RandomString = @SessionToken OUTPUT
	INSERT INTO [User].[Session] (UserID,SessionToken,LoginDateTime,LoginIPv4,LoginIPv6)
	VALUES(@UserID, @SessionToken, GETDATE(),@IPv4,@IPv6)
END

