USE [master]
GO
/****** Object:  Database [Netpincer]    Script Date: 2021. 03. 31. 16:15:38 ******/
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
/****** Object:  User [sa]    Script Date: 2021. 03. 31. 16:15:38 ******/
CREATE USER [sa] FOR LOGIN [DESKTOP-L0B7IJP\panna] WITH DEFAULT_SCHEMA=[db_owner]
GO
/****** Object:  Schema [Restaurant]    Script Date: 2021. 03. 31. 16:15:38 ******/
CREATE SCHEMA [Restaurant]
GO
/****** Object:  Schema [Users]    Script Date: 2021. 03. 31. 16:15:38 ******/
CREATE SCHEMA [Users]
GO
/****** Object:  Table [Restaurant].[RestaurantAddress]    Script Date: 2021. 03. 31. 16:15:38 ******/
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
/****** Object:  Table [Restaurant].[OpeningHours]    Script Date: 2021. 03. 31. 16:15:38 ******/
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
/****** Object:  Table [Restaurant].[Restaurant]    Script Date: 2021. 03. 31. 16:15:38 ******/
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
/****** Object:  UserDefinedFunction [dbo].[getRestaurant]    Script Date: 2021. 03. 31. 16:15:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[getRestaurant](@username nvarchar(20))
RETURNS TABLE AS
RETURN SELECT restaurantID,name,restaurantDescription,style,owner,phoneNumber, city,zipcode,line1,line2, fromHour,fromMinute,toHour,toMinute
FROM Restaurant.Restaurant
JOIN Restaurant.RestaurantAddress ON Restaurant.RestaurantAddress.addressID = Restaurant.addressID
JOIN Restaurant.OpeningHours ON Restaurant.OpeningHours.openingHoursID = Restaurant.openingHoursID
WHERE Restaurant.owner=@username
GO
/****** Object:  Table [Users].[UsersAddress]    Script Date: 2021. 03. 31. 16:15:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Users].[UsersAddress](
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
/****** Object:  Table [Users].[Users]    Script Date: 2021. 03. 31. 16:15:38 ******/
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
/****** Object:  UserDefinedFunction [dbo].[getUser]    Script Date: 2021. 03. 31. 16:15:38 ******/
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
/****** Object:  Table [Restaurant].[AllergenNames]    Script Date: 2021. 03. 31. 16:15:38 ******/
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
/****** Object:  Table [Restaurant].[Allergens]    Script Date: 2021. 03. 31. 16:15:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Restaurant].[Allergens](
	[allergenID] [int] NULL,
	[foodID] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [Restaurant].[CategoryName]    Script Date: 2021. 03. 31. 16:15:38 ******/
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
/****** Object:  Table [Restaurant].[Food]    Script Date: 2021. 03. 31. 16:15:38 ******/
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
	[restaurantID] [int] NOT NULL,
	[availableFrom] [nvarchar](20) NULL,
	[availableTo] [nvarchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[foodID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Restaurant].[Menu]    Script Date: 2021. 03. 31. 16:15:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Restaurant].[Menu](
	[menuID] [int] IDENTITY(1,1) NOT NULL,
	[restaurantID] [int] NULL,
	[categoryID] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[menuID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [Restaurant].[AllergenNames] ON 

INSERT [Restaurant].[AllergenNames] ([allergenID], [name]) VALUES (1, N'Liszt')
INSERT [Restaurant].[AllergenNames] ([allergenID], [name]) VALUES (2, N'Gluten')
INSERT [Restaurant].[AllergenNames] ([allergenID], [name]) VALUES (3, N'Cukor')
INSERT [Restaurant].[AllergenNames] ([allergenID], [name]) VALUES (4, N'Laktoz')
INSERT [Restaurant].[AllergenNames] ([allergenID], [name]) VALUES (5, N'Tojas')
INSERT [Restaurant].[AllergenNames] ([allergenID], [name]) VALUES (6, N'Diofelek')
INSERT [Restaurant].[AllergenNames] ([allergenID], [name]) VALUES (7, N'Foldimogyoro')
INSERT [Restaurant].[AllergenNames] ([allergenID], [name]) VALUES (8, N'Eper')
INSERT [Restaurant].[AllergenNames] ([allergenID], [name]) VALUES (9, N'Malna')
INSERT [Restaurant].[AllergenNames] ([allergenID], [name]) VALUES (10, N'Kiwi')
INSERT [Restaurant].[AllergenNames] ([allergenID], [name]) VALUES (11, N'Paradicsom')
INSERT [Restaurant].[AllergenNames] ([allergenID], [name]) VALUES (12, N'Kagylo')
SET IDENTITY_INSERT [Restaurant].[AllergenNames] OFF
GO
INSERT [Restaurant].[Allergens] ([allergenID], [foodID]) VALUES (4, 3)
INSERT [Restaurant].[Allergens] ([allergenID], [foodID]) VALUES (2, 10)
INSERT [Restaurant].[Allergens] ([allergenID], [foodID]) VALUES (2, 5)
INSERT [Restaurant].[Allergens] ([allergenID], [foodID]) VALUES (2, 6)
INSERT [Restaurant].[Allergens] ([allergenID], [foodID]) VALUES (2, 7)
INSERT [Restaurant].[Allergens] ([allergenID], [foodID]) VALUES (2, 8)
INSERT [Restaurant].[Allergens] ([allergenID], [foodID]) VALUES (2, 9)
INSERT [Restaurant].[Allergens] ([allergenID], [foodID]) VALUES (5, 9)
GO
SET IDENTITY_INSERT [Restaurant].[CategoryName] ON 

INSERT [Restaurant].[CategoryName] ([categoryID], [categoryName]) VALUES (1, N'Pizzak')
INSERT [Restaurant].[CategoryName] ([categoryID], [categoryName]) VALUES (2, N'Levesek')
INSERT [Restaurant].[CategoryName] ([categoryID], [categoryName]) VALUES (3, N'Foetelek')
INSERT [Restaurant].[CategoryName] ([categoryID], [categoryName]) VALUES (4, N'Husetelek')
SET IDENTITY_INSERT [Restaurant].[CategoryName] OFF
GO
SET IDENTITY_INSERT [Restaurant].[Food] ON 

INSERT [Restaurant].[Food] ([foodID], [name], [price], [rating], [pictureID], [categoryID], [restaurantID], [availableFrom], [availableTo]) VALUES (1, N'Hawaii pizza', 1500, 0, NULL, 1, 1, N'2021. 06. 01.', N'2021. 09. 01.')
INSERT [Restaurant].[Food] ([foodID], [name], [price], [rating], [pictureID], [categoryID], [restaurantID], [availableFrom], [availableTo]) VALUES (2, N'Magyaros pizza', 1600, 0, NULL, 1, 1, N'', N'')
INSERT [Restaurant].[Food] ([foodID], [name], [price], [rating], [pictureID], [categoryID], [restaurantID], [availableFrom], [availableTo]) VALUES (3, N'Sajtkrem leves', 800, 0, NULL, 2, 1, N'', N'')
INSERT [Restaurant].[Food] ([foodID], [name], [price], [rating], [pictureID], [categoryID], [restaurantID], [availableFrom], [availableTo]) VALUES (4, N'Gulyas leves', 1000, 0, NULL, 2, 1, N'', N'')
INSERT [Restaurant].[Food] ([foodID], [name], [price], [rating], [pictureID], [categoryID], [restaurantID], [availableFrom], [availableTo]) VALUES (5, N'Cordon Blue', 2000, 0, NULL, 3, 1, N'', N'')
INSERT [Restaurant].[Food] ([foodID], [name], [price], [rating], [pictureID], [categoryID], [restaurantID], [availableFrom], [availableTo]) VALUES (6, N'Magyaros pizza', 1700, 0, NULL, 1, 2, N'', N'')
INSERT [Restaurant].[Food] ([foodID], [name], [price], [rating], [pictureID], [categoryID], [restaurantID], [availableFrom], [availableTo]) VALUES (7, N'Songoku', 1800, 0, NULL, 1, 2, N'2021. 04. 01.', N'2021. 08. 31.')
INSERT [Restaurant].[Food] ([foodID], [name], [price], [rating], [pictureID], [categoryID], [restaurantID], [availableFrom], [availableTo]) VALUES (8, N'Rantott serteskaraj', 1600, 0, NULL, 4, 2, N'', N'')
INSERT [Restaurant].[Food] ([foodID], [name], [price], [rating], [pictureID], [categoryID], [restaurantID], [availableFrom], [availableTo]) VALUES (9, N'Rantott csirkemell', 1000, 0, NULL, 4, 2, N'', N'')
INSERT [Restaurant].[Food] ([foodID], [name], [price], [rating], [pictureID], [categoryID], [restaurantID], [availableFrom], [availableTo]) VALUES (10, N'Magyaros pizza', 1400, 0, NULL, 1, 3, N'2021. 03. 31.', N'2021. 04. 22.')
SET IDENTITY_INSERT [Restaurant].[Food] OFF
GO
SET IDENTITY_INSERT [Restaurant].[Menu] ON 

INSERT [Restaurant].[Menu] ([menuID], [restaurantID], [categoryID]) VALUES (1, 1, 1)
INSERT [Restaurant].[Menu] ([menuID], [restaurantID], [categoryID]) VALUES (2, 1, 2)
INSERT [Restaurant].[Menu] ([menuID], [restaurantID], [categoryID]) VALUES (3, 1, 3)
INSERT [Restaurant].[Menu] ([menuID], [restaurantID], [categoryID]) VALUES (4, 2, 1)
INSERT [Restaurant].[Menu] ([menuID], [restaurantID], [categoryID]) VALUES (5, 2, 4)
INSERT [Restaurant].[Menu] ([menuID], [restaurantID], [categoryID]) VALUES (6, 3, 1)
INSERT [Restaurant].[Menu] ([menuID], [restaurantID], [categoryID]) VALUES (7, 3, 2)
SET IDENTITY_INSERT [Restaurant].[Menu] OFF
GO
SET IDENTITY_INSERT [Restaurant].[OpeningHours] ON 

INSERT [Restaurant].[OpeningHours] ([openingHoursID], [fromHour], [fromMinute], [toHour], [toMinute]) VALUES (1, 8, 0, 16, 0)
INSERT [Restaurant].[OpeningHours] ([openingHoursID], [fromHour], [fromMinute], [toHour], [toMinute]) VALUES (2, 8, 0, 16, 0)
INSERT [Restaurant].[OpeningHours] ([openingHoursID], [fromHour], [fromMinute], [toHour], [toMinute]) VALUES (3, 8, 0, 16, 0)
SET IDENTITY_INSERT [Restaurant].[OpeningHours] OFF
GO
SET IDENTITY_INSERT [Restaurant].[Restaurant] ON 

INSERT [Restaurant].[Restaurant] ([restaurantID], [name], [addressID], [openingHoursID], [restaurantDescription], [style], [owner], [phoneNumber]) VALUES (1, N'Marica', 1, 1, N'Minden jo egy helyen', N'Modern, letisztult', N'marica', N'36-201111111')
INSERT [Restaurant].[Restaurant] ([restaurantID], [name], [addressID], [openingHoursID], [restaurantDescription], [style], [owner], [phoneNumber]) VALUES (2, N'Nagyi sutodeje', 2, 2, N'Finomat olcson, gyorsan', N'Hagyomanyos, izes, fuszeres', N'nagyi', N'36-204433665')
INSERT [Restaurant].[Restaurant] ([restaurantID], [name], [addressID], [openingHoursID], [restaurantDescription], [style], [owner], [phoneNumber]) VALUES (3, N'finom pizza', 3, 3, N'finom sdaf ', N'modern', N'pizza', N'36-201111111')
SET IDENTITY_INSERT [Restaurant].[Restaurant] OFF
GO
SET IDENTITY_INSERT [Restaurant].[RestaurantAddress] ON 

INSERT [Restaurant].[RestaurantAddress] ([addressID], [city], [zipcode], [line1], [line2]) VALUES (1, N'Veszprem', N'8200', N'Kossuth utca', N'2')
INSERT [Restaurant].[RestaurantAddress] ([addressID], [city], [zipcode], [line1], [line2]) VALUES (2, N'Veszprem', N'8200', N'Ady Endre utca', N'12')
INSERT [Restaurant].[RestaurantAddress] ([addressID], [city], [zipcode], [line1], [line2]) VALUES (3, N'Veszprem', N'8200', N'Kossuth utca', N'12')
SET IDENTITY_INSERT [Restaurant].[RestaurantAddress] OFF
GO
INSERT [Users].[Users] ([username], [password], [lastName], [firstName], [phoneNumber], [addressID], [userType], [email]) VALUES (N'katalin01', N'1234', N'Nagy', N'Katalin', N'36-202033554', 4, 0, N'katalin01@gmail.com')
INSERT [Users].[Users] ([username], [password], [lastName], [firstName], [phoneNumber], [addressID], [userType], [email]) VALUES (N'marica', N'marica', N'Kiss', N'Peter', N'36-201111111', 1, 1, N'marica@marica.com')
INSERT [Users].[Users] ([username], [password], [lastName], [firstName], [phoneNumber], [addressID], [userType], [email]) VALUES (N'nagyi', N'nagyi', N'Takacs', N'Petra', N'36-204433665', 3, 1, N'nagyi@nagyi.com')
INSERT [Users].[Users] ([username], [password], [lastName], [firstName], [phoneNumber], [addressID], [userType], [email]) VALUES (N'pistike', N'1234', N'Ehes', N'Pista', N'36-201111111', 2, 0, N'pista@gmail.com')
INSERT [Users].[Users] ([username], [password], [lastName], [firstName], [phoneNumber], [addressID], [userType], [email]) VALUES (N'pizza', N'1234', N'Pizza', N'Peter', N'36-201111111', 5, 1, N'pizza@pizza.com')
GO
SET IDENTITY_INSERT [Users].[UsersAddress] ON 

INSERT [Users].[UsersAddress] ([addressID], [city], [zipcode], [line1], [line2]) VALUES (1, N'Veszprem', N'8200', N'Kossuth utca', N'2')
INSERT [Users].[UsersAddress] ([addressID], [city], [zipcode], [line1], [line2]) VALUES (2, N'Veszprem', N'8200', N'Egyetem utca 2', N'2 emelet / 1 ajto')
INSERT [Users].[UsersAddress] ([addressID], [city], [zipcode], [line1], [line2]) VALUES (3, N'Veszprem', N'8200', N'Ady Endre utca', N'12')
INSERT [Users].[UsersAddress] ([addressID], [city], [zipcode], [line1], [line2]) VALUES (4, N'Veszprem', N'8200', N'Hazgyari ut 12', N'Emelet / Ajt?')
INSERT [Users].[UsersAddress] ([addressID], [city], [zipcode], [line1], [line2]) VALUES (5, N'Veszprem', N'8200', N'Kossuth utca', N'12')
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
ALTER TABLE [Restaurant].[Menu]  WITH CHECK ADD FOREIGN KEY([categoryID])
REFERENCES [Restaurant].[CategoryName] ([categoryID])
ON DELETE SET NULL
GO
ALTER TABLE [Restaurant].[Menu]  WITH CHECK ADD FOREIGN KEY([restaurantID])
REFERENCES [Restaurant].[Restaurant] ([restaurantID])
ON DELETE SET NULL
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
/****** Object:  StoredProcedure [dbo].[addCategoryToMenu]    Script Date: 2021. 03. 31. 16:15:38 ******/
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
/****** Object:  StoredProcedure [dbo].[addFood]    Script Date: 2021. 03. 31. 16:15:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[addFood] @foodName nvarchar(30) ,@price int ,@rating float ,@categoryID int ,@restaurantID int,
@availableFrom nvarchar(30), @availableTo nvarchar(30)
AS
DECLARE @OutputTbl TABLE (ID INT)
INSERT INTO Restaurant.Food(name,price,rating,pictureID,categoryID,restaurantID,availableFrom,availableTo)
OUTPUT Inserted.foodID	
INTO @OutputTbl(ID)
VALUES(@foodName,@price,@rating,NULL,@categoryID,@restaurantID,@availableFrom,@availableTo)
DECLARE @outputFoodID INT
SELECT  @outputFoodID = ID FROM @OutputTbl
RETURN @outputFoodID
GO
/****** Object:  StoredProcedure [dbo].[registerRestaurant]    Script Date: 2021. 03. 31. 16:15:38 ******/
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
INSERT INTO Restaurant.Restaurant(owner,name,restaurantDescription,style,phoneNumber,addressID,openingHoursID)
VALUES(@usernameParam,@nameParam, @restaurantDescriptionParam,@styleParam,@phoneNumber,@_addressID,@_openingHoursID)
GO
/****** Object:  StoredProcedure [dbo].[registerUser]    Script Date: 2021. 03. 31. 16:15:38 ******/
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
@_addressID,@_userType, @_email)
GO
USE [master]
GO
ALTER DATABASE [Netpincer] SET  READ_WRITE 
GO
