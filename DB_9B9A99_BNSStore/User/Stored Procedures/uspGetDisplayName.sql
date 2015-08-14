CREATE PROCEDURE [User].[uspGetDisplayName]
	@UserID int = NULL,
	@Username varchar(100) = NULL,
	@DisplayName nvarchar(100) = NULL OUTPUT
AS
BEGIN
	SET NOCOUNT ON;

	IF @UserID IS NULL
	BEGIN
		EXEC [User].uspGetUserID @Username = @Username, @UserID = @UserID OUTPUT
	END
	
	SET @DisplayName = (SELECT DisplayName FROM [User].DisplayName WHERE UserID = @UserID)
END