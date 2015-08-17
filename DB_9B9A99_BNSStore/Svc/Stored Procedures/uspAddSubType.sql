CREATE PROCEDURE [Svc].[uspAddSubType]
	@ProviderID int = NULL,
	@ProviderName varchar(100) = NULL,
	@SubTypeName varchar(100) = NULL
AS
BEGIN
	IF @ProviderID IS NULL
	BEGIN
		EXEC Svc.uspGetProviderID @ProviderName = @ProviderName, @ProviderID = @ProviderID OUTPUT
	END
	
	IF (SELECT SubTypeID FROM Svc.SubType WHERE ProviderID = @ProviderID AND SubTypeName = @SubTypeName) IS NOT NULL
	BEGIN
		INSERT INTO Svc.SubType (ProviderID, SubTypeName) VALUES (@ProviderID, @SubTypeName)
	END
END
