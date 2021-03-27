USE Netpincer
GO

--TEST USERS
INSERT INTO Users.UsersAddress(city,zipCode,line1,line2) VALUES('Veszprem', '8200','Jozsef Attila utca 34/2','1072')
INSERT INTO Users.Users VALUES('testUser','t3stpassword','Teszt','Elek','+36308598202','1','1')

INSERT INTO Users.UsersAddress(city,zipCode,line1,line2) VALUES('Veszprem', '8200','Jozsef Attila utca 34/2','1091')
INSERT INTO Users.Users VALUES('icuska00','testpassword','Kukor','Ica','+36308565471','2','1')

SELECT * FROM Users.Users
SELECT * FROM Users.UsersAddress

--FUNCTIONS
--DROP FUNCTION IF EXISTS getUser
GO
CREATE FUNCTION getUser(@usernameParam nvarchar(20),@passParam nvarchar(20))
RETURNS TABLE AS
RETURN SELECT username, [password], lastName, firstName, phoneNumber, city, zipcode, line1,line2
FROM Users.Users AS [u]
JOIN Users.UsersAddress ON Users.UsersAddress.addressID=[u].addressID
WHERE username=@usernameParam AND password=@passParam
GO


--UPDATE 03.20.
--USER TYPE
-- 0 - Customer
-- 1 - Restaurant Owner
-- 2 - Delivery Person
USE Netpincer
--UPDATE Users.Users SET userType=0 

INSERT INTO Users.UsersAddress(city,zipCode,line1) VALUES('Veszprem', '8200','Egri Jozsef utca 14')
INSERT INTO Users.Users VALUES('testRestaurantOwner','r3staurant','Rest','Aurant','+36204984199','3','2')

DROP FUNCTION IF EXISTS getUser
GO
CREATE FUNCTION getUser(@usernameParam nvarchar(20),@passParam nvarchar(20), @userTypeParam int)
RETURNS TABLE AS
RETURN SELECT username, [password], lastName, firstName, phoneNumber, city, zipcode, line1, line2 ,userType, email
FROM Users.Users AS [u]
JOIN Users.UsersAddress ON Users.UsersAddress.addressID=[u].addressID
WHERE username=@usernameParam AND password=@passParam AND userType=@userTypeParam
GO

SELECT * FROM getUser('testUser','t3stpassword','1')
SELECT * FROM getUser('testRestaurantOwner', 'r3staurant', '1')



--REGISTER FUNCTION  modified:03.26.
USE Netpincer
DROP PROCEDURE IF EXISTS registerUser
GO
CREATE PROCEDURE registerUser @usernameParam nvarchar(50),@passParam nvarchar(30), @_email nvarchar(30),
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

EXEC registerUser 'mennyiAsztal','a55tal2','klsad@asd.com','Veszprém','8200','Asztal u. 99','8/A','Asztal','Béla','+36205589778','1'
SELECT * FROM Users.Users
SELECT* FROM Users.UsersAddress


--2021.03.26
Use Netpincer
DROP PROCEDURE IF EXISTS registerRestaurant
GO
/*CREATE PROCEDURE registerRestaurant @usernameParam nvarchar(50),@passParam nvarchar(30),
			@lastName nvarchar(50),@fistName nvarchar(50), @phoneNumber nvarchar(20), @email nvarchar(50),
			@nameParam nvarchar(50), @restaurantDescriptionParam nvarchar(200),@styleParam nvarchar(50),
			@_city nvarchar(50),@_zipcode nvarchar(4),@_line1 nvarchar(50),@_line2 nvarchar(50),
			@_fromHour INT, @_fromMinute INT, @_toHour INT, @_toMinute INT
AS
DECLARE @OutputTbl TABLE (addressID INT, menuID INT, OpeningID INT)
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
--Menu 
INSERT INTO Restaurant.Menu(categoryID)
OUTPUT Inserted.menuID
INTO @OutputTbl(menuID)
VALUES(NULL)
DECLARE @_menuID INT
SELECT  @_menuID = menuID FROM @OutputTbl
--Restaurant
INSERT INTO Restaurant.Restaurant(owner,name,restaurantDescription,style,phoneNumber, email,addressID,openingHoursID,menuID)
VALUES(@usernameParam,@nameParam, @restaurantDescriptionParam,@styleParam,@phoneNumber,@email,@_addressID,@_openingHoursID,@_menuID)
GO


EXEC registerRestaurant 'AsztalVok1149','pass','Étterem', 'Tulaj','+3640887799','test@email.com', 
				'Teszt Étterem', 'Nagyon szép eskü nem lopott', 'Teljesen egyedi',
				'Veszprém', '8200','Nem lopott Utca 27', 'ASD', '11','00','22','00'
*/
SELECT * FROM Restaurant.Restaurant
JOIN Restaurant.RestaurantAddress ON RestaurantAddress.addressID=Restaurant.addressID
--JOIN Restaurant.Menu ON Menu.menuID=Restaurant.menuID
JOIN Restaurant.OpeningHours ON OpeningHours.openingHoursID=Restaurant.openingHoursID

SELECT * FROM Users.Users
SELECT * FROM Users.UsersAddress

--03.27.
USE Netpincer
--SELECT username FROM Users.Users WHERE username = 'AsztalVok' AND userType='1'
--ALTER TABLE Restaurant.Category ADD menuID INT NOT NULL DEFAULT 0
--ALTER TABLE Restaurant.Category 
--DROP CONSTRAINT IF EXISTS DF__Category__menuID__3C34F16F
--ALTER TABLE Restaurant.Category DROP COLUMN IF EXISTS menuID 
--ALTER TABLE Restaurant.Restaurant ADD menuID2 INT IDENTITY(1,1) FOREIGN KEY REFERENCES Restaurant.Category(menuID) ON DELETE CASCADE

--NEW STUFF, BECAUSE I'M STARTING TO LOSE IT:
SELECT * FROM Restaurant.Category
ALTER TABLE Restaurant.Category ADD restaurantID INT FOREIGN KEY REFERENCES Restaurant.Restaurant(restaurantID) ON DELETE NO ACTION
SELECT * FROM Restaurant.Restaurant
ALTER TABLE Restaurant.Restaurant 
DROP CONSTRAINT IF EXISTS FK__Restauran__menuI__75A278F
ALTER TABLE Restaurant.Restaurant 
DROP CONSTRAINT IF EXISTS FK__Restauran__menuI__31EC6D26
ALTER TABLE Restaurant.Restaurant 
DROP CONSTRAINT IF EXISTS  FK__Restauran__menuI__75A278F5
ALTER TABLE Restaurant.Restaurant DROP COLUMN IF EXISTS menuID
DROP TABLE IF EXISTS Restaurant.Menu 
GO

--MODIFY PROCEDURE
Use Netpincer
DROP PROCEDURE IF EXISTS registerRestaurant
GO
CREATE PROCEDURE registerRestaurant @usernameParam nvarchar(50),@passParam nvarchar(30),
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
--TEST
EXEC registerRestaurant 'AAAAA','a55tal','Vnev', 'Knev','+3640887799','test@email.com', 
				'Teszt Étterem still', 'AAAAAAA', 'Teljesen egyedi v2',
				'Veszprém', '8200','Nem lopott Utca 4', 'mit kell ide irni', '11','00','22','00'





DROP PROCEDURE IF EXISTS addCategoryToMenu
GO
CREATE PROCEDURE addCategoryToMenu @username nvarchar(50), @userType int, @name nvarchar(50),
				@categoryName nvarchar(30) 
AS
DECLARE @OutputTbl TABLE (restID INT) 
INSERT INTO  @OutputTbl(restID) 
SELECT restaurantID FROM Restaurant.Restaurant as [r]
JOIN Users.Users ON Users.username = [r].[owner]
WHERE [r].[owner]=@username AND Users.userType=@userType AND r.name=@name
DECLARE @restID INT
SELECT  @restID = restID FROM @OutputTbl

INSERT INTO Restaurant.Category(name,restaurantID)
VALUES(@categoryName,@restID)
GO


SELECT * FROM Restaurant.Restaurant
SELECT * FROM Users.Users
SELECT * FROM Restaurant.Category AS [c]
JOIN Restaurant.Restaurant ON Restaurant.restaurantID = [c].restaurantID
EXEC addCategoryToMenu 'AsztalVokMegint', '1', 'Teszt Étterem', 'Teszta' 
EXEC addCategoryToMenu 'AsztalVokMegint', '1', 'Teszt Étterem', 'Pizza' 
SELECT * FROM Restaurant.Category AS [c]
JOIN Restaurant.Restaurant ON Restaurant.restaurantID = [c].restaurantID

SELECT [c].[name] FROM Restaurant.Category AS [c]
JOIN Restaurant.Restaurant as [r] ON [r].restaurantID = [c].restaurantID
WHERE [r].[name] = 'Teszt Étterem' --@restaurantName--


-- 2020.03.27. 19:28
--ALLERGENS AND CATEGORY
USE Netpincer
DROP TABLE IF EXISTS Restaurant.Category
CREATE TABLE Restaurant.CategoryName
(
    categoryID INT IDENTITY PRIMARY KEY,
    categoryName nvarchar(20)
);
GO
SELECT * FROM Restaurant.Food
ALTER TABLE Restaurant.Food ADD categoryID INT 
FOREIGN KEY REFERENCES Restaurant.CategoryName(categoryID)

--EXEC sp_rename 'Restaurant.Food.CategoryID', 'categoryID', 'COLUMN';
ALTER TABLE Restaurant.Food ADD restaurantID INT 
FOREIGN KEY REFERENCES Restaurant.Restaurant(restaurantID)

DROP TABLE IF EXISTS Restaurant.Allergens
CREATE TABLE Restaurant.AllergenNames
(
    allergenID INT IDENTITY PRIMARY KEY,
    name NVARCHAR(20)
)
GO
CREATE TABLE Restaurant.Allergens
(
    allergenID INT FOREIGN KEY REFERENCES Restaurant.AllergenNames(allergenID),
    foodID INT FOREIGN KEY REFERENCES Restaurant.Food(foodID)
)
GO