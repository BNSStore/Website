CREATE PROCEDURE [User].uspGetEmailVerifyString
	@EmailAddress nvarchar(254) = NULL,
	@VerifyString varchar(64) = NULL OUTPUT
AS
BEGIN
	SET NOCOUNT ON;

	SET @VerifyString = (SELECT VerifyString FROM [User].EmailVerify WHERE EmailAddress = @EmailAddress);
END