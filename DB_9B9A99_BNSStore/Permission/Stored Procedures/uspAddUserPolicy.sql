CREATE PROCEDURE Permission.uspAddUserPolicy
	@UserID int = NULL,
	@Username varchar(100) = NULL,
	@PolicyID smallint = NULL,
	@PolicyName varchar(100) = NULL
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
	
	INSERT INTO [Permission].[UserPolicy] (UserID, PolicyID) VALUES (@UserID, @PolicyID)

END