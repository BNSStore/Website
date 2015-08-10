CREATE PROCEDURE Permission.uspGetPolicyName
	@PolicyID smallint = NULL ,
	@PolicyName varchar(100) = NULL OUTPUT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	
	SET @PolicyName = (SELECT PolicyID FROM [Permission].[Policy] WHERE PolicyID = @PolicyID)

END