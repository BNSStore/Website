CREATE PROCEDURE Permission.uspDoesUserContainPolicy
	@UserID int = NULL,
	@Username varchar(100) = NULL,
	@PolicyID smallint = NULL,
	@PolicyName varchar(100) = NULL,
	@Contains bit = 0
AS
BEGIN
	SET NOCOUNT ON;
	
	IF @UserID IS NULL
	BEGIN
		EXEC [User].uspGetUserID @Username = @Username, @UserID = @UserID OUTPUT
	END

	IF @PolicyID IS NULL
	BEGIN
		EXEC [Permission].uspGetPolicyID @PolicyName = @PolicyName, @PolicyID = @PolicyID OUTPUT
	END
	
	IF (SELECT UserID FROM [Permission].[UserPolicy] WHERE UserID = @UserID AND PolicyID = @PolicyID) IS NOT NULL
	BEGIN
		SET @Contains = 1;
	END
	ELSE
	BEGIN
		IF(SELECT rp.RoleID FROM [Permission].[RolePolicy] rp WHERE rp.PolicyID = @PolicyID 
		AND rp.RoleID = (SELECT ur.RoleID FROM [Permission].UserRole ur WHERE ur.UserID = @UserID)) IS NOT NULL
		BEGIN
			SET @Contains = 1;
		END
	END

END