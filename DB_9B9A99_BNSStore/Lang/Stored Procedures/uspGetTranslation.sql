CREATE PROCEDURE [Lang].[uspGetTranslation]
	@LangID tinyint = NULL,
	@LangName varchar(100) = NULL,
	@ProviderID int = NULL,
	@ProviderName varchar(100) = NULL,
	@Keywords varchar(MAX) = NULL,
	@Context nvarchar(MAX) = NULL OUTPUT
AS
BEGIN
	SET NOCOUNT ON;
	IF @LangID IS NULL
	BEGIN
		EXEC Lang.uspGetLangID @LangName = @LangName, @LangID = @LangID OUTPUT
	END
	IF @ProviderID IS NULL
	BEGIN
		EXEC Svc.uspGetProviderID @ProviderName = @ProviderName, @ProviderID = @ProviderID OUTPUT
	END
	DECLARE @Index int = NULL,
	@Part nvarchar(MAX) = ''
	WHILE 1=1
	BEGIN
		IF LEN(@Keywords) = 0
		BEGIN
			BREAK
		END

		SET @Index = PATINDEX('%|%',@Keywords)
		IF @Index = 0
		BEGIN
			SET @Index = LEN(@Keywords) + 1
		END

		SET @Part = @Part + SUBSTRING(@Keywords, 0, @Index)
		
		SET @Keywords = SUBSTRING(@Keywords, @Index + 1, Len(@Keywords) - @Index + 1)

		IF CharIndex('\', @Part) = Len(@Part)
		BEGIN
			SET @Part = SUBSTRING(@Part,0,Len(@Part)) + '|'
			CONTINUE
		END

		SET @Part = (SELECT Context FROM Lang.Translation WHERE [LangID] = @LangID AND ProviderID = @ProviderID AND Keyword = @Part)
		IF @Part IS NULL
		BEGIN
			SET @Part = '{NULL}'
		END
		SET @Part = REPLACE(@Part, '|', '\|')
		
		IF @Context IS NULL
		BEGIN
			SET @Context = @Part
		END
		ELSE
		BEGIN
		SET @Context = @Context + '|' + @Part
		END
		--Reset Part
		SET @Part = ''
	END
END
