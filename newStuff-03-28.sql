------------------------------------------------------------------------------
----------------------AFTER YOU RUN THE GENERATING SCRIPT RUN THIS------------
------------------------------------------------------------------------------

----------------------------------03.28..-------------------------------------
USE Netpincer
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
INSERT INTO Restaurant.Restaurant(owner,name,restaurantDescription,style,phoneNumber,addressID,openingHoursID)
VALUES(@usernameParam,@nameParam, @restaurantDescriptionParam,@styleParam,@phoneNumber,@_addressID,@_openingHoursID)
GO

USE Netpincer
SELECT * FROM Restaurant.Food
ALTER TABLE Restaurant.Food DROP COLUMN restaurantID
ALTER TABLE Restaurant.Food DROP CONSTRAINT FK__Food__restaurant__503BEA1C
ALTER TABLE Restaurant.Food DROP COLUMN IF EXISTS restaurantID

DROP TABLE IF EXISTS Restaurant.Menu
CREATE TABLE Restaurant.Menu
(
	menuID INT IDENTITY(1,1) PRIMARY KEY,
	restaurantID INT FOREIGN KEY REFERENCES Restaurant.Restaurant(restaurantID) ON DELETE SET NULL,
	categoryID INT FOREIGN KEY REFERENCES Restaurant.CategoryName(categoryID) ON DELETE SET NULL
)

--DELETE EVERYTHING AND RESET PRIMARY KEY TO 1
--DELETE FROM Restaurant.CategoryName
--DBCC CHECKIDENT ('Restaurant.CategoryName', RESEED, 0) 

--SOCKET SERVER: SELECT categoryID FROM Restaurant.CategoryName WHERE categoryName = @_categoryName
--SERVER: INSERT INTO Restaurant.Menu(restaurantID,categoryID) VALUES(1,2)
--SERVER: SELECT restaurantID FROM Restaurant.Restaurant WHERE owner=@username
--SERVER: SELECT menuID FROM Restaurant.Menu WHERE restaurantID=@restID AND categoryID=@catID
--USE Netpincer
SELECT * FROM Restaurant.Restaurant
SELECT * FROM Restaurant.CategoryName

--DELETE EVERYTHING AND RESET PRIMARY KEY TO 1
--DELETE FROM Restaurant.Menu
--DBCC CHECKIDENT ('Restaurant.Menu', RESEED, 0) 

--SELECT * FROM Restaurant.OpeningHours

GO
CREATE FUNCTION getRestaurant(@username nvarchar(20))
RETURNS TABLE AS
RETURN SELECT restaurantID,name,restaurantDescription,style,owner,phoneNumber, city,zipcode,line1,line2, fromHour,fromMinute,toHour,toMinute
FROM Restaurant.Restaurant
JOIN Restaurant.RestaurantAddress ON Restaurant.RestaurantAddress.addressID = Restaurant.addressID
JOIN Restaurant.OpeningHours ON Restaurant.OpeningHours.openingHoursID = Restaurant.openingHoursID
WHERE Restaurant.owner=@username
GO

SELECT * FROM Restaurant.Menu
Use Netpincer
--SERVER:
--SELECT Restaurant.Menu.categoryID,categoryName FROM Restaurant.CategoryName 
--JOIN Restaurant.Menu ON Restaurant.Menu.categoryID = Restaurant.CategoryName.categoryID
--WHERE Restaurant.Menu.restaurantID = @restaurantID

SELECT * FROM Restaurant.Menu
SELECT * FROM Restaurant.CategoryName
INSERT INTO Restaurant.CategoryName(categoryName) VALUES('Pizza')
INSERT INTO Restaurant.CategoryName(categoryName) VALUES('Teszta')
INSERT INTO Restaurant.Menu(restaurantID,categoryID) VALUES(7,3)

SELECT * FROM Restaurant.Food
-----DELETE EVERYTHING AND RESET PRIMARY KEY TO 1
--DELETE FROM Restaurant.Food
--DBCC CHECKIDENT ('Restaurant.Food', RESEED, 0) 
ALTER TABLE Restaurant.Food ADD restaurantID INT FOREIGN KEY REFERENCES Restaurant.Restaurant(restaurantID) NOT NULL
--LEVES
INSERT INTO Restaurant.Food(name,price,rating,pictureID,categoryID,restaurantID)
VALUES('Husleves',1200,4.5,NULL,1,7)
INSERT INTO Restaurant.Food(name,price,rating,pictureID,categoryID,restaurantID)
VALUES('Krumpli leves',1200,4.3,NULL,1,7)
INSERT INTO Restaurant.Food(name,price,rating,pictureID,categoryID,restaurantID)
VALUES('Brokkoli leves',1200,4.7,NULL,1,8)
--K?RET
INSERT INTO Restaurant.Food(name,price,rating,pictureID,categoryID,restaurantID)
VALUES('Krumpli',800,4.3,NULL,2,7)
INSERT INTO Restaurant.Food(name,price,rating,pictureID,categoryID,restaurantID)
VALUES('Rizs',700,4.7,NULL,2,7)
--PIZZA
INSERT INTO Restaurant.Food(name,price,rating,pictureID,categoryID,restaurantID)
VALUES('Sajtos pizza',1600,4.3,NULL,3,8)
INSERT INTO Restaurant.Food(name,price,rating,pictureID,categoryID,restaurantID)
VALUES('Sonkas pizza',1700,4.7,NULL,3,7)
--SERVER:
SELECT foodID,name,price,rating,pictureID
FROM Restaurant.Food 
JOIN Restaurant.CategoryName ON Restaurant.CategoryName.categoryID= Restaurant.Food.categoryID
WHERE Restaurant.Food.restaurantID = '7' AND Restaurant.Food.categoryID='1'

SELECT * FROM Restaurant.AllergenNames
INSERT INTO Restaurant.AllergenNames(name) VALUES('Liszt')
INSERT INTO Restaurant.AllergenNames(name) VALUES('Gluten')
INSERT INTO Restaurant.AllergenNames(name) VALUES('Cukor')
DELETE FROM Restaurant.AllergenNames
	WHERE allergenID = 3
SELECT * FROM Restaurant.Allergens
INSERT INTO Restaurant.Allergens VALUES(1,2)
INSERT INTO Restaurant.Allergens VALUES(2,2)
INSERT INTO Restaurant.Allergens VALUES(3,2)
INSERT INTO Restaurant.Allergens VALUES(1,7)
INSERT INTO Restaurant.Allergens VALUES(2,7)
INSERT INTO Restaurant.Allergens VALUES(3,7)
INSERT INTO Restaurant.Allergens VALUES(1,1)

SELECT Restaurant.AllergenNames.name 
FROM Restaurant.AllergenNames
JOIN Restaurant.Allergens ON Restaurant.Allergens.allergenID = Restaurant.AllergenNames.allergenID
JOIN Restaurant.Food ON Restaurant.Food.foodID = Restaurant.Allergens.foodID
WHERE Restaurant.Allergens.foodID = '1'

--03.29.
--GET RESTAURANT LIST
--SERVER:
/*SELECT restaurantID,name,restaurantDescription,style,owner,phoneNumber, city,zipcode,line1,line2, fromHour,fromMinute,toHour,toMinute
FROM Restaurant.Restaurant
JOIN Restaurant.RestaurantAddress ON Restaurant.RestaurantAddress.addressID = Restaurant.addressID
JOIN Restaurant.OpeningHours ON Restaurant.OpeningHours.openingHoursID = Restaurant.openingHoursID*/


ALTER TABLE Restaurant.Food ADD [availableFrom]	NVARCHAR(20) --Mert nem akarok pontokat konvert?lgatni
ALTER TABLE Restaurant.Food ADD [availableTo]	NVARCHAR(20) 
/*SERVER:						
SELECT foodID,name,price,rating,pictureID,availableFrom,availableTo FROM Restaurant.Food JOIN Restaurant.CategoryName ON Restaurant.CategoryName.categoryID = Restaurant.Food.categoryID 
WHERE Restaurant.Food.restaurantID = '7' AND Restaurant.Food.categoryID = '1'*/
            

--SERVER:
--RETURNS FOOD ID



----------ANNA INNEN-----------
DROP PROCEDURE IF EXISTS addFood
GO
CREATE PROCEDURE addFood @foodName nvarchar(30) ,@price int ,@rating float ,@categoryID int ,@restaurantID int,
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

/*--HASZN?LAT:
DECLARE @returnID INT  
EXEC @returnID = addFood 'Sajtkr?mleves',1250,3.2,1,7,'2020.04.19.','2020.06.23.'
SELECT  'foodID' = @returnID 
GO 

--SERVER:
DECLARE @returnID INT  
EXEC @returnID = addFood @foodName,@price,@rating,@categoryID,@restaurantID,@availableFrom,@availableTo
SELECT  'foodID' = @returnID 
GO 

SELECT * FROM Restaurant.Food
SELECT * FROM Restaurant.CategoryName
SELECT * FROM Restaurant.AllergenNames

SELECT * FROM Restaurant.Allergens
JOIN Restaurant.AllergenNames ON Restaurant.AllergenNames.allergenID = Restaurant.Allergens.allergenID
WHERE Restaurant.Allergens.allergenID = '1'

SELECT allergenID FROM Restaurant.AllergenNames WHERE name = 'Liszt'
INSERT INTO Restaurant.Allergens(allergenID,foodID)*/



/* ALLERG?NEK */
INSERT INTO Restaurant.AllergenNames(name) VALUES('Laktoz')
INSERT INTO Restaurant.AllergenNames(name) VALUES('Tojas')
INSERT INTO Restaurant.AllergenNames(name) VALUES('Diofelek')
INSERT INTO Restaurant.AllergenNames(name) VALUES('Foldimogyoro')
INSERT INTO Restaurant.AllergenNames(name) VALUES('Eper')
INSERT INTO Restaurant.AllergenNames(name) VALUES('Malna')
INSERT INTO Restaurant.AllergenNames(name) VALUES('Kiwi')
INSERT INTO Restaurant.AllergenNames(name) VALUES('Paradicsom')
INSERT INTO Restaurant.AllergenNames(name) VALUES('Kagylo')