CREATE PROCEDURE [User].[uspAddEmailAddress]
	@UserID int = NULL,
	@Username varchar(100) = null,
	@EmailAddress nvarchar(254) = NULL,
	@VerifyString varchar(64) = NULL OUTPUT
AS
BEGIN
	SET NOCOUNT ON;

	IF @UserID IS NULL AND @Username IS NOT NULL
	BEGIN
		EXEC [User].uspGetUserID @Username = @Username, @UserID = @UserID OUTPUT
	END

	EXEC dbo.uspGenerateRandomString @Length = 64, @LetterCase = 'L', @Numbers = 1, @RandomString = @VerifyString OUTPUT

	IF (SELECT EmailAddress FROM [User].EmailVerify WHERE EmailAddress = @EmailAddress) IS NOT NULL
	BEGIN
		INSERT INTO [User].EmailVerify(EmailAddress,VerifyString, UserID) VALUES (@EmailAddress, @VerifyString, @UserID)
	END
	ELSE
	BEGIN
		UPDATE [User].EmailVerify SET VerifyString = @VerifyString, UserID = @UserID WHERE EmailAddress = @EmailAddress
	END
END