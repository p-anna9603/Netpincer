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
UPDATE Users.Users SET userType=0 

INSERT INTO Users.UsersAddress(city,zipCode,line1) VALUES('Veszprem', '8200','Egri Jozsef utca 14')
INSERT INTO Users.Users VALUES('testRestaurantOwner','r3staurant','Rest','Aurant','+36204984199','3','2')

DROP FUNCTION IF EXISTS getUser
GO
CREATE FUNCTION getUser(@usernameParam nvarchar(20),@passParam nvarchar(20), @userTypeParam int)
RETURNS TABLE AS
RETURN SELECT username, [password], lastName, firstName, phoneNumber, city, zipcode, line1, line2 ,userType
FROM Users.Users AS [u]
JOIN Users.UsersAddress ON Users.UsersAddress.addressID=[u].addressID
WHERE username=@usernameParam AND password=@passParam AND userType=@userTypeParam
GO

SELECT * FROM getUser('testUser','t3stpassword','1')
SELECT * FROM getUser('testRestaurantOwner', 'r3staurant', '1')



--REGISTER FUNCTION
--DROP PROCEDURE IF EXISTS registerUser
GO
CREATE PROCEDURE registerUser @usernameParam nvarchar(20),@passParam nvarchar(20),
			@_city nvarchar(20),@_zipCode nvarchar(20),@_line1 nvarchar(20),@_line2 nvarchar(20),
			@_lastName nvarchar(20), @_firstName nvarchar(20),@phoneNumber nvarchar(20), @_userType int
AS
--BEGIN
DECLARE @OutputTbl TABLE (ID INT)
--SET @addressID=
INSERT INTO Users.UsersAddress(city,zipCode,line1,line2)
OUTPUT Inserted.addressID	--WHY DOEST IT START AT 10???
INTO @OutputTbl(ID)
VALUES(@_city, @_zipCode,@_line1,@_line2)
DECLARE @_addressID INT
SELECT  @_addressID = ID FROM @OutputTbl

INSERT INTO Users.Users VALUES(@usernameParam,@passParam,@_lastName,@_firstName,@phoneNumber,
@_addressID,@_userType)
--RETURN 0
--END
GO

EXEC registerUser 'AsztalVokMegint','a55tal','Veszprém','8200','Asztal u. 32','2/A','Asztal','Balázs','+36205544778','1'
SELECT * FROM Users.Users
SELECT* FROM Users.UsersAddress