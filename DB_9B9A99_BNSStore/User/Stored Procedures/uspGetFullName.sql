CREATE PROCEDURE [User].[uspGetFullName]
	@UserID int = NULL,
	@Username varchar(100) = NULL,
	@FullName nvarchar(300) = NULL OUTPUT
AS
BEGIN
	SET NOCOUNT ON;

	IF @UserID IS NULL
	BEGIN
		EXEC [User].uspGetUserID @Username = @Username, @UserID = @UserID OUTPUT
	END

	DECLARE @FirstName varchar(100)
	DECLARE @MiddleName varchar(100)
	DECLARE @LastName varchar(100)
	
	SET @FirstName = (SELECT FirstName FROM [User].Info WHERE UserID = @UserID)
	SET @MiddleName = (SELECT FirstName FROM [User].Info WHERE UserID = @UserID)
	SET @LastName = (SELECT FirstName FROM [User].Info WHERE UserID = @UserID)

	IF @FirstName IS NOT NULL
	BEGIN
		SET @FullName = @FirstName + ' '
	END
	IF @MiddleName IS NOT NULL
	BEGIN
		SET @FullName = @FullName + @MiddleName + ' '
	END
	IF @LastName IS NOT NULL
	BEGIN
		SET @FullName = @FullName + @LastName
	END

	SET @FullName = RTRIM(@FullName)
	
END