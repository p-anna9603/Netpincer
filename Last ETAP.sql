------------------------------2021.05.11-------------------------------
USE Netpincer

ALTER TABLE Restaurant.Food ADD discount FLOAT		---e.g. 0.05
ALTER TABLE Restaurant.Restaurant ADD approximateTime INT	---minutes e.g. 60
ALTER TABLE Restaurant.Orders ADD ETA nvarchar(30)	--- Estimated Time of Arrival

--SELECT * FROM DeliveryPerson.DeliveryPersonOrders
ALTER TABLE DeliveryPerson.DeliveryPersonOrders DROP COLUMN orderID			----1
ALTER TABLE DeliveryPerson.DeliveryPersonOrders DROP CONSTRAINT FK__DeliveryP__order__1DB06A4F		---2
ALTER TABLE DeliveryPerson.DeliveryPersonOrders DROP COLUMN orderID			---3

CREATE TABLE DeliveryPerson.AssignDelivery
(
	id INT PRIMARY KEY,
	deliveryPersonID INT FOREIGN KEY REFERENCES DeliveryPerson.DeliveryPersonOrders(id) ON DELETE SET NULL,
	orderID INT FOREIGN KEY REFERENCES Restaurant.Orders(orderID) ON DELETE SET NULL
)

DROP PROCEDURE IF EXISTS addFood
GO
CREATE PROCEDURE addFood @foodName nvarchar(30) ,@price int ,@rating float ,@categoryID int ,@restaurantID int,
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

------------SERVER STUFF----------PLEASE IGNORE-----------
--DECLARE @returnID INT EXEC @returnID = addFood @foodName,@price,@rating,@categoryID,@restaurantID,@availableFrom,@availableTo, @discount SELECT  'foodID' = @returnID
--UPDATE Restaurant.Food SET discount = 1.3 WHERE foodID = '1'
--UPDATE Restaurant.Orders SET ETA = @eta WHERE orderID = @orderID AND restaurantID = @restaurantID
--UPDATE Restaurant.Orders SET ETA = '2021.05.15 15.12' WHERE orderID = '5' AND restaurantID = '12'
--SELECT * FROM Restaurant.Orders

---------------------------05.12-----------------------------------------
USE Netpincer
INSERT INTO Restaurant.Orders(restaurantID, username,foods, [status], startOrderTime, endOrderTime, price) VALUES('1','icuska00','1,2,3,5,9',0,'2020.04.25. 15:49','2020.04.25. 16:32',3425)
INSERT INTO Restaurant.Orders(restaurantID, username,foods, [status], startOrderTime, endOrderTime, price) VALUES('1','alma','15,6,4',0,'2020.04.25. 16:49','2020.04.25. 17:32',6500)
INSERT INTO Restaurant.Orders(restaurantID, username,foods, [status], startOrderTime, endOrderTime, price) VALUES('1','testUser','1,8,4,11',0,'2020.04.25. 17:49','2020.04.25. 18:32',12500)
INSERT INTO Restaurant.Orders(restaurantID, username,foods, [status], startOrderTime, endOrderTime, price) VALUES('12','testUser','6,7,9,25',0,'2020.04.25. 18:49','2020.04.25. 19:32',8745)
INSERT INTO Restaurant.Orders(restaurantID, username,foods, [status], startOrderTime, price) VALUES('1','icuska00','6,7,9,25',0,'2020.04.25. 18:49',6100)
--INSERT INTO Users.Users(username, password, lastName, firstName, phoneNumber, addressID, userType, email) 
--    VALUES('futar01','1234', 'Futar', 'Vilmos', '36-202222221', 2, 2,'futar01@gmail.com')
--INSERT INTO DeliveryPerson.WorkingHours(username ,fromHour,fromMinute,toHour,toMinute ,workingDays) VALUES ('futar01',8,30,20,45,'1,2,3,5')


SELECT * FROM Users.Users
SELECT * FROM Restaurant.Restaurant
SELECT * FROM Restaurant.Food

---SQL étterem szenvedés
--ALTER TABLE Restaurant.Restaurant DROP CONSTRAINT FK__Restauran__owner__29221CFB
--ALTER TABLE Restaurant.Restaurant ADD FOREIGN KEY (owner) REFERENCES Users.Users(username) ON DELETE SET NULL ON UPDATE CASCADE
--UPDATE Users.Users SET username = 'marica' WHERE username = 'AsztalVokMegint'

SELECT Restaurant.restaurantID, orderID, [status], startOrderTime, endOrderTime, Orders.[username], price, foods,[password],[lastName],[firstName],Users.[phoneNumber],Users.[addressID] ,[userType], Users.[email],UsersAddress.city,UsersAddress.line1,UsersAddress.line2,UsersAddress.zipcode FROM Restaurant.Orders JOIN Restaurant.Restaurant ON Restaurant.restaurantID = Orders.restaurantID JOIN Users.Users ON Users.username = Restaurant.Orders.username JOIN Users.UsersAddress ON UsersAddress.addressID = Users.addressID WHERE Orders.restaurantID = '1'

--SELECT * FROM Restaurant.Food

--DELETE FROM  Restaurant.Orders WHERE orderID= 12
--SELECT * FROM Restaurant.Orders

--SELECT * FROM Restaurant.Food

--UPDATE Restaurant.Food SET name ='Kenyer0', price=250, rating =4.8, availableFrom='2020.10.10.', availableTo='2021.12.30.', discount = 0.1  WHERE foodID='13'

--SELECT * FROM Restaurant.Allergens

--DELETE FROM Restaurant.Allergens WHERE foodID=7


--INSERT INTO Restaurant.Orders(restaurantID, username, foods, [status], startOrderTime, endOrderTime, price, ETA) VALUES('1','icuska00','1,2,3,5,9',0,'2020.04.25. 15:49','2020.04.25. 16:32',3425,'eta datum')

--SELECT approximateTime FROM Restaurant.Restaurant WHERE restaurantID = 1

--SELECT COUNT(username) FROM Users.Users WHERE username LIKE 'guest%'

---------------------------------------05.13.-------------------------------------------
USE Netpincer

DROP TABLE IF EXISTS dbo.AssignDelivery
GO
DROP TABLE IF EXISTS DeliveryPerson.AssignDelivery
GO
CREATE TABLE DeliveryPerson.AssignDelivery
(
	id INT IDENTITY PRIMARY KEY,
	deliveryPersonID INT FOREIGN KEY REFERENCES DeliveryPerson.DeliveryPersonOrders(id) ON DELETE SET NULL,
	orderID INT FOREIGN KEY REFERENCES Restaurant.Orders(orderID) ON DELETE SET NULL
)

SELECT Users.username, id FROM Users.Users JOIN DeliveryPerson.DeliveryPersonOrders ON DeliveryPerson.DeliveryPersonOrders.username = Users.username WHERE userType = 2

SELECT * FROM DeliveryPerson.DeliveryPersonOrders JOIN DeliveryPerson.WorkingHours ON DeliveryPerson.WorkingHours.username = DeliveryPerson.DeliveryPersonOrders.username
--INSERT INTO DeliveryPerson.DeliveryPersonOrders(username) VALUES ('futar01')

SELECT * FROM DeliveryPerson.AssignDelivery

SELECT * FROM DeliveryPerson.WorkingHours
UPDATE DeliveryPerson.WorkingHours SET workingDays = '1,2,3,4,5,6,7'


SELECT * FROM Restaurant.Orders


INSERT INTO DeliveryPerson.AssignDelivery(deliveryPersonID,orderID) VALUES (1,13)
DELETE FROM DeliveryPerson.AssignDelivery WHERE deliveryPersonID=1 AND orderID=2


SELECT Restaurant.Orders.restaurantID,DeliveryPerson.AssignDelivery.orderID, [status], startOrderTime, endOrderTime, Orders.[username], price, foods,[password],[lastName],[firstName],Users.[phoneNumber],Users.[addressID] ,[userType], Users.[email],UsersAddress.city,UsersAddress.line1,UsersAddress.line2,UsersAddress.zipcode
FROM DeliveryPerson.AssignDelivery 
JOIN Restaurant.Orders ON Restaurant.Orders.orderID = DeliveryPerson.AssignDelivery.orderID 
JOIN Users.Users ON Users.username = Restaurant.Orders.username 
JOIN Users.UsersAddress ON UsersAddress.addressID = Users.addressID 
WHERE deliveryPersonID=1


-------------------------------05.14---------------------------------------
DROP PROCEDURE IF EXISTS registerRestaurant
GO
CREATE PROCEDURE registerRestaurant @usernameParam nvarchar(50),@passParam nvarchar(30),
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


SELECT * FROM Restaurant.Restaurant

DROP FUNCTION IF EXISTS getRestaurant
GO
CREATE FUNCTION getRestaurant(@username nvarchar(20))
RETURNS TABLE AS
RETURN SELECT approximateTime,restaurantID,name,restaurantDescription,style,owner,phoneNumber, city,zipcode,line1,line2, fromHour,fromMinute,toHour,toMinute
FROM Restaurant.Restaurant
JOIN Restaurant.RestaurantAddress ON Restaurant.RestaurantAddress.addressID = Restaurant.addressID
JOIN Restaurant.OpeningHours ON Restaurant.OpeningHours.openingHoursID = Restaurant.openingHoursID
WHERE Restaurant.owner=@username
GO



