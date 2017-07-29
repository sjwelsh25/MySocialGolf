USE BeadCreative
GO

/* DDL - Tables */

PRINT'Check Table Category. SW 25/10/2007'
GO
IF OBJECT_ID('dbo.Category') IS NULL 
BEGIN
  CREATE TABLE [dbo].[Category] 
  (
    [CategoryKey]  uniqueidentifier ROWGUIDCOL  NOT NULL ,
    [Name] [varchar] (100) NULL,
 	  CONSTRAINT [PK_Category_CategoryKey] PRIMARY KEY  CLUSTERED 
	  (
		  [CategoryKey]
	  )  
	)
END
GO

PRINT'Check Table CategoryItem. SW 25/10/2007'
GO
IF OBJECT_ID('dbo.CategoryItem') IS NULL 
BEGIN
  CREATE TABLE dbo.CategoryItem 
  (
    [CategoryItemKey]  uniqueidentifier ROWGUIDCOL  NOT NULL ,
    [CategoryKey] [uniqueidentifier] NULL,
    [Name] [varchar] (100) NULL,
    CONSTRAINT [PK_CategoryItem_CategoryItemKey] PRIMARY KEY  CLUSTERED 
    (
      [CategoryItemKey]
    )
  )
END  
GO

PRINT'Check Table Community. SW 01/04/2008'
GO
IF OBJECT_ID('dbo.Community') IS NULL 
BEGIN
  CREATE TABLE dbo.Community 
  (
    [CommunityKey]  uniqueidentifier ROWGUIDCOL  NOT NULL ,
    [FullName] [varchar] (100) NULL ,
    [Address] [varchar] (100) NULL ,
    [Address2] [varchar] (100) NULL,
    [Suburb] [varchar] (100) NULL,
    [State] [varchar] (10) NULL,
    [PostCode] [varchar] (10) NULL ,
    [PhoneContact] [varchar] (20) NULL ,
    [Fax] [varchar] (20) NULL ,
    [Email] [varchar] (100) NULL ,
    [Password] [varchar] (20) NULL ,
    [CommunityType] [int] NULL , -- Customer or Staff or Supplier?
    [Notes] [varchar] (500) NULL ,
    [BirthDate] [datetime] NULL,
    [HearAboutUs] [varchar] (100) NULL ,
    CONSTRAINT [PK_Community] PRIMARY KEY  CLUSTERED 
    (
      [CommunityKey]
    )  
  )
END  
GO

PRINT'Check Table Customer. SW 13/05/2008'
GO
IF OBJECT_ID('dbo.Customer') IS NULL 
BEGIN
  CREATE TABLE dbo.Customer 
  (
    [CustomerCommunityKey]  uniqueidentifier NOT NULL,
    CONSTRAINT [PK_Customer] PRIMARY KEY  CLUSTERED 
    (
      [CustomerCommunityKey]
    )
  )
END
GO

PRINT'Check Table Image. SW 25/10/2007'
GO
IF OBJECT_ID('dbo.Image') IS NULL 
BEGIN
  CREATE TABLE dbo.Image 
  (
    [ImageKey]  uniqueidentifier ROWGUIDCOL  NOT NULL ,
    [ImageParentKey] [uniqueidentifier] NULL ,
    [ImageData] [image] NULL ,
    [FileName] [varchar] (100) NULL ,
    [Description] [varchar] (100) NULL ,
    [DateTimeUploaded] [datetime] NULL,
    CONSTRAINT [PK_Image] PRIMARY KEY  CLUSTERED 
    (
      [ImageKey]
    )
  ) 
END  
GO

PRINT'Check Table luStockStatus. SW 29/11/2007'
GO
IF OBJECT_ID('dbo.LuStockStatus') IS NULL 
BEGIN
	CREATE TABLE [dbo].[LuStockStatus] 
	(
		[LuStockStatusKey]  uniqueidentifier ROWGUIDCOL  NOT NULL ,
		[Status] [varchar] (50) NULL,
  	CONSTRAINT [PK_StockStatus] PRIMARY KEY  CLUSTERED 
		(
			[LuStockStatusKey]
		)
	)
END
GO

PRINT'Check Table Sale. SW 25/10/2007'
GO
IF OBJECT_ID('dbo.Sale') IS NULL 
BEGIN
  CREATE TABLE dbo.Sale 
    (
      [SaleKey]  uniqueidentifier ROWGUIDCOL  NOT NULL ,
      [SaleDateTime] [datetime] NULL ,
      [StaffCommunityKey] [uniqueidentifier] NULL ,
      [ModifiedDateTime] [datetime] NULL ,
      [CreatedDateTime] [datetime] NULL,
	    CONSTRAINT [PK_Sale] PRIMARY KEY  CLUSTERED 
	    (
	      [SaleKey]
	    )
    )
END  
GO

PRINT'Check Table SaleItem. SW 25/10/2007'
GO
IF OBJECT_ID('dbo.SaleItem') IS NULL 
BEGIN
  CREATE TABLE dbo.SaleItem 
  (
    [SaleItemKey]  uniqueidentifier ROWGUIDCOL  NOT NULL ,
    [SaleKey] [uniqueidentifier] NULL ,
    [StockKey] [uniqueidentifier] NULL ,
    [SellInc] [money] NULL ,
    [CostInc] [money] NULL ,
    [StockSellInc] [money] NULL ,
    [Qty] [float] NULL,
		CONSTRAINT [PK_SaleItem] PRIMARY KEY  CLUSTERED 
		(
			[SaleItemKey]
		)
  )
END  
GO

PRINT'Check Table Staff. SW 13/05/2008'
GO
IF OBJECT_ID('dbo.Staff') IS NULL 
BEGIN
  CREATE TABLE dbo.Staff 
  (
    [StaffCommunityKey]  uniqueidentifier NOT NULL ,
    CONSTRAINT [PK_Staff] PRIMARY KEY  CLUSTERED 
    (
      [StaffCommunityKey]
    )
  )
END
GO

PRINT'Check Table Stock. SW 28/11/2007'
GO
IF OBJECT_ID('dbo.Stock') IS NULL 
BEGIN
	CREATE TABLE [dbo].[Stock] 
	(
		[StockKey]  uniqueidentifier ROWGUIDCOL  NOT NULL ,
		[Description] [varchar] (100) NULL ,
		[LongDescription] [varchar] (500) NULL ,
		[CategoryKey] [uniqueidentifier] NULL ,
		[CategoryItemKey] [uniqueidentifier] NULL ,
		[SupplierCommunityKey] [uniqueidentifier] NULL ,
		[Barcode] [varchar] (30) NULL ,
		[SupplierStockCode] [varchar] (30) NULL ,
		[PLU] [varchar] (20) NULL ,
		[CostInc] [money] NULL ,
		[SellInc] [money] NULL ,
		[CostTax] [money] NULL ,
		[SellTax] [money] NULL ,
		[SerialNumber] [varchar] (50) NULL ,
		[OnHand] [decimal](18, 0) NULL ,
		[StoreKey] [uniqueidentifier] NULL ,
		[CreatedDateTime] [datetime] NULL ,
    [CreatedStaffCommunityKey] [uniqueidentifier] NULL,
    [ImageKey] [uniqueidentifier] NULL ,
		[RRP] [decimal](18, 0) NULL ,
		[LuStockStatusKey] [uniqueidentifier] NULL,
    [Title] VARCHAR(100) NULL,
		CONSTRAINT [PK_Stock] PRIMARY KEY  CLUSTERED 
		(
			[StockKey]
		)	
  )
END
GO

PRINT 'Alter table Stock. SW 12/2/08'
GO
IF NOT EXISTS
    (
      SELECT * FROM dbo.SYSCOLUMNS sc
      WHERE sc.ID = OBJECT_ID('Stock')
        AND sc.Name = 'Title'
    )
BEGIN
  ALTER TABLE dbo.Stock
    ADD [Title] VARCHAR(100) NULL
END
GO

PRINT 'Alter table Stock - Rename EnteredDateTime to CreatedDateTime. SW 17/3/08'
GO
IF EXISTS
    (
      SELECT * FROM dbo.SYSCOLUMNS sc
      WHERE sc.ID = OBJECT_ID('Stock')
        AND sc.Name = 'EnteredDateTime'
    )
BEGIN
  exec dbo.sp_rename 'Stock.EnteredDateTime', 'CreatedDateTime'
END
GO

PRINT 'Alter table Stock - Add ModifiedDateTime. SW 17/3/08'
GO
IF NOT EXISTS
	  (
	    SELECT * FROM dbo.SYSCOLUMNS sc
	    WHERE sc.ID = OBJECT_ID('Stock')
	      AND sc.Name = 'ModifiedDateTime'
	  )
BEGIN
  ALTER TABLE dbo.Stock
    ADD [ModifiedDateTime] [datetime] NULL ,
			  [ModifiedCommunityKey] [uniqueidentifier] NULL
END
GO
  
IF NOT EXISTS
	  (
	    SELECT * FROM dbo.SYSCOLUMNS sc
	    WHERE sc.ID = OBJECT_ID('Stock')
	      AND sc.Name = 'LastSoldDateTime'
	  )
BEGIN
  ALTER TABLE dbo.Stock
    ADD LastSoldDateTime DATETIME NULL
END
GO

PRINT'Check Table Store. SW 29/11/2007'
GO
IF OBJECT_ID('dbo.Store') IS NULL 
BEGIN
	CREATE TABLE [dbo].[Store] 
	(
		[StoreKey]  uniqueidentifier ROWGUIDCOL  NOT NULL ,
		[Name] [varchar] (100) NULL ,
		[Address1] [varchar] (100) NULL ,
		[Address2] [varchar] (100) NULL ,
		[Suburb] [varchar] (100) NULL ,
		[Postcode] [varchar] (6) NULL ,
		[ManagerName] [varchar] (50) NULL,
		CONSTRAINT [PK_Store] PRIMARY KEY  CLUSTERED 
		(
			[StoreKey]
		)
	)
END
GO

PRINT'Check Table Supplier. SW 16/07/2008'
GO
IF OBJECT_ID('dbo.Supplier') IS NULL 
BEGIN
  CREATE TABLE dbo.Supplier 
  (
    [SupplierCommunityKey] uniqueidentifier NOT NULL ,
    CONSTRAINT [PK_Supplier] PRIMARY KEY CLUSTERED 
    (
      [SupplierCommunityKey]
    )
  )
END
GO

PRINT'Check Table Workshop. SW 29/11/2007'
GO
IF OBJECT_ID('dbo.Workshop') IS NULL 
BEGIN
	CREATE TABLE [dbo].[Workshop] 
	(
		[WorkshopKey]  uniqueidentifier ROWGUIDCOL  NOT NULL ,
		[Name] [varchar] (50) NULL ,
		[FullAddress] [varchar] (100) NULL ,
		[Notes] [varchar] (500) NULL ,
		[ImageKey] [uniqueidentifier] NULL,
		CONSTRAINT [PK_Workshop] PRIMARY KEY  CLUSTERED 
		(
			[WorkshopKey]
		)
	)
END
GO

PRINT'Check Table WorkshopCust. SW 29/11/2007'
GO
IF OBJECT_ID('dbo.WorkshopCust') IS NULL 
BEGIN
	CREATE TABLE [dbo].[WorkshopCust] 
  (
		[WorkshopCustKey]  uniqueidentifier ROWGUIDCOL  NOT NULL ,
		[WorkshopKey] [uniqueidentifier] NULL ,
		[CustomerCommunityKey] [uniqueidentifier] NULL,
		CONSTRAINT [PK_WorkshopCust] PRIMARY KEY  CLUSTERED 
		(
			[WorkshopCustKey]
		)
	)
END
GO

PRINT'Check Table WorkshopItem. SW 29/11/2007'
GO
IF OBJECT_ID('dbo.WorkshopItem') IS NULL 
BEGIN
	CREATE TABLE [dbo].[WorkshopItem] 
	(
		[WorkshopItemKey]  uniqueidentifier ROWGUIDCOL  NOT NULL ,
		[Name] [varchar] (50) NULL ,
		[WorkshopKey] [uniqueidentifier] NULL ,
		[WorkshopDateTime] [datetime] NULL ,
		[Notes] [varchar] (300) NULL,
		CONSTRAINT [PK_WorkshopItem] PRIMARY KEY  CLUSTERED 
		(
			[WorkshopItemKey]
		)
	)
END
GO

/* DDL - Procs */

PRINT 'ALTER PROCEDURE dbo.spiWorkShopCust'
GO

IF OBJECT_ID('dbo.spiWorkShopCust') IS NULL
  EXEC('CREATE PROC dbo.spiWorkShopCust AS')
GO

ALTER PROCEDURE dbo.spiWorkShopCust
  @WorkshopKey UNIQUEIDENTIFIER = NULL,
  @CustomerCommunityKey UNIQUEIDENTIFIER = NULL
AS
BEGIN
  IF (@WorkshopKey IS NULL)
      AND (@CustomerCommunityKey IS NULL)
  BEGIN
    GOTO on_error
  END    

  IF NOT EXISTS
      (
        SELECT CustomerCommunityKey
        FROM dbo.WorkshopCust wc
        WHERE wc.WorkshopKey = @WorkshopKey 
          AND wc.CustomerCommunityKey = @CustomerCommunityKey
      )
  BEGIN
    INSERT INTO dbo.WorkshopCust
      (WorkshopKey, CustomerCommunityKey)
    VALUES
      (@WorkshopKey, @CustomerCommunityKey)
  END
  
  on_error: 
    RETURN -1
  
END;
GO

PRINT 'ALTER PROCEDURE dbo.spsCategory'
GO

IF OBJECT_ID('dbo.spsCategory') IS NULL
  EXEC('CREATE PROC dbo.spsCategory AS')
GO

ALTER PROCEDURE dbo.spsCategory
  @Name VARCHAR(100) = NULL
AS
SET NOCOUNT ON;
BEGIN
  SET @Name = ISNULL(@Name, '')

  SELECT *
  FROM
    dbo.Category c
  WHERE
    (@Name  = '') OR (c.Name LIKE @Name+'%')
  ORDER BY
    c.Name
END;
GO

PRINT 'ALTER PROCEDURE dbo.spsCustomer'
GO

IF OBJECT_ID('dbo.spsCustomer') IS NULL
  EXEC('CREATE PROC dbo.spsCustomer AS')
GO

ALTER PROCEDURE dbo.spsCustomer
  @CommunityKey UNIQUEIDENTIFIER = NULL,
  @Email varchar(100) = NULL
AS
SET NOCOUNT ON;

BEGIN
  SET @Email = ISNULL(@Email, '')

  SELECT *  
  FROM
    dbo.Customer cu 
    INNER JOIN dbo.Community co ON
    (
      cu.CustomerCommunityKey = co.CommunityKey
    )
  WHERE
    (@CommunityKey IS NULL)
    OR (cu.CustomerCommunityKey = @CommunityKey)
		AND 
		(
        (@Email = '') OR (co.Email = @Email)
		)
  ORDER BY
    co.FullName
END;
GO

PRINT 'ALTER PROCEDURE dbo.spsStaff'
GO

IF OBJECT_ID('dbo.spsStaff') IS NULL
  EXEC('CREATE PROC dbo.spsStaff AS')
GO

ALTER PROCEDURE dbo.spsStaff
  @CommunityKey UNIQUEIDENTIFIER = NULL,
  @Email varchar(100) = NULL
AS
SET NOCOUNT ON;

BEGIN
  SET @Email = ISNULL(@Email, '')

  SELECT *  
  FROM
    dbo.Staff st 
    INNER JOIN dbo.Community co ON
    (
      st.StaffCommunityKey = co.CommunityKey
    )
  WHERE
    (@CommunityKey IS NULL)
    OR (st.StaffCommunityKey = @CommunityKey)
		AND 
		(
        (@Email = '') OR (co.Email = @Email)
		)
  ORDER BY
    co.FullName
END;
GO

PRINT 'ALTER PROCEDURE dbo.spsWorkShopCust'
GO

IF OBJECT_ID('dbo.spsWorkShopCust') IS NULL
  EXEC('CREATE PROC dbo.spsWorkShopCust AS')
GO

ALTER PROCEDURE [dbo].[spsWorkShopCust]
  @WorkshopKey UNIQUEIDENTIFIER = NULL
AS
SET NOCOUNT ON;
BEGIN
  SELECT
    wc.CustomerCommunityKey, c.FullName,
    c.Address, c.Address2, c.Suburb,
    c.State, c.Postcode, c.Email,
    c.Password, c.CommunityType, c.PhoneContact,
    wc.WorkshopKey
  FROM
    dbo.WorkShopCust wc
		LEFT JOIN dbo.Community c ON
		(
			wc.CustomerCommunityKey = c.CommunityKey
		)
  WHERE
    (@WorkshopKey IS NULL) 
    OR wc.WorkshopKey = @WorkshopKey
  ORDER BY
    c.FullName
END;
GO

PRINT 'ALTER PROCEDURE dbo.spsSaleItem'
GO

IF OBJECT_ID('dbo.spsSaleItem') IS NULL
  EXEC('CREATE PROC dbo.spsSaleItem AS')
GO

ALTER PROCEDURE [dbo].[spsSaleItem]
  @StockKey UNIQUEIDENTIFIER = NULL
AS
SET NOCOUNT ON;
BEGIN
  SELECT
    si.Qty, si.StockKey, 
    sa.SaleKey, sa.SaleDateTime, sa.StaffCommunityKey, sa.ModifiedDateTime, sa.CreatedDateTime,
    st.*
  FROM
    dbo.SaleItem si 
    INNER JOIN dbo.Sale sa ON 
      (
  			si.SaleKey = sa.SaleKey 
  		)
    LEFT JOIN dbo.Stock st ON
      (
        si.StockKey = st.StockKey
      )
  WHERE
    (@StockKey IS NULL)
    OR (si.StockKey = @StockKey)
  ORDER BY
    sa.SaleDateTime
END;
GO

PRINT 'ALTER PROCEDURE dbo.spsStock. SW 25/10/2007'
GO

IF OBJECT_ID('dbo.spsStock') IS NULL
  EXEC('CREATE PROC dbo.spsStock AS')
GO

ALTER PROCEDURE [dbo].[spsStock]
	@StockKey UNIQUEIDENTIFIER = NULL,
  @CategoryKey UNIQUEIDENTIFIER = NULL,
  @CategoryItemKey UNIQUEIDENTIFIER = NULL
AS
BEGIN
  SET NOCOUNT ON;
  
  SELECT
    s.StockKey, s.Description, s.LongDescription, s.CategoryKey, s.CategoryItemKey,
    s.Barcode, s.SupplierStockCode, s.SellInc, s.RRP, s.OnHand,
    s.ImageKey,
    i.FileName, i.DateTimeUploaded, i.Description, lus.LuStockStatusKey, lus.Status  
  FROM
    dbo.Stock s 
    LEFT JOIN dbo.Image i ON 
    (
      s.ImageKey = i.ImageKey
		)
    LEFT JOIN dbo.luStockStatusKey lus ON 
    (
      s.luStockStatusKey = lus.luStockStatusKey
    )
  WHERE 
    (@StockKey IS NULL) OR (@StockKey = s.StockKey)
    AND 
		(
			  (@CategoryKey IS NULL) OR (@CategoryKey = s.CategoryKey)
		)
    AND 
    (
        (@CategoryKey IS NULL) OR (@CategoryKey = s.CategoryKey)
    )
		AND 
		(
        (@CategoryItemKey IS NULL) OR (@CategoryItemKey = s.CategoryItemKey)
		)
  ORDER BY
    s.Description
END;
GO

PRINT 'ALTER PROCEDURE dbo.spsBlankStock. SW 25/10/2007'
GO

IF OBJECT_ID('dbo.spsBlankStock') IS NULL
  EXEC('CREATE PROC dbo.spsBlankStock AS')
GO

ALTER PROCEDURE [dbo].[spsBlankStock]
AS
BEGIN
  SET NOCOUNT ON;
  SELECT
    DISTINCT(st.StoreKey),
    c.CategoryKey,
    lus.LuStockStatusKey
  FROM
    dbo.Store st,
    dbo.Category c,
    dbo.StockStatus lus
  WHERE
    (st.Name LIKE ('QUEENSLAND%')) AND
    (c.Name LIKE 'BEADS%') AND
    (lus.Status LIKE 'IN Stock%')
END;
GO

PRINT 'ALTER PROCEDURE dbo.spsLookup. SW 25/10/2007'
GO

IF OBJECT_ID('dbo.spsLookup') IS NULL
  EXEC('CREATE PROC dbo.spsLookup AS')
GO

ALTER PROCEDURE [dbo].[spsLookup]
  @LookupTableName VARCHAR(50)
AS
SET NOCOUNT ON;
BEGIN
  DECLARE 
    @tmpSQL VARCHAR(1000)
		-- @tmpCRLF VARCHAR()
  
  SET @tmpSQL = 'SELECT * FROM ' + @LookupTableName

  EXEC @tmpSQL

END;
GO

PRINT 'ALTER PROCEDURE dbo.spsStore. SW 25/10/2007'
GO

IF OBJECT_ID('dbo.spsStore') IS NULL
  EXEC('CREATE PROC dbo.spsStore AS')
GO

ALTER PROCEDURE [dbo].[spsStore] AS
SET NOCOUNT ON;
BEGIN
  SELECT
    s.StoreKey, s.Name
  FROM
    dbo.Store s
  ORDER BY
    s.Name
END
GO

PRINT 'ALTER PROCEDURE dbo.spsCategoryItem. SW 25/10/2007'
GO

IF OBJECT_ID('dbo.spsCategoryItem') IS NULL
  EXEC('CREATE PROC dbo.spsCategoryItem AS')
GO

ALTER PROCEDURE [dbo].[spsCategoryItem]
  @CategoryKey UNIQUEIDENTIFIER = NULL
AS
BEGIN
  SELECT
    ci.CategoryKey, ci.Name, ci.CategoryItemKey
  FROM
    dbo.CategoryItem ci
  WHERE
    ci.CategoryKey = @CategoryKey
  ORDER BY
    ci.Name
END;
GO

PRINT 'ALTER PROCEDURE dbo.spsSupplierCommunity. SW 25/10/2007'
GO

IF OBJECT_ID('dbo.spsSupplierCommunity') IS NULL
  EXEC('CREATE PROC dbo.spsSupplierCommunity AS')
GO

ALTER PROCEDURE [dbo].[spsSupplierCommunity]
  @SupplierCommunityKey UNIQUEIDENTIFIER
AS
BEGIN
  SET NOCOUNT ON;
  SELECT
    c.CommunityKey AS SupplierCommunityKey, c.FullName, c.PhoneContact, c.Email,
    c.Address, c.Suburb, c.Postcode, c.State, c.Notes
  FROM
    dbo.Community c
  WHERE 
    (@SupplierCommunityKey IS NULL) OR (@SupplierCommunityKey = c.CommunityKey)
  ORDER BY
    c.FullName
END;
GO

PRINT 'ALTER PROCEDURE dbo.spsWorkshop. SW 25/10/2007'
GO

IF OBJECT_ID('dbo.spsWorkshop') IS NULL
  EXEC('CREATE PROC dbo.spsWorkshop AS')
GO

ALTER PROCEDURE [dbo].[spsWorkshop]
  @WorkshopKey UNIQUEIDENTIFIER
AS
BEGIN
  SET NOCOUNT ON;
  SELECT
    WorkshopKey, Name, FullAddress,
    Notes, ImageKey
  FROM
    dbo.Workshop w
  WHERE 
		(@WorkshopKey IS NULL) OR (@WorkshopKey = w.WorkshopKey)
  ORDER BY
    w.Name
END;
GO

PRINT 'ALTER PROCEDURE dbo.spsWorkshopItem. SW 25/10/2007'
GO

IF OBJECT_ID('dbo.spsWorkshopItem') IS NULL
  EXEC('CREATE PROC dbo.spsWorkshopItem AS')
GO

ALTER PROCEDURE [dbo].[spsWorkshopItem]
AS
BEGIN
  SET NOCOUNT ON;
  SELECT
    WorkshopItemKey, WorkshopKey, Name, WorkshopDateTime,
    Notes
  FROM
    dbo.WorkshopItem
  ORDER BY
    WorkshopDateTime
END;
GO

PRINT 'ALTER PROCEDURE dbo.spiCommunity. SW 01/04/2007'
GO

IF OBJECT_ID('dbo.spiCommunity') IS NULL
  EXEC('CREATE PROC dbo.spiCommunity AS')
GO

ALTER PROCEDURE [dbo].[spiCommunity]
  @CommunityKey UNIQUEIDENTIFIER,
  @FullName VARCHAR(100),
  @Address VARCHAR(100),
  @Address2 VARCHAR(100),
  @Suburb VARCHAR(100),
  @State VARCHAR(10),
  @PostCode VARCHAR(10),
  @PhoneContact VARCHAR(20),
  @Fax VARCHAR(20),
  @Email VARCHAR(100),
  @Password VARCHAR(20),
  @CommunityType VARCHAR(30) = NULL, -- 'Customer' or 'Supplier' or 'Staff'
  @Notes VARCHAR(500),
  @HearAboutUs VARCHAR(100),
  @BirthDate DATETIME,
  @NewCommunityKey UNIQUEIDENTIFIER OUTPUT
AS
BEGIN
  IF EXISTS
      (
    		SELECT PostCode FROM dbo.Community WHERE CommunityKey = @CommunityKey
      )
  BEGIN
    SET @NewCommunityKey = @CommunityKey
    UPDATE Community
    SET
      FullName = @FullName,
      Address = @Address,
      Address2 = @Address2,
      Suburb = @Suburb,
      State = @State,
      PostCode = @PostCode,
      PhoneContact = @PhoneContact,
      Fax = @Fax,
      Email = @Email,
      PassWord = @PassWord,
      Notes = @Notes,
      HearAboutUs = @HearAboutUs,
      BirthDate = @BirthDate
    WHERE CommunityKey = @CommunityKey
  END ELSE
  BEGIN
    SET @NewCommunityKey = NEWID()
    INSERT INTO dbo.Community
        (
          CommunityKey, 
          FullName, 
          Address, 
          Address2, 
          Suburb,
          State, 
          PostCode, 
          PhoneContact, 
          FAX,
          Email, 
          Password, 
          Notes, 
          HearAboutUs,
          BirthDate
        )
    SELECT
        @NewCommunityKey, 
        @FullName, 
        @Address, 
        @Address2, 
        @Suburb,
        @State, 
        @PostCode, 
        @PhoneContact, 
        @Fax,
        @Email, 
        @Password, 
        @Notes, 
        @HearAboutUs,
        @BirthDate
  END
  
  IF @CommunityType = 'Customer'
  BEGIN
    EXEC dbo.spiCustomer
        @CommunityKey = @CommunityKey
  END 
  ELSE IF @CommunityType = 'Supplier'
  BEGIN  
    EXEC dbo.spiStaff
        @CommunityKey = @CommunityKey
  END
  ELSE IF @CommunityType = 'Staff'
  BEGIN  
    EXEC dbo.spiStaff
        @CommunityKey = @CommunityKey

  END
END;
GO

PRINT 'ALTER PROCEDURE dbo.spiImage. SW 25/10/2007'
GO

IF OBJECT_ID('dbo.spiImage') IS NULL
  EXEC('CREATE PROC dbo.spiImage AS')
GO

ALTER PROCEDURE [dbo].[spiImage]
  @ImageKey UNIQUEIDENTIFIER,
  @ImageParentKey UNIQUEIDENTIFIER,
  @ImageData IMAGE,
  @FileName VARCHAR(100),
  @Description VARCHAR(100),
  @DateTimeUploaded DATETIME,
  @NewImageKey UNIQUEIDENTIFIER OUTPUT
AS
BEGIN
  IF EXISTS
      (
    		SELECT DateTimeUploaded FROM IMAGE WHERE ImageKey = @ImageKey
    	)
  BEGIN
    SET @NewImageKey = @ImageKey
    UPDATE Image
    SET
      ImageParentKey = @ImageParentKey,
      ImageData = @ImageData,
      FileName = @FileName,
      Description = @Description,
      DateTimeUploaded = @DateTimeUploaded
    WHERE ImageKey = @ImageKey
  END ELSE
  BEGIN
    SET @NewImageKey = NEWID()
    INSERT INTO Image
      (ImageKey, ImageParentKey, ImageData, FileName,
      Description, DateTimeUploaded)
    VALUES
      (@NewImageKey, @NewImageKey, @ImageData, @FileName,
      @Description, @DateTimeUploaded)
  END
END;
GO

PRINT 'ALTER PROCEDURE dbo.spiStock. SW 25/10/2007'
GO

IF OBJECT_ID('dbo.spiStock') IS NULL
  EXEC('CREATE PROC dbo.spiStock AS')
GO

ALTER PROCEDURE [dbo].[spiStock]
  @StockKey UNIQUEIDENTIFIER,
  @Description VARCHAR(100),
  @LongDescription VARCHAR(500),
  @CategoryKey UNIQUEIDENTIFIER,
  @CategoryItemKey UNIQUEIDENTIFIER,
  @SupplierCommunityKey UNIQUEIDENTIFIER,
  @StoreKey UNIQUEIDENTIFIER,
  @ImageKey UNIQUEIDENTIFIER,
  @LuStockStatusKey UNIQUEIDENTIFIER,
  @Barcode VARCHAR(30),
  @SupplierStockCode VARCHAR(30),
  @PLU VARCHAR(20),
  @CostInc MONEY,
  @SellInc MONEY,
  @CostTax MONEY,
  @SellTax MONEY,
  @RRP MONEY,
  @SerialNumber VARCHAR(50),
  @OnHand DECIMAL,
  @Title VARCHAR(100),
  @NewStockKey UNIQUEIDENTIFIER OUTPUT
AS
BEGIN
  IF EXISTS
      (
    		SELECT CreatedDateTime FROM STOCK WHERE StockKey = @StockKey
    	)
  BEGIN
    SET @NewStockKey = @StockKey
    UPDATE Stock
    SET
      Description = @Description,
      LongDescription = @LongDescription,
      CategoryKey = @CategoryKey,
      CategoryItemKey = @CategoryItemKey,
      SupplierCommunityKey = @SupplierCommunityKey,
      StoreKey = @StoreKey,
      ImageKey = @ImageKey,
      LuStockStatusKey = @LuStockStatusKey,
      Barcode = @Barcode,
      SupplierStockCode = @SupplierStockCode,
      PLU = @PLU,
      CostInc = @CostInc,
      SellInc = @SellInc,
      CostTax = @CostTax,
      SellTax = @SellTax,
      RRP = @RRP,
      SerialNumber = @SerialNumber,
      OnHand = @OnHand,
      ModifiedDateTime = GETDATE(),
			Title = @Title
    WHERE StockKey = @StockKey
  END ELSE
  BEGIN
    SET @NewStockKey = NEWID()
    INSERT INTO Stock
      (StockKey, Description, LongDescription, CategoryKey, CategoryItemKey, SupplierCommunityKey,
      StoreKey, ImageKey, LuStockStatusKey, Barcode, SupplierStockCode, PLU,
      CostInc, SellInc, CostTax, SellTax, RRP, SerialNumber,
      OnHand, CreatedDateTime, ModifiedDateTime)
    VALUES
      (@NewStockKey, @Description, @LongDescription, @CategoryKey, @CategoryItemKey, @SupplierCommunityKey,
      @StoreKey, @ImageKey, @LuStockStatusKey, @Barcode, @SupplierStockCode, @PLU, @CostInc, @SellInc,
      @CostTax, @SellTax, @RRP, @SerialNumber, @OnHand,
      GETDATE(), GETDATE())
  END
END;
GO

PRINT 'ALTER PROCEDURE dbo.spiWorkshop. SW 25/10/2007'
GO

IF OBJECT_ID('dbo.spiWorkshop') IS NULL
  EXEC('CREATE PROC dbo.spiWorkshop AS')
GO

ALTER PROCEDURE [dbo].[spiWorkshop]
  @WorkshopKey UNIQUEIDENTIFIER,
  @Name VARCHAR(50),
  @FullAddress VARCHAR(100),
  @Notes VARCHAR(500),
  @ImageKey UNIQUEIDENTIFIER,
  @NewWorkshopKey UNIQUEIDENTIFIER OUTPUT
AS
BEGIN
  IF EXISTS
	(
		SELECT * FROM Workshop WHERE WorkshopKey = @WorkshopKey
	)
  BEGIN
    SET @NewWorkshopKey = @WorkshopKey
    UPDATE Workshop
    SET
      Name = @Name,
      FullAddress = @FullAddress,
      Notes = @Notes,
      ImageKey = @ImageKey
    WHERE WorkshopKey = @WorkshopKey
  END ELSE
  BEGIN
    SET @NewWorkshopKey = NEWID()
    INSERT INTO Workshop
      (WorkshopKey, Name, FullAddress, Notes, ImageKey)
    VALUES
      (@NewWorkshopKey, @Name, @FullAddress, @Notes, @ImageKey)
  END
END;
GO

PRINT 'ALTER PROCEDURE dbo.spgCommunityTypeFromKey. SW 25/10/2007'
GO

IF OBJECT_ID('dbo.spgCommunityTypeFromKey') IS NULL
  EXEC('CREATE PROC dbo.spgCommunityTypeFromKey AS')
GO

ALTER PROCEDURE [dbo].[spgCommunityTypeFromKey]
  @CommunityKey UNIQUEIDENTIFIER = NULL,
  @CommunityType INT OUTPUT
AS
BEGIN
  SET NOCOUNT ON;
  SELECT
    @CommunityType = CommunityType
  FROM
    dbo.Community
  WHERE
    (@CommunityKey IS NULL)
    OR (CommunityKey = @CommunityKey)
END;
GO

PRINT 'ALTER PROCEDURE dbo.spsImage. SW 25/10/2007'
GO

IF OBJECT_ID('dbo.spsImage') IS NULL
  EXEC('CREATE PROC dbo.spsImage AS')
GO

ALTER PROCEDURE [dbo].[spsImage]
  @ImageKey UNIQUEIDENTIFIER
AS
BEGIN
  SET NOCOUNT ON;
  SELECT
    I.ImageKey, I.ImageParentKey, I.ImageData, I.FileName,
    I.Description, DateTimeUploaded
  FROM
    "Image" I
  WHERE
    I.ImageKey = @ImageKey
END;
GO

/* DML */

PRINT 'Insert Default Category Records. SW 9/12/07'
GO

IF NOT EXISTS
(
  SELECT * FROM Category c
  WHERE c.Name = 'Findings'
)
BEGIN
  INSERT INTO Category
    (CategoryKey, Name)
  SELECT 
    'b0d1e7ec-ffbb-481c-95f2-03add0d78faf', 'Findings'
    UNION SELECT 'b299429d-c87a-4f0e-844d-22569f3ed4a5', 'Beads'
    UNION SELECT 'd439bcac-e1a6-40e1-aeca-7f1129c92302', 'Tools'
    UNION SELECT 'babf25dc-02f7-45aa-9b51-94a4e7bbb417', 'Chains'
    UNION SELECT 'fab7d377-0c9e-496b-ae4e-b909a4a5d20c', 'Books and Patterns'
    UNION SELECT 'b68de30f-3890-49ef-a0ee-beae9076d280', 'Jewellery'
    UNION SELECT '33f29612-d5d3-4550-a61d-4e9df3a69823', 'Stringing'

  INSERT INTO CategoryItem
    (CategoryKey, CategoryItemKey, Name)
  SELECT
    /* Findings */
    'b0d1e7ec-ffbb-481c-95f2-03add0d78faf', '4934836C-94E7-4CE2-A064-2ADD5D6295EE', 'Split Rings'
    UNION SELECT 'b0d1e7ec-ffbb-481c-95f2-03add0d78faf', '14FAC9D7-C652-4C3A-B5C4-88E814F7554C', 'Jump Rings'
    UNION SELECT 'b0d1e7ec-ffbb-481c-95f2-03add0d78faf', '102DAD87-1355-42C5-B2F6-93BB4AF31162', 'Charms'
    UNION SELECT 'b0d1e7ec-ffbb-481c-95f2-03add0d78faf', '74949B88-C1FB-489B-9651-A8FB82460396', 'Earrings'
    UNION SELECT 'b0d1e7ec-ffbb-481c-95f2-03add0d78faf', 'A4F38025-D2DD-4D37-9A92-B5D1086811A7', 'Pins (Head and Eye)'
    UNION SELECT 'b0d1e7ec-ffbb-481c-95f2-03add0d78faf', 'DDCC50E8-188B-43C6-9D97-E313D2DEEE49', 'Clasps'
    
    /* Beads */
    UNION SELECT 'b299429d-c87a-4f0e-844d-22569f3ed4a5', '1A5A238E-F617-42AE-BFD1-88637637AC2E', 'Metal'
    UNION SELECT 'b299429d-c87a-4f0e-844d-22569f3ed4a5', '6B7A153D-F9D1-4340-B4F6-33FC060D16F8', 'Glass'
    UNION SELECT 'b299429d-c87a-4f0e-844d-22569f3ed4a5', 'A0D85E10-2AAF-42D3-86F7-36C8FE8C2AC4', 'Precious Stones'
    UNION SELECT 'b299429d-c87a-4f0e-844d-22569f3ed4a5', 'D3F70AAC-C666-4C81-A040-3BB634E116AF', 'Wooden'
    UNION SELECT 'b299429d-c87a-4f0e-844d-22569f3ed4a5', '84BD5CC4-64AC-4192-954D-498B4C25DA03', 'Crystal'
    UNION SELECT 'b299429d-c87a-4f0e-844d-22569f3ed4a5', 'A522A3CA-3913-4E01-A28A-5A251B10B0C4', 'Ceramic'

    /* Stringing */
    UNION SELECT '33f29612-d5d3-4550-a61d-4e9df3a69823', '7F967E7C-21FA-46FA-B0EA-661869DF811E', 'Floss/Thread'
    UNION SELECT '33f29612-d5d3-4550-a61d-4e9df3a69823', 'BCF82A3B-B0C0-4F72-ADED-6A6E6A35BAE3', 'Elastic/Stretch'
    UNION SELECT '33f29612-d5d3-4550-a61d-4e9df3a69823', '5B1731C8-DEF3-4485-ABF4-13E02FF3B145', 'Wire'
    UNION SELECT '33f29612-d5d3-4550-a61d-4e9df3a69823', '0F486C5A-818C-4D91-8AB9-FAB7D8298407', 'Leather'
    
    /* Tools */
    UNION SELECT 'd439bcac-e1a6-40e1-aeca-7f1129c92302', 'F302C1D4-0BB3-460E-A657-C87B08018981', 'Pliers'
    UNION SELECT 'd439bcac-e1a6-40e1-aeca-7f1129c92302', 'FDFFEBF3-8EBA-4117-830A-96E99A48CC00', 'Stoppers'
    UNION SELECT 'd439bcac-e1a6-40e1-aeca-7f1129c92302', '1A0D262F-3E15-4442-9CA4-7CCA2B545BB4', 'Reamers'
    UNION SELECT 'd439bcac-e1a6-40e1-aeca-7f1129c92302', '144B478C-4E79-4927-A8BE-4E6F628A38E3', 'Cutters'
                 
    /* Books and Patterns */
    UNION SELECT 'fab7d377-0c9e-496b-ae4e-b909a4a5d20c', '83434C50-E5F9-4A5C-A007-8A70A45E65B0', 'Magazines'
    UNION SELECT 'fab7d377-0c9e-496b-ae4e-b909a4a5d20c', '33D07E41-DEBD-4949-9888-D4D04514A902', 'Patterns'
    UNION SELECT 'fab7d377-0c9e-496b-ae4e-b909a4a5d20c', '4391EBE4-F014-4E5B-9838-EB4CA66719DE', 'Books'

    /* Jewellery */
    UNION SELECT 'b68de30f-3890-49ef-a0ee-beae9076d280', 'CB0BF87B-D28B-4074-9F85-F75200E2D7CD', 'Rings'
    UNION SELECT 'b68de30f-3890-49ef-a0ee-beae9076d280', '03E52003-737E-4423-B1E7-D65BCA76CF47', 'Bracelets'
    UNION SELECT 'b68de30f-3890-49ef-a0ee-beae9076d280', '1A856DF2-6BEB-44D9-8BCF-B773E08FB739', 'Earrings'
    UNION SELECT 'b68de30f-3890-49ef-a0ee-beae9076d280', 'E5A30F36-98B2-4047-8F49-9375CD234596', 'Anklets'
    UNION SELECT 'b68de30f-3890-49ef-a0ee-beae9076d280', '4A5E03E9-A811-4F8E-9999-1CAD9D49DC8A', 'Necklaces'
    UNION SELECT 'b68de30f-3890-49ef-a0ee-beae9076d280', 'C96DFF04-44E4-45A9-A312-AC5D0DBA2DE0', 'Broaches'

END
  
