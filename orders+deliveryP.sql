USE Netpincer

CREATE TABLE Restaurant.Orders
(
	orderID INT IDENTITY PRIMARY KEY,
	restaurantID INT FOREIGN KEY REFERENCES Restaurant.Restaurant(restaurantID) ON DELETE CASCADE,
	username NVARCHAR(50) NOT NULL FOREIGN KEY REFERENCES Users.Users(username) ON DELETE CASCADE,
	foods NVARCHAR(100) NOT NULL,
	[status] INT NOT NULL,
	startOrderTime NVARCHAR(50) NOT NULL,
	endOrderTime NVARCHAR(50),
	price FLOAT NOT NULL
);
GO

CREATE SCHEMA DeliveryPerson
GO

CREATE TABLE DeliveryPerson.WorkingHours
(
	workingHoursID INT IDENTITY PRIMARY KEY,
	username NVARCHAR(50) NOT NULL FOREIGN KEY REFERENCES Users.Users(username) ON DELETE CASCADE,
	fromHour INT,
	fromMinute INT,
	toHour INT,
	toMinute INT,
	workingDays NVARCHAR(50) NOT NULL
);
GO

CREATE TABLE DeliveryPerson.DeliveryPersonOrders
(
	id INT IDENTITY PRIMARY KEY,
	username NVARCHAR(50) NOT NULL FOREIGN KEY REFERENCES Users.Users(username) ON DELETE NO ACTION,
	orderID INT FOREIGN KEY REFERENCES Restaurant.Orders(orderID) ON DELETE CASCADE	
);
GO

-----------------04-25---------------
SELECT * FROM Restaurant.Orders
SELECT * FROM Restaurant.Restaurant
SELECT * FROM Users.Users WHERE userType = 0

--ALTER TABLE Restaurant.Orders ALTER COLUMN foods NVARCHAR(100) NOT NULL

--SERVER:
--SELECT orderID, [status], startOrderTime, endOrderTime, username, price, foods
--FROM Restaurant.Orders
--JOIN Restaurant.Restaurant ON Restaurant.restaurantID = Orders.restaurantID
--WHERE Orders.restaurantID = '1'

INSERT INTO Restaurant.Restaurant(name,addressID,openingHoursID,restaurantDescription,style,owner,phoneNumber) 
	VALUES ('ASD etterem',1,1,'aaaaaaaaaaasd', 'AAAAAAAA','mennyiAsztal','3641616554')

SELECT * FROM Restaurant.Orders
SELECT * FROM Restaurant.Food
SELECT * FROM Users.Users
SELECT * FROM Users.UsersAddress

INSERT INTO Users.Users(username, password, lastName, firstName, phoneNumber, addressID, userType, email) 
	VALUES('janika','1234', 'Kis', 'Janos', '36-202222222', 5, 0,'janika@gmail.com')

INSERT INTO Users.Users(username, password, lastName, firstName, phoneNumber, addressID, userType, email) 
	VALUES('panna','1234', 'Horvath', 'Panna', '36-202222222', 5, 0,'pannika@gmail.com')

INSERT INTO Restaurant.Orders(restaurantID, username,foods, [status], startOrderTime, endOrderTime, price) VALUES('1','pistike','1,2,3,5,9',0,'2020.04.25. 15:49','2020.04.25. 16:32',3425)
INSERT INTO Restaurant.Orders(restaurantID, username,foods, [status], startOrderTime, endOrderTime, price) VALUES('1','katalin01','10,6,4',0,'2020.04.25. 16:49','2020.04.25. 17:32',6500)
INSERT INTO Restaurant.Orders(restaurantID, username,foods, [status], startOrderTime, endOrderTime, price) VALUES('1','testUser','1,8,4,10',0,'2020.04.25. 17:49','2020.04.25. 18:32',12500)
INSERT INTO Restaurant.Orders(restaurantID, username,foods, [status], startOrderTime, endOrderTime, price) VALUES('2','testUser','6,7,9,10',0,'2020.04.25. 18:49','2020.04.25. 19:32',8745)
INSERT INTO Restaurant.Orders(restaurantID, username,foods, [status], startOrderTime, price) VALUES('1','icuska00','6,7,9,10',0,'2020.04.25. 18:49',6100)

INSERT INTO Restaurant.Orders(restaurantID, username,foods, [status], startOrderTime, endOrderTime, price) 
	VALUES('1','janika','1,10', 0,'2020.04.25. 18:49','2020.04.25. 19:32',8745)
INSERT INTO Restaurant.Orders(restaurantID, username,foods, [status], startOrderTime, endOrderTime, price) 
	VALUES('1','panna','6,7,10', 0,'2020.04.25. 18:49','2020.04.25. 19:32',8745)

--UPDATE Restaurant.Orders SET [status] = '2' WHERE orderID = '1'

--SELECT foodID,name,price,rating,pictureID,Restaurant.Food.categoryID,Restaurant.Food.restaurantID,availableFrom,availableTo FROM Restaurant.Food JOIN Restaurant.CategoryName ON Restaurant.CategoryName.categoryID = Restaurant.Food.categoryID WHERE Restaurant.Food.foodID = 1