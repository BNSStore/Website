CREATE PROCEDURE Permission.uspAddUserRole
	@UserID int = NULL,
	@Username varchar(100) = NULL,
	@RoleID smallint = NULL,
	@RoleName varchar(100) = NULL
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
	
	INSERT INTO [Permission].[UserRole] (UserID, RoleID) VALUES (@UserID, @RoleName)

END