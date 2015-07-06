-- =============================================
-- Author:		Cosh_
-- Create date: 2014.11.6
-- Description:	Add Translation

-- =============================================
CREATE PROCEDURE Store.uspGetGroupID
	@GroupName varchar(50) = NULL,
	@GroupID tinyint = NULL OUTPUT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here

	SET @GroupID = (SELECT GroupID FROM Store.[Group] WHERE GroupName = @GroupName)

END
