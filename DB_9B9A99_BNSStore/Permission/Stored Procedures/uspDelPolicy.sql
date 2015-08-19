CREATE PROCEDURE Permission.uspDelPolicy
	@PolicyName varchar(100) = NULL
AS
BEGIN
	SET NOCOUNT ON;

	DELETE FROM [Permission].[Policy] WHERE PolicyName = @PolicyName

END