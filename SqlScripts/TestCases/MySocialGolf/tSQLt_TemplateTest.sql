PRINT '[<Test_Class_Name>].[<Name of Test>]. XX dd/mm/yy'
GO

IF OBJECT_ID('[<Test_Class_Name>].[<Name of Test>]') IS NULL
BEGIN
  EXEC('CREATE PROC [<Test_Class_Name>].[<Name of Test>] AS SELECT 1 AS Test')
END
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [<Test_Class_Name>].[<Name of Test>]
AS
BEGIN
/*
    Description:
      <Name of Test>
    
    History: 
      dd/mm/yy 
        - Created by XX dd/mm/yy

    Run: 
      EXEC tSQLt.Run '<Test_Class_Name>.<Name of Test>';
*/

    -- Assemble: this is where any setup for running the test is done, including mocking objects if necessary

 
    -- Act: execute the code under test like a stored procedure, function or view and capture the results in variables or tables.
 

    -- Assert: Compare the expected and actual values, or call tSQLt.Fail in an IF statement. For a complete list of Assertions, see: http://tsqlt.org/user-guide/assertions/
    EXEC tSQLt.Fail 'TODO:Implement this test.' 
    -- EXEC tSQLt.AssertEqualsTable @Expected = '#Expected', @Actual = 'SubjectClasses' 
    -- EXEC tSQLt.AssertEquals @IsTestSuccessFlag, 1     

END
GO



