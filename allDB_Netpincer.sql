USE [master]
GO
/****** Object:  Database [Netpincer]    Script Date: 2021. 05. 14. 16:58:14 ******/
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
/****** Object:  User [sa]    Script Date: 2021. 05. 14. 16:58:14 ******/
CREATE USER [sa] FOR LOGIN [DESKTOP-L0B7IJP\panna] WITH DEFAULT_SCHEMA=[db_owner]
GO
/****** Object:  Schema [DeliveryPerson]    Script Date: 2021. 05. 14. 16:58:14 ******/
CREATE SCHEMA [DeliveryPerson]
GO
/****** Object:  Schema [Restaurant]    Script Date: 2021. 05. 14. 16:58:14 ******/
CREATE SCHEMA [Restaurant]
GO
/****** Object:  Schema [Users]    Script Date: 2021. 05. 14. 16:58:14 ******/
CREATE SCHEMA [Users]
GO
/****** Object:  Table [Restaurant].[RestaurantAddress]    Script Date: 2021. 05. 14. 16:58:14 ******/
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
/****** Object:  Table [Restaurant].[OpeningHours]    Script Date: 2021. 05. 14. 16:58:14 ******/
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
/****** Object:  Table [Restaurant].[Restaurant]    Script Date: 2021. 05. 14. 16:58:14 ******/
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
	[approximateTime] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[restaurantID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  UserDefinedFunction [dbo].[getRestaurant]    Script Date: 2021. 05. 14. 16:58:14 ******/
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
/****** Object:  Table [Users].[UsersAddress]    Script Date: 2021. 05. 14. 16:58:14 ******/
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
/****** Object:  Table [Users].[Users]    Script Date: 2021. 05. 14. 16:58:14 ******/
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
/****** Object:  UserDefinedFunction [dbo].[getUser]    Script Date: 2021. 05. 14. 16:58:14 ******/
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
/****** Object:  Table [DeliveryPerson].[AssignDelivery]    Script Date: 2021. 05. 14. 16:58:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [DeliveryPerson].[AssignDelivery](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[deliveryPersonID] [int] NULL,
	[orderID] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [DeliveryPerson].[DeliveryPersonOrders]    Script Date: 2021. 05. 14. 16:58:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [DeliveryPerson].[DeliveryPersonOrders](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[username] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [DeliveryPerson].[WorkingHours]    Script Date: 2021. 05. 14. 16:58:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [DeliveryPerson].[WorkingHours](
	[workingHoursID] [int] IDENTITY(1,1) NOT NULL,
	[username] [nvarchar](50) NOT NULL,
	[fromHour] [int] NULL,
	[fromMinute] [int] NULL,
	[toHour] [int] NULL,
	[toMinute] [int] NULL,
	[workingDays] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[workingHoursID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Restaurant].[AllergenNames]    Script Date: 2021. 05. 14. 16:58:14 ******/
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
/****** Object:  Table [Restaurant].[Allergens]    Script Date: 2021. 05. 14. 16:58:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Restaurant].[Allergens](
	[allergenID] [int] NULL,
	[foodID] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [Restaurant].[CategoryName]    Script Date: 2021. 05. 14. 16:58:14 ******/
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
/****** Object:  Table [Restaurant].[Food]    Script Date: 2021. 05. 14. 16:58:14 ******/
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
	[discount] [float] NULL,
PRIMARY KEY CLUSTERED 
(
	[foodID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Restaurant].[Menu]    Script Date: 2021. 05. 14. 16:58:14 ******/
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
/****** Object:  Table [Restaurant].[Orders]    Script Date: 2021. 05. 14. 16:58:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Restaurant].[Orders](
	[orderID] [int] IDENTITY(1,1) NOT NULL,
	[restaurantID] [int] NULL,
	[username] [nvarchar](50) NOT NULL,
	[foods] [nvarchar](100) NOT NULL,
	[status] [int] NOT NULL,
	[startOrderTime] [nvarchar](50) NOT NULL,
	[endOrderTime] [nvarchar](50) NULL,
	[price] [float] NOT NULL,
	[ETA] [nvarchar](30) NULL,
PRIMARY KEY CLUSTERED 
(
	[orderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [DeliveryPerson].[AssignDelivery]  WITH CHECK ADD FOREIGN KEY([deliveryPersonID])
REFERENCES [DeliveryPerson].[DeliveryPersonOrders] ([id])
ON DELETE SET NULL
GO
ALTER TABLE [DeliveryPerson].[AssignDelivery]  WITH CHECK ADD FOREIGN KEY([orderID])
REFERENCES [Restaurant].[Orders] ([orderID])
ON DELETE SET NULL
GO
ALTER TABLE [DeliveryPerson].[DeliveryPersonOrders]  WITH CHECK ADD FOREIGN KEY([username])
REFERENCES [Users].[Users] ([username])
GO
ALTER TABLE [DeliveryPerson].[WorkingHours]  WITH CHECK ADD FOREIGN KEY([username])
REFERENCES [Users].[Users] ([username])
ON DELETE CASCADE
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
ALTER TABLE [Restaurant].[Orders]  WITH CHECK ADD FOREIGN KEY([restaurantID])
REFERENCES [Restaurant].[Restaurant] ([restaurantID])
ON DELETE CASCADE
GO
ALTER TABLE [Restaurant].[Orders]  WITH CHECK ADD FOREIGN KEY([username])
REFERENCES [Users].[Users] ([username])
ON DELETE CASCADE
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
/****** Object:  StoredProcedure [dbo].[addCategoryToMenu]    Script Date: 2021. 05. 14. 16:58:14 ******/
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
/****** Object:  StoredProcedure [dbo].[addFood]    Script Date: 2021. 05. 14. 16:58:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[addFood] @foodName nvarchar(30) ,@price int ,@rating float ,@categoryID int ,@restaurantID int,
@availableFrom nvarchar(30), @availableTo nvarchar(30), @discount float
AS
DECLARE @OutputTbl TABLE (ID INT)
INSERT INTO Restaurant.Food(name,price,rating,pictureID,categoryID,restaurantID,availableFrom,availableTo,discount)
OUTPUT Inserted.foodID	
INTO @OutputTbl(ID)
VALUES(@foodName,@price,@rating,NULL,@categoryID,@restaurantID,@availableFrom,@availableTo,@discount)
DECLARE @outputFoodID INT
SELECT  @outputFoodID = ID FROM @OutputTbl
RETURN @outputFoodID
GO
/****** Object:  StoredProcedure [dbo].[registerRestaurant]    Script Date: 2021. 05. 14. 16:58:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[registerRestaurant] @usernameParam nvarchar(50),@passParam nvarchar(30),
			@lastName nvarchar(50),@fistName nvarchar(50), @phoneNumber nvarchar(20), @email nvarchar(50),
			@nameParam nvarchar(50), @restaurantDescriptionParam nvarchar(200),@styleParam nvarchar(50),
			@_city nvarchar(50),@_zipcode nvarchar(4),@_line1 nvarchar(50),@_line2 nvarchar(50),
			@_fromHour INT, @_fromMinute INT, @_toHour INT, @_toMinute INT, @approxTime INT
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
INSERT INTO Restaurant.Restaurant(owner,name,restaurantDescription,style,phoneNumber,addressID,openingHoursID,approximateTime)
VALUES(@usernameParam,@nameParam, @restaurantDescriptionParam,@styleParam,@phoneNumber,@_addressID,@_openingHoursID,@approxTime)
GO
/****** Object:  StoredProcedure [dbo].[registerUser]    Script Date: 2021. 05. 14. 16:58:14 ******/
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
