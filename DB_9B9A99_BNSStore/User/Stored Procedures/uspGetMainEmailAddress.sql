-- =============================================
-- Author:		Cosh_
-- Create date: 2014.11.6
-- Description:	Add Translation
-- =============================================
CREATE PROCEDURE [User].[uspGetMainEmailAddress]
	@UserID int = NULL,
	@Username varchar(100) = NULL,
	@EmailAddress nvarchar(254) = NULL OUTPUT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	IF @UserID IS NULL
	BEGIN
		EXEC [User].uspGetUserID @Username = @Username, @UserID = @UserID OUTPUT
	END
	
	SET @EmailAddress = (SELECT EmailAddress FROM [User].Email WHERE UserID = @UserID AND Main = 1); 
END

GO
GRANT EXECUTE
    ON OBJECT::[User].[uspGetMainEmailAddress] TO [db_executor]
    AS [dbo];

