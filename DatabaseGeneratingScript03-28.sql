USE [master]
GO
/****** Object:  Database [Netpincer]    Script Date: 2021-03-28 11:59:14 AM ******/
CREATE DATABASE [Netpincer]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Netpincer', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLSERVER\MSSQL\DATA\Netpincer.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Netpincer_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLSERVER\MSSQL\DATA\Netpincer_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [Netpincer] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Netpincer].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Netpincer] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Netpincer] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Netpincer] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Netpincer] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Netpincer] SET ARITHABORT OFF 
GO
ALTER DATABASE [Netpincer] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Netpincer] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Netpincer] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Netpincer] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Netpincer] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Netpincer] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Netpincer] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Netpincer] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Netpincer] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Netpincer] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Netpincer] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Netpincer] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Netpincer] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Netpincer] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Netpincer] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Netpincer] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Netpincer] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Netpincer] SET RECOVERY FULL 
GO
ALTER DATABASE [Netpincer] SET  MULTI_USER 
GO
ALTER DATABASE [Netpincer] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Netpincer] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Netpincer] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Netpincer] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Netpincer] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'Netpincer', N'ON'
GO
ALTER DATABASE [Netpincer] SET QUERY_STORE = OFF
GO
USE [Netpincer]
GO
/****** Object:  Schema [Restaurant]    Script Date: 2021-03-28 11:59:15 AM ******/
CREATE SCHEMA [Restaurant]
GO
/****** Object:  Schema [Users]    Script Date: 2021-03-28 11:59:15 AM ******/
CREATE SCHEMA [Users]
GO
/****** Object:  Table [Users].[UsersAddress]    Script Date: 2021-03-28 11:59:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Users].[UsersAddress](
	[addressID] [int] IDENTITY(1,1) NOT NULL,
	[city] [nvarchar](50) NOT NULL,
	[zipcode] [char](4) NOT NULL,
	[line1] [nvarchar](50) NOT NULL,
	[line2] [nvarchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[addressID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Users].[Users]    Script Date: 2021-03-28 11:59:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Users].[Users](
	[username] [nvarchar](50) NOT NULL,
	[password] [nvarchar](30) NOT NULL,
	[lastName] [nvarchar](50) NOT NULL,
	[firstName] [nvarchar](50) NOT NULL,
	[phoneNumber] [nvarchar](20) NOT NULL,
	[addressID] [int] NOT NULL,
	[userType] [int] NOT NULL,
	[email] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  UserDefinedFunction [dbo].[getUser]    Script Date: 2021-03-28 11:59:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[getUser](@usernameParam nvarchar(20),@passParam nvarchar(20), @userTypeParam int)
RETURNS TABLE AS
RETURN SELECT username, [password], lastName, firstName, phoneNumber, city, zipcode, line1, line2 ,userType, email
FROM Users.Users AS [u]
JOIN Users.UsersAddress ON Users.UsersAddress.addressID=[u].addressID
WHERE username=@usernameParam AND password=@passParam AND userType=@userTypeParam
GO
/****** Object:  Table [Restaurant].[AllergenNames]    Script Date: 2021-03-28 11:59:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Restaurant].[AllergenNames](
	[allergenID] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[allergenID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Restaurant].[Allergens]    Script Date: 2021-03-28 11:59:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Restaurant].[Allergens](
	[allergenID] [int] NULL,
	[foodID] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [Restaurant].[CategoryName]    Script Date: 2021-03-28 11:59:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Restaurant].[CategoryName](
	[categoryID] [int] IDENTITY(1,1) NOT NULL,
	[categoryName] [nvarchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[categoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Restaurant].[Food]    Script Date: 2021-03-28 11:59:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Restaurant].[Food](
	[foodID] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](60) NOT NULL,
	[price] [float] NOT NULL,
	[rating] [float] NULL,
	[pictureID] [int] NULL,
	[categoryID] [int] NULL,
	[restaurantID] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[foodID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Restaurant].[OpeningHours]    Script Date: 2021-03-28 11:59:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Restaurant].[OpeningHours](
	[openingHoursID] [int] IDENTITY(1,1) NOT NULL,
	[fromHour] [int] NULL,
	[fromMinute] [int] NULL,
	[toHour] [int] NULL,
	[toMinute] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[openingHoursID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Restaurant].[Restaurant]    Script Date: 2021-03-28 11:59:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Restaurant].[Restaurant](
	[restaurantID] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](50) NOT NULL,
	[addressID] [int] NOT NULL,
	[openingHoursID] [int] NULL,
	[restaurantDescription] [nvarchar](200) NOT NULL,
	[style] [nvarchar](50) NOT NULL,
	[owner] [nvarchar](50) NULL,
	[phoneNumber] [nvarchar](20) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[restaurantID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Restaurant].[RestaurantAddress]    Script Date: 2021-03-28 11:59:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Restaurant].[RestaurantAddress](
	[addressID] [int] IDENTITY(1,1) NOT NULL,
	[city] [nvarchar](50) NOT NULL,
	[zipcode] [char](4) NOT NULL,
	[line1] [nvarchar](50) NOT NULL,
	[line2] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[addressID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [Restaurant].[CategoryName] ON 

INSERT [Restaurant].[CategoryName] ([categoryID], [categoryName]) VALUES (1, N'Teszta')
INSERT [Restaurant].[CategoryName] ([categoryID], [categoryName]) VALUES (2, N'Pizza')
INSERT [Restaurant].[CategoryName] ([categoryID], [categoryName]) VALUES (3, N'Pizza')
INSERT [Restaurant].[CategoryName] ([categoryID], [categoryName]) VALUES (4, N'Pizza')
INSERT [Restaurant].[CategoryName] ([categoryID], [categoryName]) VALUES (5, N'Pizza')
SET IDENTITY_INSERT [Restaurant].[CategoryName] OFF
GO
SET IDENTITY_INSERT [Restaurant].[OpeningHours] ON 

INSERT [Restaurant].[OpeningHours] ([openingHoursID], [fromHour], [fromMinute], [toHour], [toMinute]) VALUES (1, 11, 0, 22, 0)
INSERT [Restaurant].[OpeningHours] ([openingHoursID], [fromHour], [fromMinute], [toHour], [toMinute]) VALUES (2, 10, 0, 23, 0)
INSERT [Restaurant].[OpeningHours] ([openingHoursID], [fromHour], [fromMinute], [toHour], [toMinute]) VALUES (3, 10, 0, 23, 0)
INSERT [Restaurant].[OpeningHours] ([openingHoursID], [fromHour], [fromMinute], [toHour], [toMinute]) VALUES (4, 11, 0, 22, 0)
INSERT [Restaurant].[OpeningHours] ([openingHoursID], [fromHour], [fromMinute], [toHour], [toMinute]) VALUES (5, 11, 0, 22, 0)
INSERT [Restaurant].[OpeningHours] ([openingHoursID], [fromHour], [fromMinute], [toHour], [toMinute]) VALUES (6, 11, 0, 22, 0)
INSERT [Restaurant].[OpeningHours] ([openingHoursID], [fromHour], [fromMinute], [toHour], [toMinute]) VALUES (7, 10, 0, 23, 0)
INSERT [Restaurant].[OpeningHours] ([openingHoursID], [fromHour], [fromMinute], [toHour], [toMinute]) VALUES (8, 11, 0, 22, 0)
INSERT [Restaurant].[OpeningHours] ([openingHoursID], [fromHour], [fromMinute], [toHour], [toMinute]) VALUES (9, 11, 0, 22, 0)
INSERT [Restaurant].[OpeningHours] ([openingHoursID], [fromHour], [fromMinute], [toHour], [toMinute]) VALUES (10, 11, 0, 22, 0)
SET IDENTITY_INSERT [Restaurant].[OpeningHours] OFF
GO
SET IDENTITY_INSERT [Restaurant].[Restaurant] ON 

INSERT [Restaurant].[Restaurant] ([restaurantID], [name], [addressID], [openingHoursID], [restaurantDescription], [style], [owner], [phoneNumber]) VALUES (1, N'Teszt Étterem', 1, 1, N'Nagyon szép eskü nem lopott', N'Teljesen egyedi', N'AsztalVokMegint', N'+362255440')
INSERT [Restaurant].[Restaurant] ([restaurantID], [name], [addressID], [openingHoursID], [restaurantDescription], [style], [owner], [phoneNumber]) VALUES (3, N'Ut?lom a C#-ot', 3, 3, N'Hosszabb le?r?s arr?l, mennyire utlom a C#-ot', N'C#', N'AsztalVok', N'+362255440')
INSERT [Restaurant].[Restaurant] ([restaurantID], [name], [addressID], [openingHoursID], [restaurantDescription], [style], [owner], [phoneNumber]) VALUES (7, N'Teszt Étterem', 6, 6, N'Nagyon szép eskü nem lopott', N'Teljesen egyedi', N'AsztalVok1149', N'+3640887799')
INSERT [Restaurant].[Restaurant] ([restaurantID], [name], [addressID], [openingHoursID], [restaurantDescription], [style], [owner], [phoneNumber]) VALUES (8, N'Utalom a C capat', 7, 7, N'Hosszabb leiras arrol, mennyire utlaom a Csharpot', N'C capa', N'Hiiiii', N'+36214563217')
INSERT [Restaurant].[Restaurant] ([restaurantID], [name], [addressID], [openingHoursID], [restaurantDescription], [style], [owner], [phoneNumber]) VALUES (9, N'Teszt Étterem still', 8, 8, N'AAAAAAA', N'Teljesen egyedi v2', N'AsztalVokMegint', N'+3640887799')
INSERT [Restaurant].[Restaurant] ([restaurantID], [name], [addressID], [openingHoursID], [restaurantDescription], [style], [owner], [phoneNumber]) VALUES (10, N'Teszt Étterem still', 9, 9, N'AAAAAAA', N'Teljesen egyedi v2', N'AsztalVokMegint2', N'+3640887799')
INSERT [Restaurant].[Restaurant] ([restaurantID], [name], [addressID], [openingHoursID], [restaurantDescription], [style], [owner], [phoneNumber]) VALUES (11, N'Teszt Étterem still', 10, 10, N'AAAAAAA', N'Teljesen egyedi v2', N'AAAAA', N'+3640887799')
SET IDENTITY_INSERT [Restaurant].[Restaurant] OFF
GO
SET IDENTITY_INSERT [Restaurant].[RestaurantAddress] ON 

INSERT [Restaurant].[RestaurantAddress] ([addressID], [city], [zipcode], [line1], [line2]) VALUES (1, N'Veszprém', N'8200', N'Nem lopott Utca 27', N'ASD')
INSERT [Restaurant].[RestaurantAddress] ([addressID], [city], [zipcode], [line1], [line2]) VALUES (2, N'Veszpr?m', N'8200', N'F?radt vagyok utca 11', N'3/A')
INSERT [Restaurant].[RestaurantAddress] ([addressID], [city], [zipcode], [line1], [line2]) VALUES (3, N'Veszpr?m', N'8200', N'F?radt vagyok utca 11', N'3/A')
INSERT [Restaurant].[RestaurantAddress] ([addressID], [city], [zipcode], [line1], [line2]) VALUES (4, N'Veszprém', N'8200', N'Nem lopott Utca 27', N'ASD')
INSERT [Restaurant].[RestaurantAddress] ([addressID], [city], [zipcode], [line1], [line2]) VALUES (5, N'Veszprém', N'8200', N'Nem lopott Utca 27', N'ASD')
INSERT [Restaurant].[RestaurantAddress] ([addressID], [city], [zipcode], [line1], [line2]) VALUES (6, N'Veszprém', N'8200', N'Nem lopott Utca 27', N'ASD')
INSERT [Restaurant].[RestaurantAddress] ([addressID], [city], [zipcode], [line1], [line2]) VALUES (7, N'Veszprem', N'8200', N'Faradt vagyok utca v2.0', N'3/A')
INSERT [Restaurant].[RestaurantAddress] ([addressID], [city], [zipcode], [line1], [line2]) VALUES (8, N'Veszprém', N'8200', N'Nem lopott Utca 4', N'mit kell ide irni')
INSERT [Restaurant].[RestaurantAddress] ([addressID], [city], [zipcode], [line1], [line2]) VALUES (9, N'Veszprém', N'8200', N'Nem lopott Utca 4', N'mit kell ide irni')
INSERT [Restaurant].[RestaurantAddress] ([addressID], [city], [zipcode], [line1], [line2]) VALUES (10, N'Veszprém', N'8200', N'Nem lopott Utca 4', N'mit kell ide irni')
SET IDENTITY_INSERT [Restaurant].[RestaurantAddress] OFF
GO
INSERT [Users].[Users] ([username], [password], [lastName], [firstName], [phoneNumber], [addressID], [userType], [email]) VALUES (N'AAAAA', N'a55tal', N'Vnev', N'Knev', N'+3640887799', 19, 1, N'test@email.com')
INSERT [Users].[Users] ([username], [password], [lastName], [firstName], [phoneNumber], [addressID], [userType], [email]) VALUES (N'AsztalVok', N'a55tal', N'Asztal', N'Balázs', N'+36205544778', 4, 1, N'aaaaaa@aaaa.com')
INSERT [Users].[Users] ([username], [password], [lastName], [firstName], [phoneNumber], [addressID], [userType], [email]) VALUES (N'AsztalVok1149', N'pass', N'Étterem', N'Tulaj', N'+3640887799', 14, 1, N'test@email.com')
INSERT [Users].[Users] ([username], [password], [lastName], [firstName], [phoneNumber], [addressID], [userType], [email]) VALUES (N'AsztalVokMegint', N'a55tal', N'Asztal', N'Balázs', N'+36205544778', 11, 1, N'aaaaaa@aaaa.com')
INSERT [Users].[Users] ([username], [password], [lastName], [firstName], [phoneNumber], [addressID], [userType], [email]) VALUES (N'AsztalVokMegint2', N'a55tal2', N'Asztal', N'Béla', N'+36205589778', 13, 1, N'aaaaaa@aaaa.com')
INSERT [Users].[Users] ([username], [password], [lastName], [firstName], [phoneNumber], [addressID], [userType], [email]) VALUES (N'Hiiiii', N'Jelszoo', N'Pistavok', N'Tscoo', N'+36214563217', 15, 1, N'aasd@gmail.com')
INSERT [Users].[Users] ([username], [password], [lastName], [firstName], [phoneNumber], [addressID], [userType], [email]) VALUES (N'icuska00', N'testpassword', N'Kukor', N'Ica', N'+36308565471', 2, 0, N'aaaaaa@aaaa.com')
INSERT [Users].[Users] ([username], [password], [lastName], [firstName], [phoneNumber], [addressID], [userType], [email]) VALUES (N'mennyiAsztal', N'a55tal2', N'Asztal', N'Béla', N'+36205589778', 16, 1, N'klsad@asd.com')
INSERT [Users].[Users] ([username], [password], [lastName], [firstName], [phoneNumber], [addressID], [userType], [email]) VALUES (N'testRestaurantOwner', N'r3staurant', N'Rest', N'Aurant', N'+36204984199', 3, 1, N'aaaaaa@aaaa.com')
INSERT [Users].[Users] ([username], [password], [lastName], [firstName], [phoneNumber], [addressID], [userType], [email]) VALUES (N'testUser', N't3stpassword', N'Teszt', N'Elek', N'+36308598202', 1, 0, N'aaaaaa@aaaa.com')
INSERT [Users].[Users] ([username], [password], [lastName], [firstName], [phoneNumber], [addressID], [userType], [email]) VALUES (N'userFromClient', N'ass', N'Flex', N'Elek', N'+3699145825', 12, 1, N'aaaaaa@aaaa.com')
GO
SET IDENTITY_INSERT [Users].[UsersAddress] ON 

INSERT [Users].[UsersAddress] ([addressID], [city], [zipcode], [line1], [line2]) VALUES (1, N'Veszprém', N'8200', N'József Attila utca 34/2', N'1072')
INSERT [Users].[UsersAddress] ([addressID], [city], [zipcode], [line1], [line2]) VALUES (2, N'Veszprém', N'8200', N'József Attila utca 34/2', N'1091')
INSERT [Users].[UsersAddress] ([addressID], [city], [zipcode], [line1], [line2]) VALUES (3, N'Veszprém', N'8200', N'Egri József utca 14', NULL)
INSERT [Users].[UsersAddress] ([addressID], [city], [zipcode], [line1], [line2]) VALUES (4, N'Veszprém', N'8200', N'Asztal u. 32', NULL)
INSERT [Users].[UsersAddress] ([addressID], [city], [zipcode], [line1], [line2]) VALUES (10, N'Veszprém', N'8200', N'Asztal u. 32', N'2/A')
INSERT [Users].[UsersAddress] ([addressID], [city], [zipcode], [line1], [line2]) VALUES (11, N'Veszprém', N'8200', N'Asztal u. 32', N'2/A')
INSERT [Users].[UsersAddress] ([addressID], [city], [zipcode], [line1], [line2]) VALUES (12, N'Veszprem', N'8200', N'Ass utca 6', N'2/A')
INSERT [Users].[UsersAddress] ([addressID], [city], [zipcode], [line1], [line2]) VALUES (13, N'Veszprém', N'8200', N'Asztal u. 99', N'8/A')
INSERT [Users].[UsersAddress] ([addressID], [city], [zipcode], [line1], [line2]) VALUES (14, N'Veszprém', N'8200', N'Nem lopott Utca 27', N'ASD')
INSERT [Users].[UsersAddress] ([addressID], [city], [zipcode], [line1], [line2]) VALUES (15, N'Veszprem', N'8200', N'Faradt vagyok utca v2.0', N'3/A')
INSERT [Users].[UsersAddress] ([addressID], [city], [zipcode], [line1], [line2]) VALUES (16, N'Veszprém', N'8200', N'Asztal u. 99', N'8/A')
INSERT [Users].[UsersAddress] ([addressID], [city], [zipcode], [line1], [line2]) VALUES (17, N'Veszprém', N'8200', N'Nem lopott Utca 4', N'mit kell ide irni')
INSERT [Users].[UsersAddress] ([addressID], [city], [zipcode], [line1], [line2]) VALUES (18, N'Veszprém', N'8200', N'Nem lopott Utca 4', N'mit kell ide irni')
INSERT [Users].[UsersAddress] ([addressID], [city], [zipcode], [line1], [line2]) VALUES (19, N'Veszprém', N'8200', N'Nem lopott Utca 4', N'mit kell ide irni')
SET IDENTITY_INSERT [Users].[UsersAddress] OFF
GO
ALTER TABLE [Restaurant].[Allergens]  WITH CHECK ADD FOREIGN KEY([allergenID])
REFERENCES [Restaurant].[AllergenNames] ([allergenID])
GO
ALTER TABLE [Restaurant].[Allergens]  WITH CHECK ADD FOREIGN KEY([foodID])
REFERENCES [Restaurant].[Food] ([foodID])
GO
ALTER TABLE [Restaurant].[Food]  WITH CHECK ADD FOREIGN KEY([categoryID])
REFERENCES [Restaurant].[CategoryName] ([categoryID])
GO
ALTER TABLE [Restaurant].[Food]  WITH CHECK ADD FOREIGN KEY([restaurantID])
REFERENCES [Restaurant].[Restaurant] ([restaurantID])
GO
ALTER TABLE [Restaurant].[Restaurant]  WITH CHECK ADD FOREIGN KEY([addressID])
REFERENCES [Restaurant].[RestaurantAddress] ([addressID])
ON DELETE CASCADE
GO
ALTER TABLE [Restaurant].[Restaurant]  WITH CHECK ADD FOREIGN KEY([openingHoursID])
REFERENCES [Restaurant].[OpeningHours] ([openingHoursID])
GO
ALTER TABLE [Restaurant].[Restaurant]  WITH CHECK ADD FOREIGN KEY([owner])
REFERENCES [Users].[Users] ([username])
ON DELETE SET NULL
GO
ALTER TABLE [Users].[Users]  WITH CHECK ADD FOREIGN KEY([addressID])
REFERENCES [Users].[UsersAddress] ([addressID])
ON DELETE CASCADE
GO
/****** Object:  StoredProcedure [dbo].[addCategoryToMenu]    Script Date: 2021-03-28 11:59:24 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[addCategoryToMenu] @categoryName nvarchar(30) 
AS
DECLARE @OutputTbl TABLE (ID INT)
INSERT INTO Restaurant.CategoryName(categoryName)
OUTPUT Inserted.categoryID	
INTO @OutputTbl(ID)
VALUES(@categoryName)
DECLARE @outputClientID INT
SELECT  @outputClientID = ID FROM @OutputTbl
RETURN @outputClientID
GO
/****** Object:  StoredProcedure [dbo].[registerRestaurant]    Script Date: 2021-03-28 11:59:24 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[registerRestaurant] @usernameParam nvarchar(50),@passParam nvarchar(30),
			@lastName nvarchar(50),@fistName nvarchar(50), @phoneNumber nvarchar(20), @email nvarchar(50),
			@nameParam nvarchar(50), @restaurantDescriptionParam nvarchar(200),@styleParam nvarchar(50),
			@_city nvarchar(50),@_zipcode nvarchar(4),@_line1 nvarchar(50),@_line2 nvarchar(50),
			@_fromHour INT, @_fromMinute INT, @_toHour INT, @_toMinute INT
AS
DECLARE @OutputTbl TABLE (addressID INT, OpeningID INT)
--Register User
EXEC registerUser @usernameParam,@passParam,@email,@_city,@_zipcode,@_line1,@_line2,@lastName,@fistName,@phoneNumber,'1'
--Restaurant Address
INSERT INTO Restaurant.RestaurantAddress(city,zipcode,line1,line2)
OUTPUT Inserted.addressID
INTO @OutputTbl(addressID)
VALUES(@_city, @_zipcode,@_line1,@_line2)
DECLARE @_addressID INT
SELECT  @_addressID = addressID FROM @OutputTbl
--Opening Hours
INSERT INTO Restaurant.OpeningHours(fromHour,fromMinute,toHour,toMinute)
OUTPUT Inserted.openingHoursID
INTO @OutputTbl(OpeningID)
VALUES(@_fromHour, @_fromMinute,@_toHour,@_toMinute)
DECLARE @_openingHoursID INT
SELECT  @_openingHoursID = OpeningID FROM @OutputTbl
--Restaurant
INSERT INTO Restaurant.Restaurant(owner,name,restaurantDescription,style,phoneNumber, email,addressID,openingHoursID)
VALUES(@usernameParam,@nameParam, @restaurantDescriptionParam,@styleParam,@phoneNumber,@email,@_addressID,@_openingHoursID)
GO
/****** Object:  StoredProcedure [dbo].[registerUser]    Script Date: 2021-03-28 11:59:24 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[registerUser] @usernameParam nvarchar(50),@passParam nvarchar(30), @_email nvarchar(30),
			@_city nvarchar(50),@_zipCode nvarchar(4),@_line1 nvarchar(50),@_line2 nvarchar(50),
			@_lastName nvarchar(50), @_firstName nvarchar(50),@phoneNumber nvarchar(20), @_userType int
AS
DECLARE @OutputTbl TABLE (ID INT)
INSERT INTO Users.UsersAddress(city,zipCode,line1,line2)
OUTPUT Inserted.addressID	--WHY DOEST IT START AT 10???
INTO @OutputTbl(ID)
VALUES(@_city, @_zipCode,@_line1,@_line2)
DECLARE @_addressID INT
SELECT  @_addressID = ID FROM @OutputTbl

INSERT INTO Users.Users VALUES(@usernameParam,@passParam,@_lastName,@_firstName,@phoneNumber,
@_addressID,@_userType,@_email)
GO
USE [master]
GO
ALTER DATABASE [Netpincer] SET  READ_WRITE 
GO
