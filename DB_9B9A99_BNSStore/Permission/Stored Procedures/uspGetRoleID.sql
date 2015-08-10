CREATE PROCEDURE Permission.uspGetRoleID
	@RoleID smallint = NULL OUTPUT,
	@RoleName varchar(100) = NULL
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	
	SET @RoleID = (SELECT RoleID FROM [Permission].[Role] WHERE RoleName = @RoleName)

END