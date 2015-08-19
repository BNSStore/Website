CREATE PROCEDURE Permission.uspGetRoleName
	@RoleID smallint = NULL,
	@RoleName varchar(100) = NULL OUTPUT
AS
BEGIN
	SET NOCOUNT ON;
	
	SET @RoleName = (SELECT RoleName FROM [Permission].[Role] WHERE RoleID = @RoleID)
END