PRINT '[<Test_Class_Name>].[SetUp]. XX dd/mm/yyyy'
GO

IF OBJECT_ID('[<Test_Class_Name>].Setup') IS NULL
BEGIN
  EXEC('CREATE PROC [<Test_Class_Name>].Setup AS SELECT 1 AS Test')
END
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [<Test_Class_Name>].Setup
AS
BEGIN
/*
    Description:
      <setup Description>

    History: 
      dd/mm/yy
      - Created by XX dd/mm/yyyy

    Register:
      exec spSystemObjectProtectTriggerEnableDisableAll 0;
      EXEC tSQLt.NewTestClass '<Test_Class_Name>';

    Run:
      EXEC tSQLt.Run '<Test_Class_Name>';
*/
    
    -- Assemble: this is where all common setups for running all the tests for this test calss (schema) is done, including mocking objects if necessary

  PRINT 'This is where you set up the Fake data needed to run your test e.g. EXEC tSQLt.FakeTable luSubjectClassCategory'

END
GO



