PRINT '[ut_UserSave].[test that a new User will return a NewUserID and SetupMessage]. SW 18/11/15'
GO

IF OBJECT_ID('[ut_UserSave].[test that a new User will return a NewUserID and SetupMessage]') IS NULL
BEGIN
  EXEC('CREATE PROC [ut_UserSave].[test that a new User will return a NewUserID and SetupMessage] AS SELECT 1 AS Test')
END
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [ut_UserSave].[test that a new User will return a NewUserID and SetupMessage]
AS
BEGIN
/*
    Description:
      test that a new User will return a NewUserID and SetupMessage
    
    History: 
      18/11/15 
        - Created by SW 18/11/15

    Run: 
      EXEC tSQLt.Run 'ut_UserSave.test that a new User will return a NewUserID and SetupMessage';
*/

  DECLARE 
    @IsPassed BIT = 0
    , @newid INT
    , @msg VARCHAR(1000)
  
  -- Assemble: a Mock User datarow - CTRL T then this helper: SELECT ', ' + c.name + ' ' + UPPER(t.Name) + CASE WHEN t.Name IN ('char', 'varchar') THEN '(' + CONVERT(VARCHAR, c.max_length) + ')' ELSE '' END FROM sys.columns c LEFT JOIN sys.types t ON c.system_type_id = t.system_type_id WHERE c.object_id = object_id(N'dbo.SubjectClasses') AND t.Name <> 'SYSNAME' ORDER BY c.column_id

  -- Act: Run the Core Proc that is being tested
  EXEC UserSave
    @NewUserID = @newid OUTPUT
  , @UserID = null 
  , @Surname = 'Welsh' 
  , @FirstName = 'Simon' 
  , @Email = 'sjw@gmail.com' 
  , @Mobile = '0400925025'
  , @CurrentHandicap = 12 
  , @SubmitMessage = @msg OUTPUT

  -- Assert
  IF (@newid = 1)
      AND LEN(@msg) = 28
      AND (SUBSTRING(@msg, 0, 9) = 'Saved at ')
    SET @IsPassed = 1
  -- debug:  select @newid, @msg, len(@msg)

  EXEC tSQLt.AssertEquals @IsPassed, 1

END
GO



