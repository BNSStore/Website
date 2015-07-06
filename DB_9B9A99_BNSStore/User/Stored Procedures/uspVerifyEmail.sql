-- =============================================
-- Author:		Cosh_
-- Create date: 2014.11.6
-- Description:	Add Translation
-- =============================================
CREATE PROCEDURE [User].[uspVerifyEmail]
	@EmailAddress nvarchar(254) = NULL,
	@VerifyString varchar(64) = NULL,
	@Verified bit = NULL OUTPUT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE [User].Email SET VerifyString = NULL WHERE EmailAddress = @EmailAddress AND VerifyString = @VerifyString
	EXEC [User].uspIsEmailVerified @EmailAddress = @EmailAddress, @Verified = @Verified OUTPUT;
END

GO
GRANT EXECUTE
    ON OBJECT::[User].[uspVerifyEmail] TO [db_executor]
    AS [dbo];

