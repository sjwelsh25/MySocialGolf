USE MySocialGolf;
GO

PRINT 'CHECK TABLE [dbo].[TestApi]'
GO

/* History:
15/6/17  
  Created to store the TestApi records - used to test the RESTFul services 
*/

IF OBJECT_Id ('dbo.TestApi') IS NULL
BEGIN 
CREATE TABLE [dbo].[TestApi] 
    (
      [TestApiId]         INT IdENTITY(1,1) NOT NULL,
      [TestApiGuid]       UNIQUEIdENTIFIER NOT NULL CONSTRAINT DF_TestApi_TestApiGuid DEFAULT NEWId(),
      [TestName]          NVARCHAR(100) NULL,
      TestUrl             NVARCHAR(500) NULL,
      [TestInputJson]     NVARCHAR(MAX) NULL,
      HttpVerb            NVARCHAR(50) NULL, -- GET, POST, PUT, DELETE
      ReturnType          NVARCHAR(50) NULL, -- List, Singular, Delete, Update
      [CreatedById]       INT NULL,
      [CreatedDateTime]   DATETIME NULL,
      [ModifiedById]      INT NULL,
      [ModifiedDateTime]  DATETIME NULL,
      CONSTRAINT [PK_TestApi] PRIMARY KEY CLUSTERED (
        [TestApiId]
      )
    )
END 
GO

PRINT 'CHECK TABLE [dbo].[TestApiLog]'
GO

/* History:
15/6/17  
  Created to store the TestApiLog records - used to test the RESTFul services 
*/

IF OBJECT_Id ('dbo.TestApiLog') IS NULL
BEGIN 
CREATE TABLE [dbo].[TestApiLog] 
    (
      [TestApiLogId] INT IdENTITY(1,1) NOT NULL,
      [TestApiLogGuid] UNIQUEIdENTIFIER NOT NULL CONSTRAINT DF_TestApiLog_TestApiLogGuid DEFAULT NEWId(),
      [TestApiId] INT NOT NULL,
      [CreatedById] INT NULL,
      [CreatedDateTime] DATETIME NULL,
      StatusCode NVARCHAR(300) NULL,
      Response VARCHAR(MAX) NULL,
      CONSTRAINT [PK_TestApiLog] PRIMARY KEY CLUSTERED (
        [TestApiLogId]
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

IF OBJECT_Id ('dbo.User') IS NULL
BEGIN 
CREATE TABLE [dbo].[User] 
    (
      [UserId] INT IdENTITY(1,1) NOT NULL
      , [UserGuid] UNIQUEIdENTIFIER NOT NULL CONSTRAINT DF_User_UserGuid DEFAULT NEWId()
      , [Surname] NVARCHAR(100) NULL
      , [FirstName] NVARCHAR(100) NULL
      , [Email] NVARCHAR(300) NULL
      , [Mobile] NVARCHAR(300) NULL
      , [CurrentHandicap] DECIMAL(18,2) NULL
      , [CurrentHandicapCalcdOn] DATETIME NULL
      , [CreatedById] INT NULL
      , [CreatedDateTime] DATETIME NULL
      , [ModifiedById] INT NULL
      , [ModifiedDateTime] DATETIME NULL
      , StatusId INT NULL
      CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED (
        [UserId]
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

IF OBJECT_Id ('dbo.UserStatus') IS NULL
BEGIN 
CREATE TABLE [dbo].[UserStatus] 
    (
      [UserStatusId] INT IdENTITY(1,1) NOT NULL
      , [Description] VARCHAR(100) -- foreign key to user
      , [CreatedById] INT NULL
      , [CreatedDateTime] DATETIME NULL
      , [ModifiedById] INT NULL
      , [ModifiedDateTime] DATETIME NULL
      CONSTRAINT [PK_UserStatus] PRIMARY KEY CLUSTERED (
        [UserStatusId]
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

IF OBJECT_Id ('dbo.UserGolfRound') IS NULL
BEGIN 
CREATE TABLE [dbo].[UserGolfRound] 
    (
      [UserGolfRoundId] INT IdENTITY(1,1) NOT NULL
      , [UserId] INT -- foreign key to user
      , [GolfCourseName] NVARCHAR(1000) NOT NULL
      , [GolfRoundDate] DATETIME NULL
      , StableFordScore INT NULL
      , StrokeScore INT NULL
      , HandicapThatDay INT NULL
      , CourseParThatDay INT NULL
      , [CreatedById] INT NULL
      , [CreatedDateTime] DATETIME NULL
      , [ModifiedById] INT NULL
      , [ModifiedDateTime] DATETIME NULL
      CONSTRAINT [PK_UserGolfRound] PRIMARY KEY CLUSTERED (
        [UserGolfRoundId]
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

IF OBJECT_Id ('dbo.GolfCourse') IS NULL
BEGIN 
CREATE TABLE [dbo].[GolfCourse] 
    (
      [GolfCourseId] INT IdENTITY(1,1) NOT NULL
      , [CourseName] NVARCHAR(1000) NULL
      , [Address] NVARCHAR(300) NULL
      , [CoursePar] INT NULL
      , [CreatedById] INT NULL
      , [CreatedDateTime] DATETIME NULL
      , [ModifiedById] INT NULL
      , [ModifiedDateTime] DATETIME NULL
      CONSTRAINT [PK_GolfCourse] PRIMARY KEY CLUSTERED (
        [GolfCourseId]
      )
    )
END 
GO

PRINT 'CREATE FUNCTION fnGetCurrentUserId. SW 16/11/15'
GO

IF OBJECT_Id('dbo.fnGetCurrentUserId') IS NULL
  EXEC ('CREATE FUNCTION dbo.fnGetCurrentUserId() RETURNS INT BEGIN RETURN (SELECT 0) END')
GO

ALTER FUNCTION dbo.fnGetCurrentUserId()
RETURNS INT
BEGIN 
  RETURN (SELECT 53)
END
GO

PRINT 'CREATE FUNCTION fnFormatDate'
GO

IF OBJECT_Id('dbo.fnFormatDate') IS NULL
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

PRINT 'ALTER PROCEDURE dbo.TestApiInsert. SW 15/6/17'
GO

IF OBJECT_Id('dbo.TestApiInsert') IS NULL
  EXEC('CREATE PROC dbo.TestApiInsert AS')
GO

ALTER PROCEDURE [dbo].TestApiInsert
  @NewTestApiId INT = NULL OUTPUT,
  @TestApiGuid UNIQUEIdENTIFIER = NULL,
  @TestName NVARCHAR(100) = NULL,
  @TestUrl NVARCHAR(500) = NULL,
  @TestInputJson NVARCHAR(MAX) = NULL,
  @HttpVerb NVARCHAR(50) = NULL,
  @ReturnType NVARCHAR(50) = NULL,
  @SubmitMessage NVARCHAR(1000) = '' OUTPUT
AS
BEGIN
  INSERT INTO [TestApi]
    (
      TestApiGuid,
      TestName,
      TestUrl,
      TestInputJson,
      HttpVerb,
      ReturnType,
      CreatedById,
      CreatedDateTime
    )
  SELECT 
      TestApiGuid = ISNULL(@TestApiGuid, NEWId()),
      TestName = @TestName,
      TestUrl = ISNULL(@TestUrl, ''),
      TestInputJson = @TestInputJson,
      HttpVerb = @HttpVerb,
      ReturnType = @ReturnType,
      CreatedById = dbo.fnGetCurrentUserId(),
      CreatedDateTime = GETDATE()

  SET @NewTestApiId = SCOPE_IdENTITY()
  SET @SubmitMessage = 'Saved at ' + dbo.fnFormatDate(GETDATE(), 'dd MMM yyyy hh:nn:ssam/pm')

END
GO

PRINT 'ALTER PROCEDURE dbo.TestApiList. SW 08/11/15'
GO

IF OBJECT_Id('dbo.TestApiList') IS NULL
  EXEC('CREATE PROC dbo.TestApiList AS')
GO

ALTER PROCEDURE [dbo].TestApiList
  @TestApiId INT = NULL OUTPUT
AS
BEGIN
  SET @TestApiId = NULLIF(@TestApiId, 0)

  -- get last log record in a CTE 
  ;WITH cte AS
  (
    SELECT 
      TestApiId,
      Response, 
      StatusCode,
      CreatedDateTime,
      RowNumber = ROW_NUMBER() OVER (PARTITION BY tal.TestApiId ORDER BY CreatedDateTime DESC)
    FROM TestApiLog tal 
    WHERE tal.TestApiId = ISNULL(@TestApiId, tal.TestApiId)
  )

  SELECT 
    t.TestApiId,
    t.TestApiGuid,
    t.TestName,
    t.TestUrl,
    t.TestInputJson,
    t.HttpVerb,
    t.ReturnType,
    t.CreatedById,
    t.CreatedDateTime,
    LastLogDateTime = cte.CreatedDateTime,
    LastLogResponse = cte.Response,
    LastStatusCode = cte.StatusCode
  FROM TestApi t
    LEFT JOIN cte ON
      cte.TestApiId = t.TestApiId
      AND cte.RowNumber = 1 -- only get the top record (effectively a top(1))
  WHERE t.TestApiId = ISNULL(@TestApiId, t.TestApiId)
  
END
-- EXEC TestApiList @TestApiId = 2
GO

IF OBJECT_Id('dbo.TestApiLogInsert') IS NULL
  EXEC('CREATE PROC dbo.TestApiLogInsert AS')
GO

ALTER PROCEDURE [dbo].TestApiLogInsert
  @TestApiId INT,
  @Response NVARCHAR(MAX) = NULL,
  @StatusCode NVARCHAR(300) = NULL,
  @NewTestApiLogId INT = NULL OUTPUT 
AS
BEGIN
  INSERT INTO [TestApiLog]
    (
      TestApiId,
      Response,
      StatusCode,
      CreatedById,
      CreatedDateTime
    )
  SELECT 
      TestApiId = @TestApiId,
      Response = @Response,
      StatusCode = @StatusCode,
      CreatedById = dbo.fnGetCurrentUserId(),
      CreatedDateTime = GETDATE()

  SET @NewTestApiLogId = SCOPE_IdENTITY()

END
  -- EXEC TestApiLogInsert @TestApiId = 1, @Response = 'Success 1'
GO


PRINT 'ALTER PROCEDURE dbo.UserUpdate. SW 08/11/15'
GO

IF OBJECT_Id('dbo.UserUpdate') IS NULL
  EXEC('CREATE PROC dbo.UserUpdate AS')
GO

ALTER PROCEDURE [dbo].UserUpdate
  @UserId INT 
  , @Surname NVARCHAR(100) 
  , @FirstName NVARCHAR(100) 
  , @Email NVARCHAR(300) 
  , @Mobile NVARCHAR(300)
  , @CurrentHandicap DECIMAL(18,2) = NULL 
  , @SubmitMessage NVARCHAR(1000) = '' OUTPUT
  , @UserGuid UNIQUEIdENTIFIER = NULL
AS
BEGIN
  UPDATE [User]
  SET
    Surname = @Surname 
    , FirstName = @FirstName
    , Email = @Email
    , Mobile = @Mobile
    , CurrentHandicap = @CurrentHandicap 
    , ModifiedById = dbo.fnGetCurrentUserId()
    , ModifiedDateTime = GETDATE()
  WHERE UserId = @UserId

  SET @SubmitMessage = 'Updated at ' + dbo.fnFormatDate(GETDATE(), 'dd MMM yyyy hh:nn:ssam/pm')

END
GO

PRINT 'ALTER PROCEDURE dbo.UserInsert. SW 08/11/15'
GO

IF OBJECT_Id('dbo.UserInsert') IS NULL
  EXEC('CREATE PROC dbo.UserInsert AS')
GO

ALTER PROCEDURE [dbo].UserInsert
  @NewUserId INT = '' OUTPUT
  , @Surname NVARCHAR(100) 
  , @FirstName NVARCHAR(100) 
  , @Email NVARCHAR(300) 
  , @Mobile NVARCHAR(300)
  , @CurrentHandicap DECIMAL(18,2) = NULL 
  , @SubmitMessage NVARCHAR(1000) = '' OUTPUT
  , @UserGuid UNIQUEIdENTIFIER = NULL
AS
BEGIN
  INSERT INTO [User]
    (
      Surname  
      , FirstName 
      , Email 
      , Mobile 
      , CurrentHandicap 
      , CreatedById 
      , CreatedDateTime
      , StatusId
      , UserGuid
    )
  SELECT 
    Surname = @Surname 
    , FirstName = @FirstName
    , Email = @Email
    , Mobile = @Mobile
    , CurrentHandicap = @CurrentHandicap 
    , CreatedById = dbo.fnGetCurrentUserId()
    , CreatedDateTime = GETDATE()
    , StatusId = 1 -- 'Active'
    , UserGuid = ISNULL(@UserGuid, NEWId())

  SET @NewUserId = SCOPE_IdENTITY()
  SET @SubmitMessage = 'New User Created ' + dbo.fnFormatDate(GETDATE(), 'dd MMM yyyy hh:nn:ssam/pm')

END
GO

PRINT 'ALTER PROCEDURE dbo.UserDelete. SW 08/11/15'
GO

IF OBJECT_Id('dbo.UserDelete') IS NULL
  EXEC('CREATE PROC dbo.UserDelete AS')
GO

ALTER PROCEDURE [dbo].UserDelete
  @UserId INT, 
  @SubmitMessage NVARCHAR(1000) = '' OUTPUT
AS
BEGIN
  IF NOT EXISTS(SELECT * FROM dbo.[User] WHERE UserId = @UserId)
  BEGIN
    SET @SubmitMessage = 'User ' + CONVERT(VARCHAR(30), ISNULL(@UserId, '-1')) + ' does not exist ' + dbo.fnFormatDate(GETDATE(), 'dd MMM yyyy hh:nn:ssam/pm')
    RETURN -1
  END  

  DELETE FROM [dbo].[User] 
  WHERE UserId = @UserId

  SET @SubmitMessage = 'Deleted at ' + dbo.fnFormatDate(GETDATE(), 'dd MMM yyyy hh:nn:ssam/pm')

END
GO

PRINT 'ALTER PROCEDURE dbo.UserList. SW 08/11/15'
GO

IF OBJECT_Id('dbo.UserList') IS NULL
  EXEC('CREATE PROC dbo.UserList AS')
GO

ALTER PROCEDURE [dbo].UserList
  @UserId INT = NULL OUTPUT
AS
BEGIN
  SELECT 
    Surname          
    , FirstName    
    , Email        
    , Mobile           
    , CurrentHandicap  
    , CurrentHandicapCalcdOn 
    , CreatedById 
    , CreatedDateTime 
    , StatusId 
    , FullName = Surname 
        + CASE 
            WHEN ISNULL(FirstName, '') <> '' THEN ', ' + FirstName
            ELSE ''
          END
    , LastGolfRoundDate = 
          (
            SELECT TOP 1 GolfRoundDate 
            FROM UserGolfRound ugr
            WHERE UserId = u.UserId
            ORDER BY GolfRoundDate DESC
          )
  FROM [User] u
  WHERE UserId = ISNULL(@UserId, UserId)
  
END
-- EXEC UserList
GO

PRINT 'ALTER PROCEDURE dbo.UserGolfRoundInsert. SW 08/11/15'
GO

IF OBJECT_Id('dbo.UserGolfRoundInsert') IS NULL
  EXEC('CREATE PROC dbo.UserGolfRoundInsert AS')
GO

ALTER PROCEDURE [dbo].UserGolfRoundInsert
  @NewUserGolfRoundId INT = NULL OUTPUT
  , @UserId INT -- foreign key to user
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
      UserId 
      , GolfCourseName 
      , GolfRoundDate
      , StableFordScore 
      , StrokeScore 
      , HandicapThatDay 
      , CourseParThatDay
      , CreatedById
      , CreatedDateTime
    )
  SELECT 
    UserId             = @UserId
    , GolfCourseName   = @GolfCourseName
    , GolfRoundDate    = ISNULL(@GolfRoundDate, '1 Jan 1899')
    , StableFordScore  = @StableFordScore
    , StrokeScore      = @StrokeScore 
    , HandicapThatDay  = @HandicapThatDay
    , CourseParThatDay = @CourseParThatDay
    , CreatedById      = dbo.fnGetCurrentUserId() 
    , CreatedDateTime  = GETDATE()
                       
  SET @NewUserGolfRoundId = SCOPE_IdENTITY()
  SET @SubmitMessage = 'Submitted at ' + dbo.fnFormatDate(GETDATE(), 'dd MMM yyyy hh:nn:ssam/pm')

END
GO

PRINT 'ALTER PROCEDURE dbo.UserGolfRoundList. SW 08/11/15'
GO

IF OBJECT_Id('dbo.UserGolfRoundList') IS NULL
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

PRINT 'DML - TestApi'
GO

/*
api/v1/users/54          --> GET: Get user 54
api/v1/users             --> GET: Get All
api/v1/users             --> POST: Add new user
api/v1/users/54          --> DELETE: Delete User 54
api/v1/users/54/rounds   --> GET: Get all rounds for user 54

api/v1/rounds/13         --> GET: Get Round 13
api/v1/rounds            --> GET: Get all rounds
api/v1/rounds            --> POST: Add new round
api/v1/rounds/13         --> DELETE: Delete round 13


GET 
POST Create
POST Update (or PUT)
DELETE
*/

;WITH defaultTestsCte AS
(
  SELECT TestName = 'GetUser', HttpVerb = 'GET', TestUrl = 'api/users/1', TestInputJson = '', ReturnType = 'Singular', CreatedById = dbo.fnGetCurrentUserId(), CreatedDateTime = GETDATE()
  UNION ALL 
  SELECT TestName = 'GetAllUsers', HttpVerb = 'GET', TestUrl = 'api/users', TestInputJson = '', ReturnType = 'List', CreatedById = dbo.fnGetCurrentUserId(), CreatedDateTime = GETDATE()
  UNION ALL 
  SELECT TestName = 'AddUser', HttpVerb = 'POST', TestUrl = 'api/users', TestInputJson = '{ Surname:"Welsh", FirstName:"Simon", Mobile:"0400925025", CurrentHandicap:11 }', ReturnType = '', CreatedById = dbo.fnGetCurrentUserId(), CreatedDateTime = GETDATE()
  UNION ALL 
  SELECT TestName = 'DeleteUser', HttpVerb = 'DELETE', TestUrl = 'api/users/1', TestInputJson = '', ReturnType = '', CreatedById = dbo.fnGetCurrentUserId(), CreatedDateTime = GETDATE()
  UNION ALL 
  SELECT TestName = 'GetRoundsForUser', HttpVerb = 'GET', TestUrl = 'api/users/2/rounds', TestInputJson = '', ReturnType = 'List', CreatedById = dbo.fnGetCurrentUserId(), CreatedDateTime = GETDATE()
)

INSERT INTO TestApi
    (
      TestName,
      HttpVerb,
      TestUrl,
      TestInputJson,
      ReturnType,
      CreatedById,
      CreatedDateTime
    )
  SELECT 
      cte.TestName,
      cte.HttpVerb,
      cte.TestUrl,
      cte.TestInputJson,
      cte.ReturnType,
      cte.CreatedById,
      cte.CreatedDateTime
  FROM defaultTestsCte cte
    LEFT JOIN TestApi ta ON 
      ta.TestName = cte.TestName
  WHERE ta.TestAPIID IS NULL

  -- drop table testapi