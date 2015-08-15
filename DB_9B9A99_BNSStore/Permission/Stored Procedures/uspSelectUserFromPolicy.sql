CREATE PROCEDURE [Permission].[uspSelectUserFromPolicy]
	@PolicyID smallint = NULL,
	@PolicyName varchar(100) = NULL,
	@WildCard bit = 1
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @Output TABLE(UserID int NOT NULL);

	PD:

		IF @PolicyID IS NULL
		BEGIN
			EXEC [Permission].uspGetPolicyID @PolicyName = @PolicyName, @PolicyID = @PolicyID OUTPUT
		END

		--Declare Tables
		DECLARE @UserPolicy TABLE(UserID int NOT NULL);
		DECLARE @RolePolicy TABLE(RoleID smallint NOT NULL);
		DECLARE @UserRole TABLE(UserID int NOT NULL);

		--Insert UserIDs to UserPolicy
		INSERT INTO @UserPolicy SELECT UserID FROM [Permission].[UserPolicy] WHERE PolicyID = @PolicyID
		--Insert RoleIDs to RolePolicy
		INSERT INTO @RolePolicy SELECT RoleID FROM [Permission].[RolePolicy] WHERE PolicyID = @PolicyID

		--Loop through RoleIDs to add UserIDs to  UserRole
		DECLARE @Cursor CURSOR;
		DECLARE @RoleID smallint;
		BEGIN
			SET @Cursor = CURSOR FOR SELECT RoleID FROM @RolePolicy  
			OPEN @Cursor 
			FETCH NEXT FROM @Cursor 
			INTO @RoleID
		
			WHILE @@FETCH_STATUS = 0
			BEGIN
				
				FETCH NEXT FROM @Cursor 
				INTO @RoleID 
			END; 

			INSERT INTO @UserRole SELECT UserID FROM [Permission].[UserRole] WHERE RoleID = @RoleID

			CLOSE @Cursor ;
			DEALLOCATE @Cursor;
		END;

		--Declare TMP table
		DECLARE @TMP TABLE(UserID int NOT NULL);
		--Move data from Output to TMP
		INSERT INTO @TMP SELECT UserID FROM @Output
		--Clean Output
		DELETE FROM @Output

		--Insert UserIDs from TMP, UserPolicy and UserRole to Output 
		INSERT INTO @Output SELECT Distinct UserID FROM (SELECT tmp.UserID FROM @TMP tmp UNION SELECT up.UserID FROM @UserPolicy up UNION SELECT ur.UserID FROM @UserRole ur) outupur
		
		--Loop back with wildcard policy name
		IF @WildCard = 1 AND @PolicyName != '*'
		BEGIN
			IF @PolicyName IS NULL
			BEGIN
				EXEC [Permission].[uspGetPolicyName] @PolicyID = @PolicyID, @PolicyName = @PolicyName OUTPUT;
			END;
			SET @PolicyName = REPLACE(@PolicyName, '.*', '');
			DECLARE @Part varchar(100) = NULL;
			DECLARE @Index tinyint = 0;
			SET @Index = LEN(@PolicyName) - (CHARINDEX('.', REVERSE(@PolicyName)) - 1);
			IF @Index > LEN(@PolicyName)
			BEGIN
				SET @Part = '*';
			END;
			ELSE
			BEGIN
				SET @Part = SUBSTRING(@PolicyName, 0, @Index) + '.*';
			END;

			SET @PolicyName = @Part;
			SET @PolicyID = NULL;
			--Jump back to Start Getting UserIDs
			GOTO PD;
		END
		ELSE
		BEGIN
			SELECT UserID FROM @Output
		END
END