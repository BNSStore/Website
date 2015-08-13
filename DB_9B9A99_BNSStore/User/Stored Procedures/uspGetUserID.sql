CREATE PROCEDURE [User].[uspGetUserID] 
	@UserID int = NULL OUTPUT,
	@Username varchar(100) = NULL,
	@EmailAddress varchar(254) = NULL

AS
BEGIN
	SET NOCOUNT ON;

	IF @Username IS NOT NULL
	BEGIN
		SET @UserID = (SELECT [UserID] FROM [User].Account WHERE Username = @Username)
	END
	ELSE IF @EmailAddress IS NOT NULL
	BEGIN
		SET @UserID = (SELECT [UserID] FROM [User].Email WHERE EmailAddress = @EmailAddress)
	END
	IF @UserID IS NULL
	BEGIN
			SET @UserID = -1;
	END
END
