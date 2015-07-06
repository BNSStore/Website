-- =============================================
-- Author:		Cosh_
-- Create date: 2014.9.13
-- Description:	Create simple user
-- =============================================
CREATE PROCEDURE [User].[uspGetUserID] 
	@UserID int = NULL OUTPUT,
	@Username varchar(100) = NULL,
	@EmailAddress varchar(254) = NULL

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here

	IF @Username IS NOT NULL
	BEGIN
		SET @UserID = (SELECT TOP 1 [UserID] FROM [User].Account WHERE Username = @Username)
	END
	ELSE IF @EmailAddress IS NOT NULL
	BEGIN
		SET @UserID = (SELECT TOP 1 [UserID] FROM [User].Email WHERE EmailAddress = @EmailAddress)
	END
	IF @UserID IS NULL
	BEGIN
			SET @UserID = -1;
	END
END


GO
GRANT EXECUTE
    ON OBJECT::[User].[uspGetUserID] TO [db_executor]
    AS [dbo];

