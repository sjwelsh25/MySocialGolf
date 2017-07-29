PRINT '[ut_UserSave].[test that an updated User will return null NewUserID and an updated SetupMessage]. SW 18/11/15'
GO

IF OBJECT_ID('[ut_UserSave].[test that an updated User will return null NewUserID and an updated SetupMessage]') IS NULL
BEGIN
  EXEC('CREATE PROC [ut_UserSave].[test that an updated User will return null NewUserID and an updated SetupMessage] AS SELECT 1 AS Test')
END
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [ut_UserSave].[test that an updated User will return null NewUserID and an updated SetupMessage]
AS
BEGIN
/*
    Description:
      test that an updated User will return null NewUserID and an updated SetupMessage
    
    History: 
      18/11/15 
        - Created by SW 18/11/15

    Run: 
      EXEC tSQLt.Run 'ut_UserSave.test that an updated User will return null NewUserID and an updated SetupMessage';
*/

DECLARE 
    @IsPassed BIT = 0
    , @newid INT
    , @msg VARCHAR(1000)
  
  -- Assemble: a Mock User datarow - CTRL T then this helper: SELECT ', ' + c.name + ' ' + UPPER(t.Name) + CASE WHEN t.Name IN ('char', 'varchar') THEN '(' + CONVERT(VARCHAR, c.max_length) + ')' ELSE '' END FROM sys.columns c LEFT JOIN sys.types t ON c.system_type_id = t.system_type_id WHERE c.object_id = object_id(N'dbo.SubjectClasses') AND t.Name <> 'SYSNAME' ORDER BY c.column_id

  -- Act: Run the Core Proc that is being tested
  EXEC UserSave
    @NewUserID = @newid OUTPUT
  , @UserID = 1 -- update has this PK value set
  , @Surname = 'Welsh' 
  , @FirstName = 'Simon' 
  , @Email = 'sjw@gmail.com' 
  , @Mobile = '0400925025'
  , @CurrentHandicap = 12 
  , @SubmitMessage = @msg OUTPUT

  -- Assert
  IF (@newid is null) -- return no NewID
      AND LEN(@msg) = 30
      AND (SUBSTRING(@msg, 0, 11) = 'Updated at ')
    SET @IsPassed = 1
  -- debug:  select @newid, @msg, len(@msg), SUBSTRING(@msg, 0, 11)

  EXEC tSQLt.AssertEquals @IsPassed, 1
 

END
GO



