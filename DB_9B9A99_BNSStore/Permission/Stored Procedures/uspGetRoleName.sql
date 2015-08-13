CREATE PROCEDURE Permission.uspGetRoleName
	@RoleID smallint = NULL,
	@RoleName varchar(100) = NULL OUTPUT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	
	SET @RoleName = (SELECT RoleName FROM [Permission].[Role] WHERE RoleID = @RoleID)

END