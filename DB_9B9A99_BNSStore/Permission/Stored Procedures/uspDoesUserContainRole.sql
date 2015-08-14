CREATE PROCEDURE Permission.uspDoesUserContainRole
	@UserID int = NULL,
	@Username varchar(100) = NULL,
	@RoleID smallint = NULL,
	@RoleName varchar(100) = NULL,
	@Contains bit = 0
AS
BEGIN
	SET NOCOUNT ON;
	
	IF @UserID IS NULL
	BEGIN
		EXEC [User].uspGetUserID @Username = @Username, @UserID = @UserID OUTPUT
	END

	IF @RoleID IS NULL
	BEGIN
		EXEC [Permission].uspGetRoleID @RoleName = @RoleName, @RoleID = @RoleID OUTPUT
	END
	
	IF (SELECT UserID FROM [Permission].[UserRole] WHERE UserID = @UserID AND RoleID = @RoleID) IS NOT NULL
	BEGIN
		SET @Contains = 1;
	END

END