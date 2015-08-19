CREATE PROCEDURE Permission.uspGetRoleID
	@RoleID smallint = NULL OUTPUT,
	@RoleName varchar(100) = NULL
AS
BEGIN
	SET NOCOUNT ON;
	
	SET @RoleID = (SELECT RoleID FROM [Permission].[Role] WHERE RoleName = @RoleName)
END