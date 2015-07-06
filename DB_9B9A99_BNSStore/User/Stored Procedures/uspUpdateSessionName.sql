-- =============================================
-- Author:		Cosh_
-- Create date: 2014.9.13
-- Description:	Create simple user
-- =============================================
CREATE PROCEDURE [User].[uspUpdateSessionName]
	@UserID int = NULL,
	@Username varchar(100) = NULL,
	@SessionName nvarchar(100) = NULL,
	@SessionToken char(32) = NULL
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

	UPDATE [User].[Session] SET SessionName = @SessionName WHERE UserID = @UserID AND SessionToken = @SessionToken 
END

