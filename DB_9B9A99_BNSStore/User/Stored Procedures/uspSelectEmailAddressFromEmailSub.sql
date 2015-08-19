CREATE PROCEDURE [User].[uspSelectEmailAddressIDFromEmailSub]
	@ProviderID int = NULL,
	@ProviderName varchar(100) = NULL,
	@SubTypeID int = NULL,
	@SubTypeName varchar(100) = NULL
AS
BEGIN
	IF @SubTypeID IS NULL
	BEGIN
		EXEC [Svc].uspGetSubTypeID @ProviderID = @ProviderID, @ProviderName = @ProviderName, @SubTypeName = @SubTypeName, @SubTypeID = @SubTypeID OUTPUT
	END
END