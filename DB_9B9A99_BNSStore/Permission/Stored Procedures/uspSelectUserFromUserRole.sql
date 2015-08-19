CREATE PROCEDURE Permission.uspSelectUserFromUserRole
	@RoleID smallint = NULL,
	@RoleName varchar(100) = NULL
AS
BEGIN
	SET NOCOUNT ON;

	IF @RoleID IS NULL
	BEGIN
		EXEC [Permission].uspGetRoleID @RoleName = @RoleName, @RoleID = @RoleID OUTPUT
	END

	SELECT UserID FROM [Permission].[UserRole] WHERE RoleID = @RoleID
END