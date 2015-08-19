CREATE PROCEDURE Permission.uspAddPolicy
	@PolicyName varchar(100) = NULL
AS
BEGIN
	SET NOCOUNT ON;
	
	INSERT INTO [Permission].[Policy] (PolicyName) VALUES (@PolicyName)
END