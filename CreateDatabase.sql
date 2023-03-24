USE [master]
GO
/****** Object:  Database [MobileHomeTracker]    Script Date: 3/24/2023 11:09:05 AM ******/
CREATE DATABASE [MobileHomeTracker]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'MobileHomeTracker', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\MobileHomeTracker.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'MobileHomeTracker_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\MobileHomeTracker_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [MobileHomeTracker] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [MobileHomeTracker].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [MobileHomeTracker] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [MobileHomeTracker] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [MobileHomeTracker] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [MobileHomeTracker] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [MobileHomeTracker] SET ARITHABORT OFF 
GO
ALTER DATABASE [MobileHomeTracker] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [MobileHomeTracker] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [MobileHomeTracker] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [MobileHomeTracker] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [MobileHomeTracker] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [MobileHomeTracker] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [MobileHomeTracker] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [MobileHomeTracker] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [MobileHomeTracker] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [MobileHomeTracker] SET  DISABLE_BROKER 
GO
ALTER DATABASE [MobileHomeTracker] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [MobileHomeTracker] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [MobileHomeTracker] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [MobileHomeTracker] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [MobileHomeTracker] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [MobileHomeTracker] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [MobileHomeTracker] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [MobileHomeTracker] SET RECOVERY FULL 
GO
ALTER DATABASE [MobileHomeTracker] SET  MULTI_USER 
GO
ALTER DATABASE [MobileHomeTracker] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [MobileHomeTracker] SET DB_CHAINING OFF 
GO
ALTER DATABASE [MobileHomeTracker] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [MobileHomeTracker] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [MobileHomeTracker] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [MobileHomeTracker] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'MobileHomeTracker', N'ON'
GO
ALTER DATABASE [MobileHomeTracker] SET QUERY_STORE = OFF
GO
USE [MobileHomeTracker]
GO
/****** Object:  Table [dbo].[Home]    Script Date: 3/24/2023 11:09:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Home](
	[ID] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](100) NULL,
 CONSTRAINT [PK_Homes] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LocationRecord]    Script Date: 3/24/2023 11:09:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LocationRecord](
	[Longitude] [float] NOT NULL,
	[Latitude] [float] NOT NULL,
	[RecordDate] [datetime] NOT NULL,
	[ID] [uniqueidentifier] NOT NULL,
	[HomeID] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_LocationRecords] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[LocationRecord]  WITH CHECK ADD  CONSTRAINT [FK_LocationRecords_Homes] FOREIGN KEY([HomeID])
REFERENCES [dbo].[Home] ([ID])
GO
ALTER TABLE [dbo].[LocationRecord] CHECK CONSTRAINT [FK_LocationRecords_Homes]
GO
/****** Object:  StoredProcedure [dbo].[usp_Home_Delete]    Script Date: 3/24/2023 11:09:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_Home_Delete](
    @ID uniqueidentifier
)
AS
BEGIN
    SET NOCOUNT ON;

    Delete from dbo.Home
    Where ID = @ID
END
GO
/****** Object:  StoredProcedure [dbo].[usp_Home_GetAll]    Script Date: 3/24/2023 11:09:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_Home_GetAll] 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * from dbo.Home
END
GO
/****** Object:  StoredProcedure [dbo].[usp_Home_GetById]    Script Date: 3/24/2023 11:09:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_Home_GetById](
    @Id uniqueidentifier
)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT * FROM dbo.Home
    WHERE id = @id
END
GO
/****** Object:  StoredProcedure [dbo].[usp_Home_Insert]    Script Date: 3/24/2023 11:09:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_Home_Insert] (
    @Id uniqueidentifier, 
    @Name varchar(100)
)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO dbo.Home (
        Id,
        [Name]
    ) VALUES (
        @Id,
        @Name
    );
END
GO
/****** Object:  StoredProcedure [dbo].[usp_Home_Update]    Script Date: 3/24/2023 11:09:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_Home_Update](
    @ID uniqueidentifier,
    @Name varchar(100)
)
AS
BEGIN
    SET NOCOUNT ON;

    Update dbo.Home 
    Set		
        [Name] = @Name
    Where ID = @ID
END
GO
/****** Object:  StoredProcedure [dbo].[usp_LocationRecord_Delete]    Script Date: 3/24/2023 11:09:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_LocationRecord_Delete](
    @ID uniqueidentifier
)
AS
BEGIN
    SET NOCOUNT ON;

    Delete from dbo.LocationRedord
    Where ID = @ID
END
GO
/****** Object:  StoredProcedure [dbo].[usp_LocationRecord_GetAll]    Script Date: 3/24/2023 11:09:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_LocationRecord_GetAll] 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * from dbo.LocationRecord
END
GO
/****** Object:  StoredProcedure [dbo].[usp_LocationRecord_GetById]    Script Date: 3/24/2023 11:09:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_LocationRecord_GetById](
    @Id uniqueidentifier
)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT * FROM dbo.LocationRecord
    WHERE id = @id
END
GO
/****** Object:  StoredProcedure [dbo].[usp_LocationRecord_Insert]    Script Date: 3/24/2023 11:09:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_LocationRecord_Insert] (
    @ID uniqueidentifier, 
    @HomeID varchar(100),
	@Latitude float,
	@Longitude float,
	@RecordDate datetime
)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO dbo.LocationRecord (
        ID,
        HomeID,
		Latitude,
		Longitude,
		RecordDate
    ) VALUES (
        @ID,
        @HomeID,
		@Latitude,
		@Longitude,
		@RecordDate
    );
END
GO
/****** Object:  StoredProcedure [dbo].[usp_LocationRecord_Update]    Script Date: 3/24/2023 11:09:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_LocationRecord_Update](
    @ID uniqueidentifier,
    @HomeID uniqueidentifier,
	@Longitude float,
	@Latitude float,
	@RecordDate datetime
)
AS
BEGIN
    SET NOCOUNT ON;

    Update dbo.LocationRecord
    Set		
        Longitude = @Longitude,
		Latitude = @Latitude,
		RecordDate = @RecordDate
    Where ID = @ID
END
GO
USE [master]
GO
ALTER DATABASE [MobileHomeTracker] SET  READ_WRITE 
GO
