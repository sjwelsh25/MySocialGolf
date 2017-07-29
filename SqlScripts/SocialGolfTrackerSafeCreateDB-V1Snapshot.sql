USE MySocialGolf;
GO

PRINT 'CHECK TABLE [dbo].[TestAPI]'
GO

/* History:
15/6/17  
  Created to store the TestAPI records - used to test the RESTFul services 
*/

IF OBJECT_ID ('dbo.TestAPI') IS NULL
BEGIN 
CREATE TABLE [dbo].[TestAPI] 
    (
      [TestAPIID] INT IDENTITY(1,1) NOT NULL
      , [TestAPIGUID] UNIQUEIDENTIFIER NOT NULL CONSTRAINT DF_TestAPI_TestAPIGUID DEFAULT NEWID()
      , [TestName] NVARCHAR(100) NULL
      , [TestInputJSON] NVARCHAR(MAX) NULL
      , [Status] NVARCHAR(300) NULL
      , [CreatedByID] INT NULL
      , [CreatedDateTime] DATETIME NULL
      , [ModifiedByID] INT NULL
      , [ModifiedDateTime] DATETIME NULL
      CONSTRAINT [PK_TestAPI] PRIMARY KEY CLUSTERED (
        [TestAPIID]
      )
    )
END 
GO

PRINT 'CHECK TABLE [dbo].[TestAPILog]'
GO

/* History:
15/6/17  
  Created to store the TestAPILog records - used to test the RESTFul services 
*/

IF OBJECT_ID ('dbo.TestAPILog') IS NULL
BEGIN 
CREATE TABLE [dbo].[TestAPILog] 
    (
      [TestAPILogID] INT IDENTITY(1,1) NOT NULL,
      [TestAPILogGUID] UNIQUEIDENTIFIER NOT NULL CONSTRAINT DF_TestAPILog_TestAPILogGUID DEFAULT NEWID(),
      [TestAPIID] INT NOT NULL,
      [CreatedByID] INT NULL,
      [CreatedDateTime] DATETIME NULL,
      Response VARCHAR(MAX) NULL,
      CONSTRAINT [PK_TestAPILog] PRIMARY KEY CLUSTERED (
        [TestAPILogID]
      )
    )
END 
GO


PRINT 'CHECK TABLE [dbo].[User]'
GO

/* History:
28/07/2014  
  Created to store user details e.g. think My account
*/

IF OBJECT_ID ('dbo.User') IS NULL
BEGIN 
CREATE TABLE [dbo].[User] 
    (
      [UserID] INT IDENTITY(1,1) NOT NULL
      , [UserGUID] UNIQUEIDENTIFIER NOT NULL CONSTRAINT DF_User_UserGUID DEFAULT NEWID()
      , [Surname] NVARCHAR(100) NULL
      , [FirstName] NVARCHAR(100) NULL
      , [Email] NVARCHAR(300) NULL
      , [Mobile] NVARCHAR(300) NULL
      , [CurrentHandicap] DECIMAL(18,2) NULL
      , [CurrentHandicapCalcdOn] DATETIME NULL
      , [CreatedByID] INT NULL
      , [CreatedDateTime] DATETIME NULL
      , [ModifiedByID] INT NULL
      , [ModifiedDateTime] DATETIME NULL
      , StatusID INT NULL
      CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED (
        [UserID]
      )
    )
END 
GO

PRINT 'CHECK TABLE [dbo].[UserStatus]'
GO

/* History:
28/07/2014  
  Created to store user's Status e.g. Active, Deleted, 
*/

IF OBJECT_ID ('dbo.UserStatus') IS NULL
BEGIN 
CREATE TABLE [dbo].[UserStatus] 
    (
      [UserStatusID] INT IDENTITY(1,1) NOT NULL
      , [Description] VARCHAR(100) -- foreign key to user
      , [CreatedByID] INT NULL
      , [CreatedDateTime] DATETIME NULL
      , [ModifiedByID] INT NULL
      , [ModifiedDateTime] DATETIME NULL
      CONSTRAINT [PK_UserStatus] PRIMARY KEY CLUSTERED (
        [UserStatusID]
      )
    )
END 
GO

PRINT 'CHECK TABLE [dbo].[UserGolfRound]'
GO

/* History:
28/07/2014  
  Created to store user's Rounds details e.g. enter the score
*/

IF OBJECT_ID ('dbo.UserGolfRound') IS NULL
BEGIN 
CREATE TABLE [dbo].[UserGolfRound] 
    (
      [UserGolfRoundID] INT IDENTITY(1,1) NOT NULL
      , [UserID] INT -- foreign key to user
      , [GolfCourseName] NVARCHAR(1000) NOT NULL
      , [GolfRoundDate] DATETIME NULL
      , StableFordScore INT NULL
      , StrokeScore INT NULL
      , HandicapThatDay INT NULL
      , CourseParThatDay INT NULL
      , [CreatedByID] INT NULL
      , [CreatedDateTime] DATETIME NULL
      , [ModifiedByID] INT NULL
      , [ModifiedDateTime] DATETIME NULL
      CONSTRAINT [PK_UserGolfRound] PRIMARY KEY CLUSTERED (
        [UserGolfRoundID]
      )
    )
END 
GO

PRINT 'CHECK TABLE [dbo].[UserGroup]'
GO

/* History:
28/07/2014  
  Created to store user's groups (social club group) details
*/

IF OBJECT_ID ('dbo.UserGroup') IS NULL
BEGIN 
CREATE TABLE [dbo].[UserGroup] 
    (
      [UserGroupID] INT IDENTITY(1,1) NOT NULL
      , [UserID] INT -- foreign key to user
      , [GroupName] NVARCHAR(300) NULL
      , [CreatedByID] INT NULL
      , [CreatedDateTime] DATETIME NULL
      , [ModifiedByID] INT NULL
      , [ModifiedDateTime] DATETIME NULL
      CONSTRAINT [PK_UserGroup] PRIMARY KEY CLUSTERED (
        [UserGroupID]
      )
    )
END 
GO


PRINT 'CHECK TABLE [dbo].[GolfCourse]'
GO

/* History:
28/07/2014  
  Created to store the Golf Courses where the rounds are played
*/

IF OBJECT_ID ('dbo.GolfCourse') IS NULL
BEGIN 
CREATE TABLE [dbo].[GolfCourse] 
    (
      [GolfCourseID] INT IDENTITY(1,1) NOT NULL
      , [CourseName] NVARCHAR(1000) NULL
      , [Address] NVARCHAR(300) NULL
      , [CoursePar] INT NULL
      , [CreatedByID] INT NULL
      , [CreatedDateTime] DATETIME NULL
      , [ModifiedByID] INT NULL
      , [ModifiedDateTime] DATETIME NULL
      CONSTRAINT [PK_GolfCourse] PRIMARY KEY CLUSTERED (
        [GolfCourseID]
      )
    )
END 
GO

PRINT 'CREATE FUNCTION fnGetCurrentUserID. SW 16/11/15'
GO

IF OBJECT_ID('dbo.fnGetCurrentUserID') IS NULL
  EXEC ('CREATE FUNCTION dbo.fnGetCurrentUserID() RETURNS INT BEGIN RETURN (SELECT 0) END')
GO

ALTER FUNCTION dbo.fnGetCurrentUserID()
RETURNS INT
BEGIN 
  RETURN (SELECT 53)
END
GO

PRINT 'CREATE FUNCTION fnFormatDate'
GO

IF OBJECT_ID('dbo.fnFormatDate') IS NULL
BEGIN
EXEC('CREATE FUNCTION fnFormatDate() RETURNS INT BEGIN RETURN 0 END')
END
GO
 
ALTER FUNCTION [dbo].[fnFormatDate] 
  (@d as sql_variant, @fOUT NVARCHAR(100))
RETURNS NVARCHAR(100) AS 
BEGIN
/*
  HISTORY
  9/11/07 SL #5828 - from SQLMag article #96608
 
  Usage example - 
    select dbo.fnFormatDate(BirthDate, 'dd/mm/yyyy') from Community
 
  Options for @fOUT
    mm = month
    dd = day
    yyyy or yy = year
    hh = hours 
    nn = minutes 
    ss = seconds 
    ms = milliseconds 
    mmmm = long month name (e.g., January) 
    mmm = short (three characters) month name (e.g., Jan) 
    wdd = long day-of-the-week name (e.g., Monday) 
    wd = short (three characters) day-of-the-week name (e.g., Mon) 
    AM/PM = AM or PM 
    am/pm = am or pm 
    A/P = A or P 
    a/p = a or p 
*/
 
  DECLARE @rv as NVARCHAR(100),@d1 datetime,@dc NVARCHAR(100)
  DECLARE @s NVARCHAR(100),@n int
  SET @dc=CONVERT(NVARCHAR(100),@d,109)
  IF ISDATE(@dc)=0 
    RETURN @dc
  SET @d1=CONVERT(datetime,@dc)
 
  DECLARE @aUpper tinyint,@aLower tinyint
  DECLARE @a NVARCHAR(2),@p NVARCHAR(2)
  SET @aUpper=ASCII('A')
  SET @aLower=ASCII('a')
  SET @a='a'
  SET @p='p'
  SET @n=CHARINDEX('a/p',@fOUT,1)
  IF @n>0
  BEGIN
    IF ASCII(SUBSTRING(@fOUT,@n,1))=@aUpper
    BEGIN
      SET @a='A'
      SET @p='P'
    END
  END
  ELSE
  BEGIN
    SET @a='am'
    SET @p='pm'
    SET @n=CHARINDEX('am/pm',@fOUT,1)
    IF @n>0
    BEGIN
      IF ASCII(SUBSTRING(@fOUT,@n,1))=@aUpper
      BEGIN
        SET @a='AM'
        SET @p='PM'
      END
    END
  END
 
  SET @rv=@fOUT
  SET @s=CASE WHEN CHARINDEX('a/p',@rv,1)=0 and
    CHARINDEX('am/pm',@rv,1)=0 
    THEN CONVERT(NVARCHAR(2),DATEPART(hh,@d1))
    WHEN DATEPART(hh,@d1) between 1 and 12 THEN
      CONVERT(NVARCHAR(2),DATEPART(hh,@d1))
      WHEN DATEPART(hh,@d1)=0 THEN '12'
      ELSE CONVERT(NVARCHAR(2),DATEPART(hh,@d1)-12)
      END
  SET @rv=CASE WHEN CHARINDEX('a/p',@rv,1)>0 THEN
    CASE WHEN DATEPART(hh,@d1)<12 THEN REPLACE(@rv,'a/p',@a)
    ELSE REPLACE(@rv,'a/p',@p) END
    WHEN CHARINDEX('am/pm',@rv,1)>0 THEN
    CASE WHEN DATEPART(hh,@d1)<12 THEN REPLACE(@rv,'am/pm',@a)
    ELSE REPLACE(@rv,'am/pm',@p) END
    ELSE @rv
    END
  SET @rv=REPLACE(@rv,'hh',CASE WHEN len(@s)=2 THEN @s ELSE '0'+@s END)
  SET @s=CONVERT(NVARCHAR(2),DATEPART(n,@d1))
  SET @rv=REPLACE(@rv,'nn',CASE WHEN len(@s)=2 THEN @s ELSE '0'+@s END)
  SET @s=CONVERT(NVARCHAR(2),DATEPART(s,@d1))
  SET @rv=REPLACE(@rv,'ss',CASE WHEN len(@s)=2 THEN @s ELSE '0'+@s END)
  SET @s=CONVERT(NVARCHAR(3),DATEPART(ms,@d1))
  SET @rv=REPLACE(@rv,'ms',CASE WHEN len(@s)=3 THEN
    @s WHEN len(@s)=2 THEN '0'+@s ELSE '00'+@s END)
  SET @s=CONVERT(NVARCHAR(4),DATEPART(yyyy,@d1))
  SET @rv=REPLACE(@rv,'yyyy',@s)
  SET @s=RIGHT(@s,2)
  SET @rv=REPLACE(@rv,'yy',@s)
  SET @s=CONVERT(NVARCHAR(20),DATENAME(mm,@d1))
  SET @rv=REPLACE(@rv,'mmmm',@s)
  SET @s=LEFT(@s,3)
  SET @rv=REPLACE(@rv,'mmm',@s)
  SET @s=CONVERT(NVARCHAR(20),DATENAME(dw,@d1))
  SET @rv=REPLACE(@rv,'wdd',@s)
  SET @s=LEFT(@s,3)
  SET @rv=REPLACE(@rv,'wd',@s)
  SET @s=CONVERT(NVARCHAR(2),DATEPART(mm,@d1))
  SET @rv=REPLACE(@rv,'mm',CASE WHEN len(@s)=2 THEN @s ELSE '0'+@s END)
  SET @s=CONVERT(NVARCHAR(2),DATEPART(dd,@d1))
  SET @rv=REPLACE(@rv,'dd',CASE WHEN len(@s)=2 THEN @s ELSE '0'+@s END)
  RETURN @rv
END
GO

PRINT 'ALTER PROCEDURE dbo.TestAPIInsert. SW 15/6/17'
GO

IF OBJECT_ID('dbo.TestAPIInsert') IS NULL
  EXEC('CREATE PROC dbo.TestAPIInsert AS')
GO

ALTER PROCEDURE [dbo].TestAPIInsert
  @NewTestAPIID INT = NULL OUTPUT,
  @TestAPIGUID UNIQUEIDENTIFIER = NULL,
  @TestName NVARCHAR(100) = NULL,
  @TestInputJSON NVARCHAR(MAX) = NULL,
  @Status NVARCHAR(300) = NULL,
  @SubmitMessage NVARCHAR(1000) = '' OUTPUT
AS
BEGIN
  INSERT INTO [TestAPI]
    (
      TestAPIGUID,
      TestName,
      TestInputJSON,
      [Status],
      CreatedByID,
      CreatedDateTime
    )
  SELECT 
      TestAPIGUID = ISNULL(@TestAPIGUID, NEWID()),
      TestName = @TestName,
      TestInputJSON = @TestInputJSON,
      [Status] = @Status,
      CreatedByID = dbo.fnGetCurrentUserID(),
      CreatedDateTime = GETDATE()

  SET @NewTestAPIID = SCOPE_IDENTITY()
  SET @SubmitMessage = 'Saved at ' + dbo.fnFormatDate(GETDATE(), 'dd MMM yyyy hh:nnam/pm')

END
GO

PRINT 'ALTER PROCEDURE dbo.TestAPIList. SW 08/11/15'
GO

IF OBJECT_ID('dbo.TestAPIList') IS NULL
  EXEC('CREATE PROC dbo.TestAPIList AS')
GO

ALTER PROCEDURE [dbo].TestAPIList
  @TestAPIID INT = NULL OUTPUT
AS
BEGIN
  SET @TestAPIID = NULLIF(@TestAPIID, 0)

  SELECT 
    TestAPIID,
    TestAPIGUID,
    TestName,
    TestInputJSON,
    [Status],
    CreatedByID,
    CreatedDateTime,
    LastLogDateTime = (SELECT TOP(1) CreatedDateTime FROM TestAPILog tal WHERE tal.TestAPIID = t.TestAPIID ORDER BY CreatedDateTime DESC),
    LastLogResponse = (SELECT TOP(1) Response FROM TestAPILog tal WHERE tal.TestAPIID = t.TestAPIID ORDER BY CreatedDateTime DESC)
  FROM TestAPI t
    --LEFT JOIN TestAPILog tal ON
    --  tal.TestAPIID = t.TestAPIID
  WHERE TestAPIID = ISNULL(@TestAPIID, TestAPIID)
  
END
-- EXEC TestAPIList @TestAPIID = 2
GO

IF OBJECT_ID('dbo.TestAPILogInsert') IS NULL
  EXEC('CREATE PROC dbo.TestAPILogInsert AS')
GO

ALTER PROCEDURE [dbo].TestAPILogInsert
  @TestAPIID INT,
  @Response NVARCHAR(MAX) = NULL,
  @NewTestAPILogID INT = NULL OUTPUT 
AS
BEGIN
  INSERT INTO [TestAPILog]
    (
      TestAPIID,
      Response,
      CreatedByID,
      CreatedDateTime
    )
  SELECT 
      TestAPIID = @TestAPIID,
      Response = @Response,
      CreatedByID = dbo.fnGetCurrentUserID(),
      CreatedDateTime = GETDATE()

  SET @NewTestAPILogID = SCOPE_IDENTITY()

END
  -- EXEC TestAPILogInsert @TestAPIID = 1, @Response = 'Success 1'
GO


PRINT 'ALTER PROCEDURE dbo.UserUpdate. SW 08/11/15'
GO

IF OBJECT_ID('dbo.UserUpdate') IS NULL
  EXEC('CREATE PROC dbo.UserUpdate AS')
GO

ALTER PROCEDURE [dbo].UserUpdate
  @UserID INT 
  , @Surname NVARCHAR(100) 
  , @FirstName NVARCHAR(100) 
  , @Email NVARCHAR(300) 
  , @Mobile NVARCHAR(300)
  , @CurrentHandicap DECIMAL(18,2) = NULL 
  , @SubmitMessage NVARCHAR(1000) = '' OUTPUT
  , @UserGUID UNIQUEIDENTIFIER = NULL
AS
BEGIN
  UPDATE [User]
  SET
    Surname = @Surname 
    , FirstName = @FirstName
    , Email = @Email
    , Mobile = @Mobile
    , CurrentHandicap = @CurrentHandicap 
    , ModifiedByID = dbo.fnGetCurrentUserID()
    , ModifiedDateTime = GETDATE()
  WHERE UserID = @UserID

  SET @SubmitMessage = 'Updated at ' + dbo.fnFormatDate(GETDATE(), 'dd MMM yyyy hh:nnam/pm')

END
GO

PRINT 'ALTER PROCEDURE dbo.UserInsert. SW 08/11/15'
GO

IF OBJECT_ID('dbo.UserInsert') IS NULL
  EXEC('CREATE PROC dbo.UserInsert AS')
GO

ALTER PROCEDURE [dbo].UserInsert
  @NewUserID INT = '' OUTPUT
  , @Surname NVARCHAR(100) 
  , @FirstName NVARCHAR(100) 
  , @Email NVARCHAR(300) 
  , @Mobile NVARCHAR(300)
  , @CurrentHandicap DECIMAL(18,2) = NULL 
  , @SubmitMessage NVARCHAR(1000) = '' OUTPUT
  , @UserGUID UNIQUEIDENTIFIER = NULL
AS
BEGIN
  INSERT INTO [User]
    (
      Surname  
      , FirstName 
      , Email 
      , Mobile 
      , CurrentHandicap 
      , CreatedByID 
      , CreatedDateTime
      , StatusID
      , UserGUID
    )
  SELECT 
    Surname = @Surname 
    , FirstName = @FirstName
    , Email = @Email
    , Mobile = @Mobile
    , CurrentHandicap = @CurrentHandicap 
    , CreatedByID = dbo.fnGetCurrentUserID()
    , CreatedDateTime = GETDATE()
    , StatusID = 1 -- 'Active'
    , UserGUID = ISNULL(@UserGUID, NEWID())

  SET @NewUserID = SCOPE_IDENTITY()
  SET @SubmitMessage = 'Saved at ' + dbo.fnFormatDate(GETDATE(), 'dd MMM yyyy hh:nnam/pm')

END
GO

PRINT 'ALTER PROCEDURE dbo.UserList. SW 08/11/15'
GO

IF OBJECT_ID('dbo.UserList') IS NULL
  EXEC('CREATE PROC dbo.UserList AS')
GO

ALTER PROCEDURE [dbo].UserList
  @UserID INT = NULL OUTPUT
AS
BEGIN
  SELECT 
    Surname          
    , FirstName    
    , Email        
    , Mobile           
    , CurrentHandicap  
    , CurrentHandicapCalcdOn 
    , CreatedByID 
    , CreatedDateTime 
    , StatusID 
    , FullName = Surname 
        + CASE 
            WHEN ISNULL(FirstName, '') <> '' THEN ', ' + FirstName
            ELSE ''
          END
    , LastGolfRoundDate = 
          (
            SELECT TOP 1 GolfRoundDate 
            FROM UserGolfRound ugr
            WHERE UserID = u.UserID
            ORDER BY GolfRoundDate DESC
          )
  FROM [User] u
  WHERE UserID = ISNULL(@UserID, UserID)
  
END
-- EXEC UserList
GO

PRINT 'ALTER PROCEDURE dbo.UserGolfRoundInsert. SW 08/11/15'
GO

IF OBJECT_ID('dbo.UserGolfRoundInsert') IS NULL
  EXEC('CREATE PROC dbo.UserGolfRoundInsert AS')
GO

ALTER PROCEDURE [dbo].UserGolfRoundInsert
  @NewUserGolfRoundID INT = NULL OUTPUT
  , @UserID INT -- foreign key to user
  , @GolfCourseName NVARCHAR(1000)
  , @GolfRoundDate DATETIME
  , @StableFordScore INT 
  , @StrokeScore INT 
  , @HandicapThatDay INT 
  , @CourseParThatDay INT 
  , @SubmitMessage NVARCHAR(1000) = '' OUTPUT
AS
BEGIN
  INSERT INTO [UserGolfRound]
    (
      UserID 
      , GolfCourseName 
      , GolfRoundDate
      , StableFordScore 
      , StrokeScore 
      , HandicapThatDay 
      , CourseParThatDay
      , CreatedByID
      , CreatedDateTime
    )
  SELECT 
    UserID             = @UserID
    , GolfCourseName   = @GolfCourseName
    , GolfRoundDate    = ISNULL(@GolfRoundDate, '1 Jan 1899')
    , StableFordScore  = @StableFordScore
    , StrokeScore      = @StrokeScore 
    , HandicapThatDay  = @HandicapThatDay
    , CourseParThatDay = @CourseParThatDay
    , CreatedByID      = dbo.fnGetCurrentUserID() 
    , CreatedDateTime  = GETDATE()
                       
  SET @NewUserGolfRoundID = SCOPE_IDENTITY()
  SET @SubmitMessage = 'Submitted at ' + dbo.fnFormatDate(GETDATE(), 'dd MMM yyyy hh:nnam/pm')

END
GO

PRINT 'ALTER PROCEDURE dbo.UserGolfRoundList. SW 08/11/15'
GO

IF OBJECT_ID('dbo.UserGolfRoundList') IS NULL
  EXEC('CREATE PROC dbo.UserGolfRoundList AS')
GO

ALTER PROCEDURE [dbo].UserGolfRoundList
AS
BEGIN
  SELECT 
    *
  FROM [UserGolfRound]
  
END
-- EXEC dbo.UserGolfRoundList
GO
