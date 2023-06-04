USE [master]
GO
/****** Object:  Database [task_distribution_app_db]    Script Date: 6/4/2023 8:26:43 AM ******/
CREATE DATABASE [task_distribution_app_db]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'task_distribution_app_db', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\task_distribution_app_db.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'task_distribution_app_db_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\task_distribution_app_db_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [task_distribution_app_db] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [task_distribution_app_db].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [task_distribution_app_db] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [task_distribution_app_db] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [task_distribution_app_db] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [task_distribution_app_db] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [task_distribution_app_db] SET ARITHABORT OFF 
GO
ALTER DATABASE [task_distribution_app_db] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [task_distribution_app_db] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [task_distribution_app_db] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [task_distribution_app_db] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [task_distribution_app_db] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [task_distribution_app_db] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [task_distribution_app_db] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [task_distribution_app_db] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [task_distribution_app_db] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [task_distribution_app_db] SET  DISABLE_BROKER 
GO
ALTER DATABASE [task_distribution_app_db] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [task_distribution_app_db] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [task_distribution_app_db] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [task_distribution_app_db] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [task_distribution_app_db] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [task_distribution_app_db] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [task_distribution_app_db] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [task_distribution_app_db] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [task_distribution_app_db] SET  MULTI_USER 
GO
ALTER DATABASE [task_distribution_app_db] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [task_distribution_app_db] SET DB_CHAINING OFF 
GO
ALTER DATABASE [task_distribution_app_db] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [task_distribution_app_db] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [task_distribution_app_db] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [task_distribution_app_db] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [task_distribution_app_db] SET QUERY_STORE = OFF
GO
USE [task_distribution_app_db]
GO
/****** Object:  UserDefinedFunction [dbo].[F_FIND_AVAILABLE_DEVELOPER]    Script Date: 6/4/2023 8:26:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Ziya Çetinkaya
-- Create date: 2023-06-03
-- Description:	Verilen zorluk seviyesi için uygun olan Developer'ı bulur.
-- =============================================
CREATE FUNCTION [dbo].[F_FIND_AVAILABLE_DEVELOPER]
(
	@DIFFICULTYLEVEL_ID INT
)
RETURNS INT
AS
BEGIN
	DECLARE @USER_ID INT

	DECLARE @T_USER_TASK_COUNT TABLE (USER_ID INT, USER_FULLNAME NVARCHAR(150), TASK_COUNT INT)
	
	DELETE FROM @T_USER_TASK_COUNT
	INSERT INTO @T_USER_TASK_COUNT 
	SELECT TUSER.USER_ID, TUSER.USER_FULLNAME, COUNT(TTASK.TASK_ID) AS TASK_COUNT 
	FROM TUSER 
	LEFT JOIN TTASK ON TTASK.TASK_ASSIGNED_USER_ID = TUSER.USER_ID AND TTASK.TASK_DIFFICULTYLEVEL_ID = @DIFFICULTYLEVEL_ID
	WHERE USER_ROLE_ID = 3
	GROUP BY TUSER.USER_ID, TUSER.USER_FULLNAME
	
	DECLARE @FIRST_TASK_COUNT INT	
	SELECT TOP 1 @FIRST_TASK_COUNT = TASK_COUNT FROM @T_USER_TASK_COUNT ORDER BY TASK_COUNT, USER_FULLNAME
	IF (@FIRST_TASK_COUNT = 0) 
		SELECT TOP 1 @USER_ID = USER_ID FROM @T_USER_TASK_COUNT  ORDER BY TASK_COUNT, USER_FULLNAME
	ELSE 
		SELECT TOP 1 @USER_ID = TASK_ASSIGNED_USER_ID
		FROM TTASK
		WHERE TASK_DIFFICULTYLEVEL_ID = @DIFFICULTYLEVEL_ID
			AND TASK_ASSIGNED_USER_ID NOT IN (
				SELECT T1.TASK_ASSIGNED_USER_ID
				FROM TTASK T1
				INNER JOIN TTASK T2 ON T1.TASK_ASSIGNED_USER_ID = T2.TASK_ASSIGNED_USER_ID AND T1.TASK_ASSIGNED_DATE > T2.TASK_ASSIGNED_DATE
				WHERE T1.TASK_DIFFICULTYLEVEL_ID = T2.TASK_DIFFICULTYLEVEL_ID AND T1.TASK_DIFFICULTYLEVEL_ID = @DIFFICULTYLEVEL_ID
			)
		ORDER BY TASK_ASSIGNED_DATE ASC

	RETURN @USER_ID
END
GO
/****** Object:  Table [dbo].[TDIFFICULTYLEVEL]    Script Date: 6/4/2023 8:26:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TDIFFICULTYLEVEL](
	[DIFFICULTYLEVEL_ID] [int] IDENTITY(1,1) NOT NULL,
	[DIFFICULTYLEVEL_SCORE] [int] NULL,
	[DIFFICULTYLEVEL_NAME] [nvarchar](50) NULL,
 CONSTRAINT [PK_TDIFFICULTYLEVEL] PRIMARY KEY CLUSTERED 
(
	[DIFFICULTYLEVEL_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TROLE]    Script Date: 6/4/2023 8:26:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TROLE](
	[ROLE_ID] [int] IDENTITY(1,1) NOT NULL,
	[ROLE_NAME] [nvarchar](50) NULL,
 CONSTRAINT [PK_TROLE] PRIMARY KEY CLUSTERED 
(
	[ROLE_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TTASK]    Script Date: 6/4/2023 8:26:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TTASK](
	[TASK_ID] [int] IDENTITY(1,1) NOT NULL,
	[TASK_TITLE] [nvarchar](250) NULL,
	[TASK_DESCRIPTION] [nvarchar](max) NULL,
	[TASK_DIFFICULTYLEVEL_ID] [int] NULL,
	[TASK_ASSIGNED_USER_ID] [int] NULL,
	[TASK_ASSIGNED_DATE] [datetime] NULL,
	[TASK_IS_COMPLETE] [bit] NULL,
	[TASK_COMPLETE_DATE] [datetime] NULL,
	[TASK_CREATED_AT] [datetime] NULL,
	[TASK_CREATED_BY] [int] NULL,
 CONSTRAINT [PK_TTASK] PRIMARY KEY CLUSTERED 
(
	[TASK_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TUSER]    Script Date: 6/4/2023 8:26:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TUSER](
	[USER_ID] [int] IDENTITY(1,1) NOT NULL,
	[USER_USERNAME] [nvarchar](50) NULL,
	[USER_FULLNAME] [nvarchar](150) NULL,
	[USER_PASSWORD] [nvarchar](50) NULL,
	[USER_ROLE_ID] [int] NULL,
 CONSTRAINT [PK_TUSER] PRIMARY KEY CLUSTERED 
(
	[USER_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[TDIFFICULTYLEVEL] ON 
GO
INSERT [dbo].[TDIFFICULTYLEVEL] ([DIFFICULTYLEVEL_ID], [DIFFICULTYLEVEL_SCORE], [DIFFICULTYLEVEL_NAME]) VALUES (1, 1, N'LEVEL 1')
GO
INSERT [dbo].[TDIFFICULTYLEVEL] ([DIFFICULTYLEVEL_ID], [DIFFICULTYLEVEL_SCORE], [DIFFICULTYLEVEL_NAME]) VALUES (2, 2, N'LEVEL 2')
GO
INSERT [dbo].[TDIFFICULTYLEVEL] ([DIFFICULTYLEVEL_ID], [DIFFICULTYLEVEL_SCORE], [DIFFICULTYLEVEL_NAME]) VALUES (3, 3, N'LEVEL 3')
GO
INSERT [dbo].[TDIFFICULTYLEVEL] ([DIFFICULTYLEVEL_ID], [DIFFICULTYLEVEL_SCORE], [DIFFICULTYLEVEL_NAME]) VALUES (4, 4, N'LEVEL 4')
GO
INSERT [dbo].[TDIFFICULTYLEVEL] ([DIFFICULTYLEVEL_ID], [DIFFICULTYLEVEL_SCORE], [DIFFICULTYLEVEL_NAME]) VALUES (5, 5, N'LEVEL 5')
GO
INSERT [dbo].[TDIFFICULTYLEVEL] ([DIFFICULTYLEVEL_ID], [DIFFICULTYLEVEL_SCORE], [DIFFICULTYLEVEL_NAME]) VALUES (6, 6, N'LEVEL 6')
GO
INSERT [dbo].[TDIFFICULTYLEVEL] ([DIFFICULTYLEVEL_ID], [DIFFICULTYLEVEL_SCORE], [DIFFICULTYLEVEL_NAME]) VALUES (7, 7, N'LEVEL 7')
GO
INSERT [dbo].[TDIFFICULTYLEVEL] ([DIFFICULTYLEVEL_ID], [DIFFICULTYLEVEL_SCORE], [DIFFICULTYLEVEL_NAME]) VALUES (8, 8, N'LEVEL 8')
GO
SET IDENTITY_INSERT [dbo].[TDIFFICULTYLEVEL] OFF
GO
SET IDENTITY_INSERT [dbo].[TROLE] ON 
GO
INSERT [dbo].[TROLE] ([ROLE_ID], [ROLE_NAME]) VALUES (1, N'YÖNETİCİ')
GO
INSERT [dbo].[TROLE] ([ROLE_ID], [ROLE_NAME]) VALUES (2, N'ANALİST')
GO
INSERT [dbo].[TROLE] ([ROLE_ID], [ROLE_NAME]) VALUES (3, N'DEVELOPER')
GO
SET IDENTITY_INSERT [dbo].[TROLE] OFF
GO
SET IDENTITY_INSERT [dbo].[TUSER] ON 
GO
INSERT [dbo].[TUSER] ([USER_ID], [USER_USERNAME], [USER_FULLNAME], [USER_PASSWORD], [USER_ROLE_ID]) VALUES (1, N'ahmet.yilmaz', N'Ahmet Yilmaz', N'12345', 1)
GO
INSERT [dbo].[TUSER] ([USER_ID], [USER_USERNAME], [USER_FULLNAME], [USER_PASSWORD], [USER_ROLE_ID]) VALUES (2, N'ayse.kaya', N'Ayse Kaya', N'12345', 2)
GO
INSERT [dbo].[TUSER] ([USER_ID], [USER_USERNAME], [USER_FULLNAME], [USER_PASSWORD], [USER_ROLE_ID]) VALUES (3, N'mehmet.demir', N'Mehmet Demir', N'12345', 3)
GO
INSERT [dbo].[TUSER] ([USER_ID], [USER_USERNAME], [USER_FULLNAME], [USER_PASSWORD], [USER_ROLE_ID]) VALUES (4, N'fatma.arslan', N'Fatma Arslan', N'12345', 3)
GO
INSERT [dbo].[TUSER] ([USER_ID], [USER_USERNAME], [USER_FULLNAME], [USER_PASSWORD], [USER_ROLE_ID]) VALUES (5, N'ali.can', N'Ali Can', N'12345', 3)
GO
INSERT [dbo].[TUSER] ([USER_ID], [USER_USERNAME], [USER_FULLNAME], [USER_PASSWORD], [USER_ROLE_ID]) VALUES (6, N'aysenur.öztürk', N'Aysenur Öztürk', N'12345', 3)
GO
INSERT [dbo].[TUSER] ([USER_ID], [USER_USERNAME], [USER_FULLNAME], [USER_PASSWORD], [USER_ROLE_ID]) VALUES (7, N'murat.sahin', N'Murat Sahin', N'12345', 3)
GO
INSERT [dbo].[TUSER] ([USER_ID], [USER_USERNAME], [USER_FULLNAME], [USER_PASSWORD], [USER_ROLE_ID]) VALUES (8, N'elif.dogan', N'Elif Dogan', N'12345', 3)
GO
INSERT [dbo].[TUSER] ([USER_ID], [USER_USERNAME], [USER_FULLNAME], [USER_PASSWORD], [USER_ROLE_ID]) VALUES (9, N'hasan.yildiz', N'Hasan Yildiz', N'12345', 3)
GO
INSERT [dbo].[TUSER] ([USER_ID], [USER_USERNAME], [USER_FULLNAME], [USER_PASSWORD], [USER_ROLE_ID]) VALUES (10, N'sibel.avci', N'Sibel Avci', N'12345', 3)
GO
SET IDENTITY_INSERT [dbo].[TUSER] OFF
GO
ALTER TABLE [dbo].[TTASK] ADD  CONSTRAINT [DF_TTASK_TASK_IS_COMPLETE]  DEFAULT ((0)) FOR [TASK_IS_COMPLETE]
GO
ALTER TABLE [dbo].[TTASK]  WITH CHECK ADD  CONSTRAINT [FK_TTASK_TDIFFICULTYLEVEL] FOREIGN KEY([TASK_DIFFICULTYLEVEL_ID])
REFERENCES [dbo].[TDIFFICULTYLEVEL] ([DIFFICULTYLEVEL_ID])
GO
ALTER TABLE [dbo].[TTASK] CHECK CONSTRAINT [FK_TTASK_TDIFFICULTYLEVEL]
GO
ALTER TABLE [dbo].[TTASK]  WITH CHECK ADD  CONSTRAINT [FK_TTASK_TUSER] FOREIGN KEY([TASK_ASSIGNED_USER_ID])
REFERENCES [dbo].[TUSER] ([USER_ID])
GO
ALTER TABLE [dbo].[TTASK] CHECK CONSTRAINT [FK_TTASK_TUSER]
GO
ALTER TABLE [dbo].[TTASK]  WITH CHECK ADD  CONSTRAINT [FK_TTASK_TUSER1] FOREIGN KEY([TASK_CREATED_BY])
REFERENCES [dbo].[TUSER] ([USER_ID])
GO
ALTER TABLE [dbo].[TTASK] CHECK CONSTRAINT [FK_TTASK_TUSER1]
GO
ALTER TABLE [dbo].[TUSER]  WITH CHECK ADD  CONSTRAINT [FK_TUSER_TROLE] FOREIGN KEY([USER_ROLE_ID])
REFERENCES [dbo].[TROLE] ([ROLE_ID])
GO
ALTER TABLE [dbo].[TUSER] CHECK CONSTRAINT [FK_TUSER_TROLE]
GO
USE [master]
GO
ALTER DATABASE [task_distribution_app_db] SET  READ_WRITE 
GO
