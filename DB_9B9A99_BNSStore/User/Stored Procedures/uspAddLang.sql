-- =============================================
-- Author:		Cosh_
-- Create date: 2014.9.13
-- Description:	Create simple user
-- =============================================
CREATE PROCEDURE [User].[uspAddLang]
	@UserID int = NULL,
	@Username varchar(100) = NULL,
	@LangID tinyint = NULL,
	@LangName varchar(100) = NULL,
	@Main bit = 0

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	IF @LangID IS NULL
	BEGIN
		EXEC Lang.uspGetLangID @LangName = @LangName, @LangID = @LangID OUTPUT
	END
	IF @UserID IS NULL
	BEGIN
		EXEC [User].uspGetUserID @Username = @Username, @UserID = @UserID OUTPUT
	END
	IF @Main = 1
	BEGIN
		UPDATE [User].Lang SET Main = 0 WHERE UserID = @UserID
	END

	DELETE FROM [User].Lang WHERE UserID = @UserID AND [LangID] = @LangID;

	INSERT INTO [User].Lang (UserID, [LangID], Main)
	VALUES (@UserID, @LangID, @Main)
END

