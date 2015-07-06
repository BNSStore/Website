-- =============================================
-- Author:		Cosh_
-- Create date: 2014.9.13
-- Description:	Create a random int with max and min
-- =============================================
CREATE PROCEDURE [dbo].[uspGenerateRandomString]
	@Length smallint = NULL,
	@List varchar(MAX) = NULL,
	@LetterCase char(1) = NULL,
	@Numbers bit = NULL,
	@RandomString varchar(MAX) = NULL OUTPUT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	DECLARE @LoopCount smallint = 0,
	@ListLength int,
	@RandomInt int

	IF @List IS NULL
	BEGIN
		IF @LetterCase IS NULL
		BEGIN
			SET @List = 'abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ';
		END
		ELSE IF @LetterCase = 'L'
		BEGIN
			SET @List = 'abcdefghijklmnopqrstuvwxyz';
		END
		ELSE IF @LetterCase = 'U'
		BEGIN
			SET @List = 'ABCDEFGHIJKLMNOPQRSTUVWXYZ';
		END

		IF @Numbers IS NOT NULL AND @Numbers = 1
		BEGIN
			SET @List = @List + '0123456789';
		END
	END

    SET @ListLength = LEN(@List)
	SET @RandomString = ''
	WHILE @LoopCount < @Length
	BEGIN
		EXEC dbo.uspGenerateRandomInt @Max = @ListLength, @Min = 1, @RandomInt = @RandomInt OUTPUT
		SET @RandomString = @RandomString + SUBSTRING(@List, @RandomInt, 1)
		SET @LoopCount = @LoopCount + 1
	END
END
