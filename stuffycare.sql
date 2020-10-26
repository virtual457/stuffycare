drop table orders
drop table users
--//////////////////////////////////////////////////////////////////////////
--Vendors
--/////////////////////////////////////////////////////////////////////////
go
DROP TABLE vendors;
--creation of Vendor table
CREATE TABLE vendors (
	vendorid int not null primary key Identity(1,1),
	email varchar(200),
	pass varchar(200),
	pno varchar(10)
);
--inserting values to vendor table
insert into Vendors values('vendor@stuffycare.com','vendor','9945583998')
--creation of vendor_auth stored procedure
DROP PROCEDURE vendor_auth
go
CREATE PROCEDURE vendor_auth
@Email varchar(200),
@Pass varchar(200),
@Role varchar(20) output
As
BEGIN
	IF EXISTS(select * from vendors where email=@Email and pass=@Pass)
	Begin
		set @Role='Logged in successfully'
	End
	Else
	Begin
		IF EXISTS(select * from vendors where email=@Email)
		Begin
			set @Role='Incorrect Password'
		end
		else
		Begin
			set @Role='Incorrect Email'
		end
	end
	select @Role
END
go
--testing the vendor_auth stored procedure
declare  @output varchar(100)
exec vendor_auth 'vendor@stuffycare.com','vendor',@output output
go
DROP PROCEDURE vendor_create
go
--creation of vendor create procedure
CREATE PROCEDURE vendor_create
@email varchar(100),
@pass varchar(100),
@pno varchar(10),
@ret varchar(100) output
As
BEGIN
	IF ((SELECT email FROM vendors WHERE vendors.email = @email )= @email) 
	begin
		set @ret = 'Email alredy exists'
	end
	ELSE
	begin
		if((select pno from vendors where vendors.pno=@pno)=@pno)
		begin
			set @ret = 'Phone No alredy exists'
		end
		else
		begin
			insert into vendors values(@email,@pass,@pno)
			set @ret ='Vendor added sucessfully'
		end
	end
	select @ret
END
go
--testing vendor_create procedure
declare  @output varchar(100)
exec vendor_create 'vendor2@stuffycare.com','vendor2','3547856426',@output output 
go
select * from vendors




--////////////////////////////////////////////////////////////////////////////////////////////
--user part of database begins
--////////////////////////////////////////////////////////////////////////////////////////////

--creation of user table
CREATE TABLE users (
	email varchar(100) primary key,
	pass varchar(100),
	pno varchar(10)
);

insert into users values('chandan@gmail.com','chandan','9945583998'),('chandangowda457@gmail.com','chandangowda457','8073598383');

DROP PROCEDURE user_auth
go
--creation of user_auth procedure
CREATE PROCEDURE user_auth
@Email varchar(200),
@Pass varchar(200),
@Role varchar(20) output
As
BEGIN
	IF EXISTS(select * from users where email=@Email and pass=@Pass)
	Begin
		set @Role='Logged in successfully'
	End
	Else
	Begin
		IF EXISTS(select * from users where email=@Email)
		Begin
			set @Role='Incorrect Password'
		end
		else
		Begin
			set @Role='Incorrect Email'
		end
	end
	select @Role
END
go
declare  @output varchar(100)
exec user_auth 'chandangowda457@gmail.com','chandangowda457',@output output
go
DROP PROCEDURE user_create
go
--creation if user_create procedure
CREATE PROCEDURE user_create
@email varchar(100),
@pass varchar(100),
@pno varchar(10),
@ret varchar(100) output
As
BEGIN
	IF ((SELECT email FROM users WHERE users.email = @email )= @email) 
	begin
		set @ret = 'Email alredy exists'
	end
	ELSE
	begin
		if((select pno from users where users.pno=@pno)=@pno)
		begin
			set @ret = 'Phone No alredy exists'
		end
		else
		begin
			insert into users values(@email,@pass,@pno)
			set @ret ='User added sucessfully'
		end
	end
	select @ret
END
go
drop procedure get_user
go
--creation of get_user procedure
Create procedure get_user
@email varchar(100)
as
begin
	if(@email='all')
	begin
		select * from users
	end
	begin
		select * from users where users.email=@email
	end
end
go
--checking of procedures creted for users
declare @output varchar(200)
Exec user_create 'chandan.keelar1a@gamil.com','chandan.keelara','99415583998',@output output
go
declare @lol varchar(200)
Exec user_auth 'chandan.keelara@gmail.com','chandan.keelara',@lol output
go
Exec get_user 'chandan@gmail.com'
select * from users
go
--////////////////////////////////////////////////////////////////////////////////////////////
--Appointment part of database begins
--////////////////////////////////////////////////////////////////////////////////////////////
drop table appointments
go
--creation of appointment table
Create table appointments 
(
	aptid int not null primary key Identity(1,1) ,
	emailid varchar(200),
	pno varchar(200),
	dt date,
	tm time,
	servicetype varchar(100),
	[address] varchar(100),
	[message] varchar(100)
)
go
select * from appointments
go
drop procedure add_appointment
go
--creation of add_appointment method
create procedure add_appointment
@email varchar(100),
@pno varchar(200),
@dt date,
@tm time,
@servicetype varchar(100),
@address varchar(100),
@message varchar(100),
@ret varchar(100) output
As
BEGIN
	set @ret='could not add appointment'
	if((select email from users where users.email=@email )=@email)
		begin
			insert into appointments values(@email,@pno,convert(date,@dt),convert(time,@tm),@servicetype,@address,@message)
			set @ret ='appointment created sucessfully'
		end
	else
		begin
			set @ret='Signup to get appointment'
		end
	select @ret
END
go
declare @lol varchar(200)
--testing the add_appointemnet procedure
Exec add_appointment 'chandangowda457@gmail.com','8745698725','20081111','20:10:10','daycare','door no 1119 5th cross 1st main road','My pet is sick again and keeps vomitting',@lol output
go
select * from appointments
go
drop procedure get_allappointments
go
create procedure get_allappointments
@category varchar(100)
As
	if(@category='all')
	begin
		select* from appointments
	end
	else
	begin
		select * from appointments where servicetype=@category
	end
go

Exec  get_allappointments 'grooming'
--////////////////////////////////////////////////////////////////////////////////////////////
--Admin part of database begins
--////////////////////////////////////////////////////////////////////////////////////////////
go
DROP TABLE admins;
--creation of admin table
CREATE TABLE admins (
	email varchar(200) primary key,
	pass varchar(200),
	pno varchar(10)
);
--inserting values to admin table
insert into admins values('admin@stuffycare.com','admin','9945583998')
--creation of admin_auth stored procedure
DROP PROCEDURE admin_auth
go
CREATE PROCEDURE admin_auth
@Email varchar(200),
@Pass varchar(200),
@Role varchar(20) output
As
BEGIN
	IF EXISTS(select * from admins where email=@Email and pass=@Pass)
	Begin
		set @Role='Logged in successfully'
	End
	Else
	Begin
		IF EXISTS(select * from admins where email=@Email)
		Begin
			set @Role='Incorrect Password'
		end
		else
		Begin
			set @Role='Incorrect Email'
		end
	end
	select @Role
END
go
--testing the admin_auth stored procedure
declare  @output varchar(100)
exec admin_auth 'admin@stuffycare.com','admin',@output output
go
DROP PROCEDURE admin_create
go
--creation of admin create procedure
CREATE PROCEDURE admin_create
@email varchar(100),
@pass varchar(100),
@pno varchar(10),
@ret varchar(100) output
As
BEGIN
	IF ((SELECT email FROM admins WHERE admins.email = @email )= @email) 
	begin
		set @ret = 'Email alredy exists'
	end
	ELSE
	begin
		if((select pno from admins where admins.pno=@pno)=@pno)
		begin
			set @ret = 'Phone No alredy exists'
		end
		else
		begin
			insert into admins values(@email,@pass,@pno)
			set @ret ='User added sucessfully'
		end
	end
	select @ret
END
go
--testing admin_create procedure
declare  @output varchar(100)
exec admin_create 'admin2@stuffycare.com','admin2','3547856426',@output output 
go
select * from admins
--//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
--Items Part of the database
--/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
Drop table items
go

Create table items
(
itemid int not null primary key Identity(1,1),
[name] varchar(100),
[description] varchar(500),
category varchar(100),
price float,
sku varchar(100),
saleprice float,
quantity int,
moa int,
own varchar(100),
photo varchar(max)
);
go
drop procedure get_items
go
create procedure get_items
@itemid int
As
	if(@itemid=0)
	begin
		select * from items
	end
	else
	begin
		select * from items where itemid=@itemid
	end
go
drop procedure add_item
go
create procedure add_item
@name varchar(100),
@description varchar(500),
@category varchar(100),
@price float,
@sku varchar(100),
@saleprice float,
@quantity int,
@moa int,
@own varchar(100),
@photo varchar(max),
@ret varchar(100) output
as
begin
	set @ret='could not add item'
	if (exists (select email from admins where email=@own) or exists(select email from vendors where email=@own))
	begin
	if not exists (select itemid from items where items.[name]=@name)
		begin
			insert into items values(@name,@description,@category,@price,@sku,@saleprice,@quantity,@moa,@own,convert(varchar,@photo))
			set @ret ='item created sucessfully'
		end
	else
		begin
			set @ret=Stuff(
			(select ' alredy present as item id '+ convert(varchar(20),itemid)
			from items where items.[name]=@name and items.[category]=@category and items.[saleprice]=@saleprice),1,1,'')
		end
	end
	else
	begin
		set @ret = 'You are not autorized to add items'
	end
	select @ret	
end
go
declare @ret varchar(100)
exec add_item 'Comb','used to comb hait of pets','grooming',20.2,'IDK',25.2,10,2,'admin@stuffycare.com','encoded string',@ret output
select * from items
go
--//////////////////////////////////////////////////////////////////////////
--Orders 
--/////////////////////////////////////////////////////////////////////////


go
create table orders
(
orderid int not null primary key Identity(1,1),
email varchar(100) references users(email),
itemid int references items(itemid),
dt date,
quantity int,
[status] varchar(100),
method varchar(100),
total float,
);
go
drop procedure add_order
go
create procedure add_order
@email varchar(100),
@itemid int,
@quantity int,
@method varchar(100),
@ret varchar(100) output
as
	begin
	begin try
	begin
		if exists(select email from users where email=@email)
		begin
			If((select quantity from items where itemid=@itemid)>0)
				begin
					insert into orders values(@email,@itemid,GETDATE(),@quantity,'order placed',@method,@quantity*(select saleprice from items where itemid=itemid))
					update items 
					set quantity=items.quantity-@quantity
					where itemid=@itemid
					set @ret= 'Order placed sucessfully'
				end
			else
				begin
					set @ret = 'No stock'
				end
		end
		else
		begin
			set @ret ='Signup to place orders'
		end

	end
	end try
	begin catch
		set @ret = ERROR_MESSAGE()
		rollback
	end catch
	select @ret
	end
go

declare @ret varchar(100)
exec add_order 'chandan@gmail.com',1,2,'paypal',@ret output
go
drop procedure get_orders
go
create procedure get_orders
@orderid int
as
begin
	if(@orderid= 0)
	 begin 
		select * from orders
	 end
	else
		begin
			select * from orders where orders.orderid=@orderid
		end
end
go
exec get_orders 0
go
--//////////////////////////////////////////////////////////////////////////////////////////
--vendor authorization
--////////////////////////////////////////////////////////////////////////////////////////////
drop table authvendors
go
create table authvendors
(
authvendorsid int not null primary key Identity(1,1),
email varchar(100),
pass varchar(100),
pno varchar(100)
);
go
insert into authvendors values('vendor3@stuffycare.com','vendor3','9945583999')
go
drop procedure auth_vendoradd

go
create procedure auth_vendoradd
@email varchar(100),
@pass varchar(100),
@pno varchar(100),
@ret varchar(100) output
as
begin
	begin try
	if exists(select * from authvendors where email=@email and pno=@pno) 
	begin
		exec vendor_create @email=@email,@pass=@pass,@pno=@pno,@ret=@ret output
		delete from authvendors where email=@email
	end
	else 
	begin
		set @ret = 'Can authorize only req vendors'
	end
	select @ret
	end try
	begin catch
		Rollback
	end catch
end
go
select * from vendors
select * from authvendors
declare @ret varchar(100)
exec auth_vendoradd 'vendor3@stuffycare.com','vendor3','9945583999',@ret output
go
drop procedure add_authvendors
go
create procedure add_authvendors
@email varchar(100),
@pass varchar(100),
@pno varchar(100),
@ret varchar(100) output
as
begin
	if (exists(select * from authvendors where email=@email and pno=@pno)or exists(select * from vendors where email=@email and pno=@pno))
	begin
		set @ret = 'email or phone number alredy exists'
	end
	else
	begin
		insert into authvendors values(@email,@pass,@pno)
		set @ret ='acoount placed for request'
	end
	select @ret
end
go
--//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
--auth vendorItems Part of the database
--/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
Drop table vendoritems
go

Create table vendoritems
(
itemid int not null primary key Identity(1,1),
[name] varchar(100),
[description] varchar(500),
category varchar(100),
price float,
sku varchar(100),
saleprice float,
quantity int,
moa int,
own varchar(100),
photo varchar(max)
);
go
drop procedure vendorget_items
go
create procedure vendorget_items
@itemid int
As
	if(@itemid=0)
	begin
		select * from vendoritems
	end
	else
	begin
		select * from vendoritems where itemid=@itemid
	end
go
drop procedure vendoradd_item
go
create procedure vendoradd_item
@name varchar(100),
@description varchar(500),
@category varchar(100),
@price float,
@sku varchar(100),
@saleprice float,
@quantity int,
@moa int,
@own varchar(100),
@photo varchar(max),
@ret varchar(100) output
as
begin
	set @ret='could not add item'
	if exists (select email from vendors where email=@own)
	begin
	if (not exists (select itemid from items where items.[name]=@name) and not exists (select itemid from vendoritems where vendoritems.[name]=@name))
		begin
			insert into vendoritems values(@name,@description,@category,@price,@sku,@saleprice,@quantity,@moa,@own,convert(varchar,@photo))
			set @ret ='item created sucessfully'
		end
	else
		begin
		if exists(select itemid from items where items.[name]=@name)
			begin
			set @ret=Stuff(
			(select ' alredy present as item id '+ convert(varchar(20),itemid)
			from items where items.[name]=@name and items.[category]=@category and items.[saleprice]=@saleprice),1,1,'')
			end
		else
			begin
				set @ret=Stuff(
				(select ' alredy present as vendoritem id '+ convert(varchar(20),itemid)
				from vendoritems where vendoritems.[name]=@name and vendoritems.[category]=@category and vendoritems.[saleprice]=@saleprice),1,1,'')
			end
		end
	end
	else
	begin
		set @ret = 'You are not autorized to add items'
	end
	select @ret	
end
go
declare @ret varchar(100)
exec vendoradd_item 'brush','used to brush the teeth','grooming',20.2,'IDK',25.2,10,2,'vendor@stuffycare.com','encoded string',@ret output
select * from vendoritems
go
drop procedure auth_vendoritem
go
create procedure auth_vendoritem
@itemid int,
@email varchar(100),
@ret varchar(100) output
as
begin
	if exists(select email from admins where admins.email=@email)
	begin
		if exists(select itemid from vendoritems where vendoritems.itemid=@itemid)
		begin 
		declare @name varchar(100)
		declare @desc varchar(100)
		declare @category varchar(100)
		declare @price float
		declare @sku varchar(100)
		declare @saleprice float
		declare @quantity int
		declare @moa int
		declare @own varchar(100)
		declare @photo varchar(max)
		select @name=[name],@desc=vendoritems.[description],@category=vendoritems.category,@price=price,@sku=sku,@saleprice=saleprice,@quantity=quantity,@moa=moa,@own=own,@photo=photo from vendoritems where itemid=@itemid 
		exec add_item @name,@desc,@category,@price,@sku,@saleprice,@quantity,@moa,@own,@photo,@ret output
		delete from vendoritems where itemid=@itemid
		set @ret = 'Item authorized sucessfully'
		end
		else
		begin
			set @ret = 'Item doesnt exits in vendor items'
		end
	end
	else
	begin
		set @ret = 'U are not authorized'
	end
	select @ret
end
go
select * from items
select * from vendoritems
declare @ret varchar(100)
exec auth_vendoritem 1,'admin@stuffycare.com',@ret out
select * from admins
select * from items
--//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
--Update quantity
--//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
drop procedure update_item_quantity
go
create procedure update_item_quantity
@itemid int,
@quantity int,
@own varchar(100),
@ret varchar(100) output
as
begin
	if((select own from items where itemid=@itemid)=@own)
	begin
		update items
		set quantity=@quantity
		where itemid=@itemid
		set @ret = 'updated Quantity sucessfully'
	end
	else
	begin
		set @ret = 'only owner can update quantity'
	end
	select @ret
end
go
select * from items
declare @ret varchar(100)
exec update_item_quantity 1,10,'admin@stuffycare.com',@ret out
