CREATE PROCEDURE [Permission].[uspSelectPolicyFromUser]
	@UserID int = NULL,
	@Username varchar(100) = NULL,
	@PolicyID smallint = NULL,
	@PolicyName varchar(100) = NULL,
	@WildCard bit = 1
AS
BEGIN

	SET NOCOUNT ON;
	IF @UserID IS NULL
	BEGIN
		EXEC [User].uspGetUserID @Username = @Username, @UserID = @UserID OUTPUT
	END

	--TODO: Add Role Support
	SELECT RoleID FROM [Permission].UserRole WHERE UserID = @UserID
END