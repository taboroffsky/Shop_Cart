CREATE DATABASE ShopCartDB;

use ShopCartDB;

CREATE TABLE [User]
(
UserId int identity primary key,
UserName nvarchar(50),
Password nvarchar(50)
)

CREATE TABLE [News]
(
NewsId int identity primary key,
UserId int foreign key references [User](UserId),
Title nvarchar(max),
ShortDescription nvarchar(max),
[Content] nvarchar(max),
CreatedDate date,
[Status] nvarchar(max)
)



CREATE TABLE [Category]
(
CategoryId int IDENTITY PRIMARY KEY,
Name nvarchar(max)
)

CREATE TABLE [Color]
(
ColorId int IDENTITY PRIMARY KEY,
Color nvarchar(max)
)

CREATE TABLE [Model]
(
ModelId int IDENTITY PRIMARY KEY,
Model nvarchar(max)
)

CREATE TABLE [Storage]
(
StorageId int IDENTITY PRIMARY KEY,
Storage nvarchar(max)
)


CREATE TABLE [Product]
(
ProductId int IDENTITY PRIMARY KEY,
ProductName nvarchar(max),
UserId int FOREIGN KEY REFERENCES [User](UserId),
CategoryId int FOREIGN KEY REFERENCES [Category](CategoryId),
ColorId int FOREIGN KEY REFERENCES [Color](ColorId),
ModelId int FOREIGN KEY REFERENCES [Model](ModelId),
StorageId int FOREIGN KEY REFERENCES [Storage](StorageId),
SellStartDate date,
SellEndDate date,
IsNew binary
)

ALTER TABLE [Product]
ADD [Image] nvarchar(250)
