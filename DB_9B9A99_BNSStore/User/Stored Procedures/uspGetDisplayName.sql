-- =============================================
-- Author:		Cosh_
-- Create date: 2014.9.13
-- Description:	Create simple user
-- =============================================
CREATE PROCEDURE [User].[uspGetDisplayName]
	@UserID int = NULL,
	@Username varchar(100) = NULL,
	@DisplayName nvarchar(100) = NULL OUTPUT
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

	DECLARE @IsEmployee bit
	EXEC Store.uspIsEmployee @UserID = @UserID, @IsEmployee = @IsEmployee OUTPUT
	IF @IsEmployee = 1
	BEGIN
		SET @DisplayName = (SELECT REPLACE((ISNULL(FirstName, '') + ' ' + ISNULL(MiddleName, '') + ' ' + ISNULL(LastName, '')), '  ', ' ') FROM [User].Info WHERE UserID = @UserID)
	END
	ELSE
	BEGIN
		SET @DisplayName = (SELECT DisplayName FROM [User].DisplayName WHERE UserID = @UserID)
	END
END


GO
GRANT EXECUTE
    ON OBJECT::[User].[uspGetDisplayName] TO [db_executor]
    AS [dbo];

