CREATE PROCEDURE Permission.uspGetPolicyID
	@PolicyID smallint = NULL OUTPUT,
	@PolicyName varchar(100) = NULL
AS
BEGIN
	SET NOCOUNT ON;
	
	SET @PolicyID = (SELECT PolicyID FROM [Permission].[Policy] WHERE PolicyName = @PolicyName)
END