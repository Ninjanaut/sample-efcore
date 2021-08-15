create database OrderSystem;

use OrderSystem;
go

create table Customer
(
	Id int identity not null,
	FirstName nvarchar(255) not null,
	LastName nvarchar(255) not null,
	constraint PK_Customer_Id primary key (Id)
);

create table [Address]
(
	CustomerId int not null,
	City nvarchar(255) not null,
	Street nvarchar(255) not null,
	PostalCode nvarchar(255) not null,
	constraint PK_Customer_CustomerId primary key (CustomerId),
	constraint FK_Customer_CustomerId foreign key (CustomerId) references Customer (Id)
);

create table [Product]
(
	Id int identity not null,
	Number int not null,
	[Name] nvarchar(255) not null,
	constraint PK_Product_Id primary key (Id),
);

create table [Order]
(
	Id int identity not null,
	CustomerId int not null,
	[Status] int not null,
	OrderDate DateTime2(0) not null,
	constraint PK_Order_Id primary key (Id),
	constraint FK_Order_CustomerId foreign key (CustomerId) references Customer (Id)
);

create table [OrderLine]
(
	OrderId int not null,
	ProductId int not null,
	Quantity int not null,
	UnitPrice decimal(10,2) not null,
	constraint PK_OrderLine_OrderId_ProductId primary key (OrderId, ProductId),
	constraint FK_OrderLine_OrderId foreign key (OrderId) references [Order] (Id),
	constraint FK_OrderLine_ProductId foreign key (ProductId) references Product (Id)
);