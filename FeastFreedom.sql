DROP DATABASE IF EXISTS FeastFreedom;
CREATE DATABASE FeastFreedom;

CREATE TABLE Roles(
RoleID int PRIMARY KEY IDENTITY(1,1),
RoleName varchar(50) NOT NULL
);

INSERT INTO Roles(RoleName) VALUES('Admin');
INSERT INTO Roles(RoleName) VALUES('User');
INSERT INTO Roles(RoleName) VALUES('Kitchen');

CREATE TABLE Users(
UserID int PRIMARY KEY IDENTITY(1,1),
FirstName varchar(100) NOT NULL,
LastName varchar(100) NOT NULL,
Email varchar(255) NOT NULL,
UserPassword varchar(255) NOT NULL,
FK_RoleID int FOREIGN KEY REFERENCES Roles(RoleID),
SecurityAnswer1 varchar(255) NOT NULL,
SecurityAnswer2 varchar(255) NOT NULL
);

UPDATE Users SET FK_RoleID = 2;

--DROP TABLE IF EXISTS Kitchen;
CREATE TABLE Kitchen(
KitchenID int PRIMARY KEY IDENTITY(1,1),
KitchenName varchar(255) NOT NULL,
KitchenEmail varchar(255) NOT NULL,
KitchenPasswod varchar(255) NOT NULL,
DaysWorking varchar(255) NOT NULL,
HoursWorking varchar(255) NOT NULL,
Image varchar(255) NOT NULL,
FK_RoleID int FOREIGN KEY REFERENCES Roles(RoleID)
);

UPDATE Kitchen SET FK_RoleID = 3;

CREATE TABLE Menu(
MenuItemID int PRIMARY KEY IDENTITY(1,1),
MenuItemName varchar(255) NOT NULL,
Veg_NoVeg bit NOT NULL, --boolean
MenuItemPrice money NOT NULL,
FK_KitchenID int FOREIGN KEY REFERENCES Kitchen(KitchenID)
);

CREATE TABLE Orders(
OrderID int PRIMARY KEY IDENTITY(1,1),
FK_UserID int FOREIGN KEY REFERENCES Users(UserID),
FK_KitchenID int FOREIGN KEY REFERENCES Kitchen(KitchenID),
OrderDate date
);

CREATE TABLE OrderDetails(
OrderID int NOT NULL FOREIGN KEY REFERENCES Orders(OrderID),
MenuItemID int NOT NULL FOREIGN KEY REFERENCES Menu(MenuItemID)
PRIMARY KEY (OrderID, MenuItemID),
Price money NOT NULL,
Quantity int NOT NULL
);