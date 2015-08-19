CREATE PROCEDURE [User].[uspVerifyEmailAddress]
	@EmailAddress nvarchar(254) = NULL,
	@VerifyString varchar(64) = NULL
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @UserID int = NULL
	SET @UserID = (SELECT UserID FROM [User].EmailVerify WHERE EmailAddress = @EmailAddress AND VerifyString = @VerifyString)
	IF @UserID IS NOT NULL
	BEGIN
		DELETE FROM [User].EmailVerify WHERE EmailAddress = @EmailAddress AND VerifyString = @VerifyString
		INSERT INTO [User].Email (UserID, EmailAddress) VALUES (@UserID, @EmailAddress)
	END
	RAISERROR('Cannot find matched EmailAddress and VerifyString', 16, 1);
END