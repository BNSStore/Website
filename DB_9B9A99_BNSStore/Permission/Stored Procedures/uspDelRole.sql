CREATE PROCEDURE Permission.uspDelRole
	@RoleName varchar(100) = NULL
AS
BEGIN
	SET NOCOUNT ON;
	
	DELETE FROM [Permission].[Role] WHERE RoleName = @RoleName
END