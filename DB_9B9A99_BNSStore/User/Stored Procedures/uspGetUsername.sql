CREATE PROCEDURE [User].[uspGetUsername] 
	@UserID int = NULL ,
	@Username varchar(100) = NULL OUTPUT

AS
BEGIN
	SET NOCOUNT ON;

	SET @Username = (SELECT [Username] FROM [User].Account WHERE UserID = @UserID)
END
