Create Table Products
(
	Id int identity(1,1) primary key,
	ProductName varchar(100) not null,
	Price float not null,
	Qty int not null,
)

Select * from Products