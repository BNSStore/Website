CREATE PROCEDURE Permission.uspAddRolePolicy
	@RoleID smallint = NULL,
	@RoleName varchar(100) = NULL,
	@PolicyID smallint = NULL,
	@PolicyName varchar(100) = NULL
AS
BEGIN

	IF @RoleID IS NULL
	BEGIN
		EXEC [Permission].uspGetRoleID @RoleName = @RoleName, @RoleID = @RoleID OUTPUT
	END

	IF @PolicyID IS NULL
	BEGIN
		EXEC [Permission].uspGetPolicyID @PolicyName = @PolicyName, @PolicyID = @PolicyID OUTPUT
	END
	
	INSERT INTO [Permission].[RolePolicy] (RoleID, PolicyID) VALUES (@RoleID, @PolicyID)

END