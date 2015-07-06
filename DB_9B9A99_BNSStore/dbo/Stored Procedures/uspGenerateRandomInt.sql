-- =============================================
-- Author:		Cosh_
-- Create date: 2014.9.13
-- Description:	Create a random int with max and min
-- =============================================
CREATE PROCEDURE [dbo].[uspGenerateRandomInt]
	@Max int,
	@Min int,
	@RandomInt int OUTPUT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SET @RandomInt = CONVERT(int, RAND(CHECKSUM(NEWID())) * @Max + @Min)
END
