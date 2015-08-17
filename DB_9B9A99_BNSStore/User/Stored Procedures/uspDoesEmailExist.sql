CREATE PROCEDURE [User].[uspDoesEmailExist]
	@EmailAddress nvarchar(254) = NULL,
	@Exist bit = 0 OUTPUT
AS
BEGIN
	SET NOCOUNT ON;

	IF (SELECT UserID FROM [User].Email WHERE EmailAddress = @EmailAddress) IS NOT NULL
	BEGIN
		SET @Exist = 1
	END
	ELSE
	BEGIN
		SET @Exist = 0
	END
END