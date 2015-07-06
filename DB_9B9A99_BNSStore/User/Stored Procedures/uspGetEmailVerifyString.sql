-- =============================================
-- Author:		Cosh_
-- Create date: 2014.11.6
-- Description:	Add Translation
-- =============================================
CREATE PROCEDURE [User].uspGetEmailVerifyString
	@EmailAddress nvarchar(254) = NULL,
	@VerifyString varchar(64) = NULL OUTPUT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SET @VerifyString = (SELECT VerifyString FROM [User].Email WHERE EmailAddress = @EmailAddress);
END

GO
GRANT EXECUTE
    ON OBJECT::[User].[uspGetEmailVerifyString] TO [db_executor]
    AS [dbo];

