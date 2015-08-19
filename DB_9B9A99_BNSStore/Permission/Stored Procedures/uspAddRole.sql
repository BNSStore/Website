CREATE PROCEDURE Permission.uspAddRole
	@RoleName varchar(100) = NULL
AS
BEGIN
	SET NOCOUNT ON;
	
	INSERT INTO [Permission].[Role] (RoleName) VALUES (@RoleName)
END