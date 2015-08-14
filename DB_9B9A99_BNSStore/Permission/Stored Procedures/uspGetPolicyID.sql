CREATE PROCEDURE Permission.uspGetPolicyID
	@PolicyID smallint = NULL OUTPUT,
	@PolicyName varchar(100) = NULL
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	
	SET @PolicyID = (SELECT PolicyID FROM [Permission].[Policy] WHERE PolicyName = @PolicyName)

END