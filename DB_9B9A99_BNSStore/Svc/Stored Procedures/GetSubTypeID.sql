CREATE PROCEDURE [Svc].[uspGetSubTypeID]
	@ProviderID int = NULL,
	@ProviderName varchar(100) = NULL,
	@SubTypeName varchar(100) = NULL,
	@SubTypeID int = NULL OUTPUT
AS
BEGIN
	IF @ProviderID IS NULL
	BEGIN
		EXEC Svc.uspGetProviderID @ProviderName = @ProviderName, @ProviderID = @ProviderID OUTPUT
	END
	
	SET @SubTypeID = (SELECT SubTypeID FROM [Svc].[SubType] WHERE ProviderID = @ProviderID AND SubTypeName = @SubTypeName)
END