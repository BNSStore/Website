CREATE PROCEDURE Permission.uspDelPolicy
	@PolicyName varchar(100) = NULL
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	
	DELETE FROM [Permission].[Policy] WHERE PolicyName = @PolicyName

END