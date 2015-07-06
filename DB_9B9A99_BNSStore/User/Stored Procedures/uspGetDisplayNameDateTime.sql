-- =============================================
-- Author:		Cosh_
-- Create date: 2014.9.13
-- Description:	Create simple user
-- =============================================
CREATE PROCEDURE [User].[uspGetDisplayNameDateTime]
	@UserID int = NULL,
	@Username varchar(100) = NULL,
	@DisplayNameDateTime datetime2(0) = NULL OUTPUT
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
	
	SET @DisplayNameDateTime = (SELECT DisplayNameDateTime FROM [User].DisplayName WHERE UserID = @UserID)
END

