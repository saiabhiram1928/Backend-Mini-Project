use db_Video_Store;
select * from Videos;
select * from Users;
select * from Customers;
select * from Carts;
select * from CartItems;
select * from Orders;
select * from OrderItems;
select * from Payments;
select * from Inventories;
select * from Addresses;
select * from Rentals;
select * from Permanents;
select * from Refunds;


delete from Rentals where Id=3;
delete from Orders where Id=4;
delete from Payments where TransactionId = 720;
delete from Refunds where Id=3;
