------------------------------2021.05.11-------------------------------
USE Netpincer

ALTER TABLE Restaurant.Food ADD discount FLOAT		---e.g. 0.05
ALTER TABLE Restaurant.Restaurant ADD approximateTime INT	---minutes e.g. 60
ALTER TABLE Restaurant.Orders ADD ETA nvarchar(30)	--- Estimated Time of Arrival

--SELECT * FROM DeliveryPerson.DeliveryPersonOrders
ALTER TABLE DeliveryPerson.DeliveryPersonOrders DROP COLUMN orderID			----1
ALTER TABLE DeliveryPerson.DeliveryPersonOrders DROP CONSTRAINT FK__DeliveryP__order__13F1F5EB		---2
ALTER TABLE DeliveryPerson.DeliveryPersonOrders DROP COLUMN orderID			---3

CREATE TABLE AssignDelivery
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

