CREATE PROCEDURE [User].[uspIsEmailSub]
	@UserID int = NULL,
	@Username varchar(100) = NULL,
	@EmailAddress nvarchar(254) = NULL,
	@ProviderID int = NULL,
	@ProviderName varchar(100) = NULL,
	@SubTypeName varchar(100) = NULL,
	@SubTypeID int = NULL,
	@IsEmailSub bit = NULL OUTPUT
AS
BEGIN
	SET NOCOUNT ON;

	IF @UserID IS NULL
	BEGIN
		EXEC [User].[uspGetUserID] @Username = @Username, @EmailAddress = @EmailAddress, @UserID = @UserID OUTPUT;
	END;

	IF @EmailAddress IS NULL
		BEGIN EXEC [User].[uspGetMainEmailAddress] @UserID = @UserID, @EmailAddress = @EmailAddress OUTPUT
	END

	IF @SubTypeID IS NULL
	BEGIN
		EXEC [Svc].uspGetSubTypeID @ProviderID = @ProviderID, @ProviderName = @ProviderName, @SubTypeName = @SubTypeName, @SubTypeID = @SubTypeID OUTPUT
	END

	IF(SELECT [UserID] FROM [User].[EmailSub] WHERE [EmailAddress] = @EmailAddress AND [SubTypeID] = @SubTypeID) IS NULL
	BEGIN
		SET @IsEmailSub = 0;
	END
	ELSE
	BEGIN
		SET @IsEmailSub = 1
	END
END
