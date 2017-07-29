PRINT '[ut_UserSave].[SetUp]. SW0 18/11/2015'
GO

IF OBJECT_ID('[ut_UserSave].Setup') IS NULL
BEGIN
  EXEC('CREATE PROC [ut_UserSave].Setup AS SELECT 1 AS Test')
END
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [ut_UserSave].Setup
AS
BEGIN
/*
    Description:
      Have to set up any fake tables needed to test the UserSave Proc

    History: 
      18/11/15
      - Created by SW0 18/11/2015

    Register:
      exec spSystemObjectProtectTriggerEnableDisableAll 0;
      EXEC tSQLt.NewTestClass 'ut_UserSave';

    Run:
      EXEC tSQLt.Run 'ut_UserSave';
*/
    
    -- Assemble: this is where all common setups for running all the tests for this test calss (schema) is done, including mocking objects if necessary
  EXEC tSQLt.FakeTable 'User', @identity = 1

END
GO



