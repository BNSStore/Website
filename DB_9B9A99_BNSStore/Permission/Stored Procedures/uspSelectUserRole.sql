CREATE PROCEDURE Permission.uspSelectUserRole
	@UserID int = NULL,
	@Username varchar(100) = NULL,
	@RoleID smallint = NULL,
	@RoleName varchar(100) = NULL
AS
BEGIN

	SET NOCOUNT ON;
	IF @UserID IS NOT NULL OR @Username IS NOT NULL
	BEGIN
		IF @UserID IS NULL
		BEGIN
			EXEC [User].uspGetUserID @Username = @Username, @UserID = @UserID OUTPUT
		END
		SELECT RoleID FROM [Permission].UserRole WHERE UserID = @UserID
	END
	ELSE IF @RoleID IS NOT NULL OR @RoleName IS NOT NULL
	BEGIN
		IF @RoleID IS NULL
		BEGIN
			EXEC [Permission].uspGetRoleID @RoleName = @RoleName, @RoleID = @RoleID OUTPUT
		END
		SELECT UserID FROM [Permission].[UserRole] WHERE RoleID = @RoleID
	END

END