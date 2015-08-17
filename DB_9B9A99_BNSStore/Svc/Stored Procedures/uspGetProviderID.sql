CREATE PROCEDURE [Svc].[uspGetProviderID]
	@ProviderName varchar(100) = NULL,
	@ProviderID int = NULL OUTPUT
AS
BEGIN
	SET NOCOUNT ON;

	SET @ProviderID = (SELECT ProviderID FROM Svc.Provider WHERE ProviderName = @ProviderName)
END
