-- =============================================
-- Author:		Cosh_
-- Create date: 2014.9.13
-- Description:	Create simple user
-- =============================================
CREATE PROCEDURE [Svc].[uspGetProviderID]
	@ProviderName varchar(100) = NULL,
	@ProviderID int = NULL OUTPUT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SET @ProviderID = (SELECT ProviderID FROM Svc.Provider WHERE ProviderName = @ProviderName)
END
