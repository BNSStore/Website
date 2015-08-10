CREATE PROCEDURE Permission.uspDoesUserContainPolicy
	@UserID int = NULL,
	@Username varchar(100) = NULL,
	@PolicyID smallint = NULL,
	@PolicyName varchar(100) = NULL,
	@WildCard bit = 1,
	@Contains bit = 0 OUTPUT
AS
BEGIN
	SET NOCOUNT ON;
	
	IF @UserID IS NULL
	BEGIN
		EXEC [User].uspGetUserID @Username = @Username, @UserID = @UserID OUTPUT
	END

	IF @PolicyID IS NULL
	BEGIN
		EXEC [Permission].uspGetPolicyID @PolicyName = @PolicyName, @PolicyID = @PolicyID OUTPUT
	END
	
	--Search from UserPolicy
	IF (SELECT UserID FROM [Permission].[UserPolicy] WHERE UserID = @UserID AND PolicyID = @PolicyID) IS NOT NULL
	BEGIN
		SET @Contains = 1;
	END
	ELSE
	BEGIN
		--Failed to find in UserPolicy. Search with RolePolicy
		IF
		(SELECT TOP 1 (SELECT rp.RoleID FROM [Permission].[RolePolicy] rp WHERE rp.PolicyID = 2)
		WHERE EXISTS
		(SELECT ur.RoleID FROM [Permission].UserRole ur WHERE ur.UserID = 36)) IS NOT NULL
		BEGIN
			SET @Contains = 1;
		END
		ELSE IF @WildCard = 1 AND @PolicyName != '*'
		BEGIN
			--Failed to find in RolePolicy. Search with WildCard
			IF @PolicyName IS NULL
			BEGIN
				EXEC [Permission].uspGetPolicyID  @PolicyID = @PolicyID, @PolicyName = @PolicyName OUTPUT
			END
			SET @PolicyName = REPLACE(@PolicyName, '.*', '')

			DECLARE @Part varchar(100) = NULL
			DECLARE @Index tinyint = 0
			SET @Index = LEN(@PolicyName) - (CHARINDEX('.', REVERSE(@PolicyName)) - 2)
			IF @Index > LEN(@PolicyName)
			BEGIN
				SET @Part = '*'
			END
			ELSE
			BEGIN
				SET @Part = SUBSTRING(@PolicyName, 0, @Index) + '.*'
			END

			EXEC [Permission].[uspDoesUserContainPolicy] @UserID = @UserID, @PolicyName = @Part, @Contains = @Contains OUTPUT;
		END
		ELSE
		BEGIN
			--Can't search with WildCard or Failed to find with WildCard
			SET @Contains = 0
		END
	END

END