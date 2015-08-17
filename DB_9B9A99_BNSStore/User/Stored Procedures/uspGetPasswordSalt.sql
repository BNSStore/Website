CREATE PROCEDURE [User].[uspGetPasswordSalt]
	@UserID int = NULL,
	@Username varchar(100) = NULL,
	@PasswordSalt char(64) = NULL OUTPUT

AS
BEGIN
	SET NOCOUNT ON;

	IF @UserID IS NULL
	BEGIN
		EXEC [User].uspGetUserID @Username = @Username, @UserID = @UserID OUTPUT
	END
	SET @PasswordSalt = (SELECT PasswordSalt FROM [User].Account WHERE UserID = @UserID)
END