-- Dropping the tables before recreating the database in the order depending how the foreign keys are placed.
IF OBJECT_ID('tblShoppingCart', 'U') IS NOT NULL DROP TABLE tblShoppingCart;
IF OBJECT_ID('tblOrder', 'U') IS NOT NULL DROP TABLE tblOrder;
IF OBJECT_ID('tblItem', 'U') IS NOT NULL DROP TABLE tblItem;
IF OBJECT_ID('tblUser', 'U') IS NOT NULL DROP TABLE tblUser;

-- Checks if the database already exists.
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'BirthdayDB')
CREATE DATABASE BirthdayDB;
GO

USE BirthdayDB
CREATE TABLE tblUser(
	UserID INT IDENTITY(1,1) PRIMARY KEY 	NOT NULL,
	Username VARCHAR (40) UNIQUE			NOT NULL,
	UserPassword VARCHAR (40)				NOT NULL,
	FirstName VARCHAR (40),
	LastName VARCHAR (40),
	UserAddress VARCHAR (40),
	PhoneNumber VARCHAR (13),
);

USE BirthdayDB
CREATE TABLE tblItem(
	ItemID INT IDENTITY(1,1) PRIMARY KEY 	NOT NULL,
	ItemName VARCHAR (40) UNIQUE		    NOT NULL,
	ItemType VARCHAR (40)					NOT NULL,
	Price VARCHAR (10)						NOT NULL,
);

USE BirthdayDB
CREATE TABLE tblShoppingCart(
	ShoppingCartID INT IDENTITY(1,1) PRIMARY KEY 	NOT NULL,
	Amount INT DEFAULT 0, 
	UserID INT FOREIGN KEY REFERENCES tblUser(UserID),
	ItemID INT FOREIGN KEY REFERENCES tblItem(ItemID),
);

USE BirthdayDB
CREATE TABLE tblOrder(
	OrderID INT IDENTITY(1,1) PRIMARY KEY 			NOT NULL,
	TotalPrice VARCHAR (10)							NOT NULL,
	OrderCreated DATE								NOT NULL,
	TotalCakesOrdered INT							NOT NULL,
	AllCakes VARCHAR (200)							NOT NULL,
	UserID INT FOREIGN KEY REFERENCES tblUser(UserID),
);
