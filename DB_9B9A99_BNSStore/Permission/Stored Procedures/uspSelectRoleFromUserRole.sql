CREATE PROCEDURE Permission.uspSelectRoleFromUserRole
	@UserID int = NULL,
	@Username varchar(100) = NULL
AS
BEGIN
	SET NOCOUNT ON;
	IF @UserID IS NULL
	BEGIN
		EXEC [User].uspGetUserID @Username = @Username, @UserID = @UserID OUTPUT
	END
	SELECT RoleID FROM [Permission].UserRole WHERE UserID = @UserID

END