use ShopCartDB;

CREATE TABLE [Order]
(
OrderId int PRIMARY KEY IDENTITY,
OrderName nvarchar(50),
OderDate date,
PaymentType nvarchar(50),
[Status] nvarchar(50),
CustomerName nvarchar(50),
CustomerPhone nvarchar(15),
CustomerEmail nvarchar(50),
CustomerAdress nvarchar(50)
)

CREATE TABLE [OrderDetails]
(
OrderId int not null,
ProductId int not null,
Price int,
Quantity int,
PRIMARY KEY (OrderId, ProductId)
)

ALTER TABLE [OrderDetails]
ADD FOREIGN KEY (OrderId) REFERENCES [Order](OrderId);
ALTER TABLE [OrderDetails]
ADD FOREIGN KEY (ProductId) REFERENCES Product(ProductId);
