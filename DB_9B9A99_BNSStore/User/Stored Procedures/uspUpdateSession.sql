CREATE PROCEDURE [User].[uspUpdateSession]
	@UserID int = NULL,
	@Username varchar(100) = NULL,
	@SessionToken char(32) = NULL,
	@NewSessionToken char(32) = NULL,
	@IPv4 varchar(15) = NULL,
	@IPv6 varchar(45) = NULL
AS
BEGIN
	SET NOCOUNT ON;

	IF @UserID IS NULL
	BEGIN
		EXEC [User].uspGetUserID @Username = @Username, @UserID = @UserID OUTPUT
	END

	UPDATE [User].[Session] SET
	SessionToken = @NewSessionToken,
	LoginDateTime = GETDATE(),
	LoginIPv4 = @IPv4,
	LoginIPv6 = @IPv6
	WHERE UserID = @UserID AND SessionToken = @SessionToken
END

