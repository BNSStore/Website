CREATE PROCEDURE Permission.uspGetPolicyName
	@PolicyID smallint = NULL ,
	@PolicyName varchar(100) = NULL OUTPUT
AS
BEGIN
	SET NOCOUNT ON;
	
	SET @PolicyName = (SELECT PolicyID FROM [Permission].[Policy] WHERE PolicyID = @PolicyID)
END