
--RENDSZERFEJLESZTES PROJECT DATABASE V1 2021.03.15.

use Netpincer
GO
--RESET--
/*DROP TABLE IF EXISTS Restaurant.Restaurant
DROP TABLE IF EXISTS Restaurant.RestaurantAddress
DROP TABLE IF EXISTS Restaurant.OpeningHours
DROP TABLE IF EXISTS Restaurant.Menu
DROP TABLE IF EXISTS Restaurant.Category
DROP TABLE IF EXISTS Restaurant.Food
DROP TABLE IF EXISTS Users.Users
DROP TABLE IF EXISTS Users.UsersAddress
DROP SCHEMA IF EXISTS Restaurant
DROP SCHEMA IF EXISTS Users
GO*/

--Lehet aláhúzza törlés után is, de elvileg futni fog


--RESTAURANT
GO
CREATE SCHEMA Restaurant
GO

CREATE TABLE Restaurant.RestaurantAddress
(
	addressID INT IDENTITY PRIMARY KEY,
	city NVARCHAR(50) NOT NULL,
	zipcode CHAR(4) NOT NULL,
	line1 NVARCHAR(100) NOT NULL,
	line2 NVARCHAR(100) NOT NULL
);
GO
CREATE TABLE Restaurant.OpeningHours
(
	openingHoursID INT IDENTITY PRIMARY KEY,
	fromHour INT,
	fromMinute INT,
	toHour INT,
	toMinute INT
);
GO
CREATE TABLE Restaurant.Food
(
	foodID INT IDENTITY PRIMARY KEY,
	name NVARCHAR(60) NOT NULL,
	price FLOAT NOT NULL,
	rating FLOAT,
	pictureID INT
);
GO
CREATE TABLE Restaurant.Category
(
	categoryID INT IDENTITY PRIMARY KEY,
	name NVARCHAR(30) NOT NULL,
	foodID INT FOREIGN KEY REFERENCES Restaurant.Food(foodID) ON DELETE CASCADE	
);
GO
CREATE TABLE Restaurant.Menu	--CAN INSERT EMPTY MENU
(
	menuID INT IDENTITY PRIMARY KEY,
	categoryID INT FOREIGN KEY REFERENCES Restaurant.Category(categoryID) ON DELETE CASCADE	
	
);
GO
CREATE TABLE Restaurant.Restaurant
(
	restaurantID INT IDENTITY PRIMARY KEY,
	name NVARCHAR(50) NOT NULL,
	addressID INT FOREIGN KEY REFERENCES Restaurant.RestaurantAddress(addressID) ON DELETE CASCADE NOT NULL,
	openingHoursID INT FOREIGN KEY REFERENCES Restaurant.OpeningHours(openingHoursID) ON DELETE NO ACTION,
	restaurantDescription NVARCHAR(200) NOT NULL,
	style NVARCHAR(50) NOT NULL,
	menuID INT FOREIGN KEY REFERENCES Restaurant.Menu(menuID) ON DELETE CASCADE,
);
GO



--USERS
GO
CREATE SCHEMA Users
GO
CREATE TABLE Users.UsersAddress
(
	addressID INT IDENTITY PRIMARY KEY,
	city NVARCHAR(50) NOT NULL,
	zipcode CHAR(4) NOT NULL,
	line1 NVARCHAR(50) NOT NULL,
	line2 NVARCHAR(50) NOT NULL
);
GO
CREATE TABLE Users.Users
(
	username NVARCHAR(50) PRIMARY KEY,
	password NVARCHAR(30) NOT NULL,
	lastName NVARCHAR(50) NOT NULL,
	firstName NVARCHAR(50) NOT NULL,
	phoneNumber NVARCHAR(20) NOT NULL,
	addressID INT FOREIGN KEY REFERENCES Users.UsersAddress(addressID) ON DELETE CASCADE NOT NULL
);
GO


--USERTYPE ADDED TO USER 03.20.
--LINE2 CAN BE NULL
ALTER TABLE Users.Users ADD userType int NOT NULL
GO
ALTER TABLE Users.UsersAddress ALTER COLUMN line2 nvarchar(20)

