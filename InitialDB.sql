USE [master]
GO
/****** Object:  Database [ContactsDatabase]    Script Date: 16.09.2021 15:01:32 ******/
CREATE DATABASE [ContactsDatabase]
 CONTAINMENT = NONE

 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [ContactsDatabase] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ContactsDatabase].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ContactsDatabase] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ContactsDatabase] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ContactsDatabase] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ContactsDatabase] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ContactsDatabase] SET ARITHABORT OFF 
GO
ALTER DATABASE [ContactsDatabase] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [ContactsDatabase] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ContactsDatabase] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ContactsDatabase] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ContactsDatabase] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ContactsDatabase] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ContactsDatabase] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ContactsDatabase] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ContactsDatabase] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ContactsDatabase] SET  DISABLE_BROKER 
GO
ALTER DATABASE [ContactsDatabase] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ContactsDatabase] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ContactsDatabase] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ContactsDatabase] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ContactsDatabase] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ContactsDatabase] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [ContactsDatabase] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ContactsDatabase] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [ContactsDatabase] SET  MULTI_USER 
GO
ALTER DATABASE [ContactsDatabase] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ContactsDatabase] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ContactsDatabase] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ContactsDatabase] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [ContactsDatabase] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [ContactsDatabase] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [ContactsDatabase] SET QUERY_STORE = OFF
GO
USE [ContactsDatabase]
GO
/****** Object:  Table [dbo].[Contacts]    Script Date: 16.09.2021 15:01:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Contacts](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[PhoneNumber] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Contacts] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Contacts] ON 

INSERT [dbo].[Contacts] ([ID], [Name], [PhoneNumber]) VALUES (1, N'a', N'89999999999')
INSERT [dbo].[Contacts] ([ID], [Name], [PhoneNumber]) VALUES (2, N'b', N'89999999999')
INSERT [dbo].[Contacts] ([ID], [Name], [PhoneNumber]) VALUES (3, N'ba', N'89999999999')
INSERT [dbo].[Contacts] ([ID], [Name], [PhoneNumber]) VALUES (4, N'bab', N'89999999999')
INSERT [dbo].[Contacts] ([ID], [Name], [PhoneNumber]) VALUES (5, N'ac', N'89999999999')
INSERT [dbo].[Contacts] ([ID], [Name], [PhoneNumber]) VALUES (6, N'c', N'89999999999')
INSERT [dbo].[Contacts] ([ID], [Name], [PhoneNumber]) VALUES (7, N'a', N'89999999999')
INSERT [dbo].[Contacts] ([ID], [Name], [PhoneNumber]) VALUES (8, N'ab', N'89999999999')
INSERT [dbo].[Contacts] ([ID], [Name], [PhoneNumber]) VALUES (9, N'ab', N'89999999990')
INSERT [dbo].[Contacts] ([ID], [Name], [PhoneNumber]) VALUES (10, N'ab', N'89999999990')
SET IDENTITY_INSERT [dbo].[Contacts] OFF
GO
/****** Object:  StoredProcedure [dbo].[Contact_GetByID]    Script Date: 16.09.2021 15:01:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Contact_GetByID] @ID nvarchar(MAX)
AS
SELECT TOP 1 *
FROM Contacts
WHERE ID = @ID
GO
/****** Object:  StoredProcedure [dbo].[Contact_GetByLetters]    Script Date: 16.09.2021 15:01:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Contact_GetByLetters] 
@firstLetters nvarchar(MAX)
AS
SELECT *
FROM Contacts
WHERE Name LIKE @firstLetters+'%'
GO
USE [master]
GO
ALTER DATABASE [ContactsDatabase] SET  READ_WRITE 
GO
