-- =============================================
-- Author:		Cosh_
-- Create date: 2014.11.6
-- Description:	Add Translation

-- =============================================
CREATE PROCEDURE Store.uspAddEmployee
	@UserID int = NULL,
	@Username varchar(100) = NULL,
	@FirstName nvarchar(100) = NULL,
	@MiddleName nvarchar(100) = NULL,
	@LastName nvarchar(100) = NULL,
	@GroupID tinyint = NULL,
	@GroupName varchar(50) = NULL,
	@Manager bit = 0
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

	IF @GroupID IS NULL
	BEGIN
		EXEC Store.uspGetGroupID @GroupName = @GroupName, @GroupID = @GroupID OUTPUT
	END

	INSERT INTO [User].Info (UserID, FirstName, MiddleName, LastName) VALUES (@UserID, @FirstName, @MiddleName, @LastName )

	INSERT INTO Store.Employee (UserID, GroupID, Manager) VALUES (@UserID, @GroupID, @Manager)

END
