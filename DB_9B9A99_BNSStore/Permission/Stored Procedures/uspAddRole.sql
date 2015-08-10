CREATE PROCEDURE Permission.uspAddRole
	@RoleName varchar(100) = NULL
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	
	INSERT INTO [Permission].[Role] (RoleName) VALUES (@RoleName)

END