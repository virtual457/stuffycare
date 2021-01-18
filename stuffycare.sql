
use StuffyCare
go
drop table [vendorservices]
drop table vendoritems
drop table appointments
drop table pets
drop table cart
drop table address
drop table wishlist
drop table reveiws
drop table orders
drop table items
drop table users

create TABLE users(
	id int not null Identity(1,1),
	userid AS ('U' + RIGHT('0000000000' + CAST(id AS VARCHAR(10)),10)) persisted primary key,
	firstname varchar(100),
	lastname varchar(100),
	email varchar(100) ,
	pass varchar(100),
	pno varchar(10) ,
	[image] varchar(max),
	loyaltyPoints int,
	isdeleted bit,
	
);

--//////////////////////////////////////////////////////////////////////////
--Vendors
--/////////////////////////////////////////////////////////////////////////
go
DROP TABLE vendors;
--creation of Vendor table
CREATE TABLE vendors (
	id int not null Identity(1,1),
	vendorid AS ('V' + RIGHT('0000000000' + CAST(id AS VARCHAR(10)),10)) persisted  primary key,
	[name] varchar(100),
	[description] varchar(max),
	email varchar(100) unique,
	pass varchar(100),
	pno varchar(10) unique,
	gender varchar(100),
	storeName varchar(100),
	city varchar(100),
	[location] varchar(max),
	yearsofexperience float,
	monfrom varchar(100),
	monto varchar(100),
	tuefrom varchar(100),
	tueto varchar(100),
	wedfrom varchar(100),
	wedto varchar(100),
	thurfrom varchar(100),
	thurto varchar(100),
	frifrom varchar(100),
	frito varchar(100),
	satfrom varchar(100),
	satto varchar(100),
	sunfrom varchar(100),
	sunto varchar(100),
	photo varchar(max),
	photoidproof varchar(max),
	issellingitem bit,
	homeservice bit,
	isdeleted bit,
	isauthorized bit,
	Constraint ven_duplicate_email unique(email),
	Constraint ven_duplicate_phno unique(pno)
);
--inserting values to vendor table
insert into vendors values('vendor','we take good care of your pets during day','vendor@stuffycare.com','vendor','9945563998','Male','srinivasa stores','HYD','this is the location',10.0,'09:00','15:00','09:00','15:00','09:00','15:00','09:00','15:00','09:00','15:00','09:00','15:00','09:00','15:00','photo string','photoidproof string',1,1,0,0)
select * from vendors
--creation of vendor_auth stored procedure
DROP PROCEDURE vendor_auth
go
CREATE PROCEDURE vendor_auth
@Email varchar(200),
@Pass varchar(200),
@Role varchar(20) output
As
BEGIN
	IF (EXISTS(select * from vendors where email=@Email and pass=@Pass)or exists(select * from vendors where pno=@Email and pass=@Pass))
	Begin
		set @Role='Logged in successfully'
	End
	Else
	Begin
		IF (EXISTS(select * from vendors where email=@Email)or exists(select * from vendors where pno=@Email))
		Begin
			set @Role='Incorrect Password'
		end
		else
		Begin
			if(exists(select * from authvendors where email=@Email)or exists(select * from authvendors where pno=@Email))
			begin
				set @Role='Account is yet to be Authorized by admin'
			end
			else
			begin
				set @Role='Incorrect Email or Phone number'
			end
		end
	end
	select @Role
END
go
--testing the vendor_auth stored procedure
select * from vendors
declare  @output varchar(100)
exec vendor_auth 'vendor2@stuffycare.com','vendor2',@output output
go
DROP PROCEDURE vendor_create
go
--creation of vendor create procedure
CREATE PROCEDURE vendor_create
@name varchar(100),
@description varchar(max),
@email varchar(100),
@pass varchar(100),
@pno varchar(10),
@gender varchar(100),
@storename varchar(100),
@city varchar(100),
@location varchar(100),
@yearsofexperience float,
@monfrom varchar(100),
@monto varchar(100),
@tuefrom varchar(100),
@tueto varchar(100),
@wedfrom varchar(100),
@wedto varchar(100),
@thurfrom varchar(100),
@thurto varchar(100),
@frifrom varchar(100),
@frito varchar(100),
@satfrom varchar(100),
@satto varchar(100),
@sunfrom varchar(100),
@sunto varchar(100),
@photo varchar(max),
@photoidproof varchar(max),
@homeservice bit,
@issellingitem bit,
@ret varchar(100) output
As
BEGIN
	IF (((SELECT email FROM users WHERE users.email = @email )= @email) or ((SELECT email FROM vendors WHERE vendors.email = @email )= @email) or((SELECT email FROM admins WHERE admins.email = @email )= @email) or ((SELECT email FROM authvendors WHERE authvendors.email = @email )= @email))
	begin
		set @ret = 'Email alredy exists'
	end
	ELSE
	begin
		if(((select pno from users where users.pno=@pno)=@pno)or ((select pno from vendors where vendors.pno=@pno)=@pno) or((select pno from admins where admins.pno=@pno)=@pno) or ((select pno from authvendors where authvendors.pno=@pno)=@pno))
		begin
			set @ret = 'Phone No alredy exists'
		end
		else
		begin
			insert into vendors values(@name,@description,@email,@pass,@pno,@gender,@storename,@city,@location,@yearsofexperience,@monfrom,@monto,@tuefrom,@tueto,@wedfrom,@wedto,@thurfrom,@thurto,@frifrom,@frito,@satfrom,@satto,@sunfrom,@sunto,@photo,@photoidproof,@issellingitem,@homeservice,0,0)
			set @ret ='Vendor added sucessfully'
		end
	end
	select @ret
END
go
--testing vendor_create procedure
declare  @output varchar(100)
exec vendor_create 'vendor2','we take very good care of your pets during day','vendor2@stuffycare.com','vendor','1234567890','Female','srinivasa stores','HYD','this is the location',10.0,'09:00','15:00','09:00','15:00','09:00','15:00','09:00','15:00','09:00','15:00','09:00','15:00','09:00','15:00','photo string','photoidproof string',0,0,@output output 
go
select * from vendors


--////////////////////////////////////////////////////////////////////////////////////////////
--Vendorsservices part of database begins
--////////////////////////////////////////////////////////////////////////////////////////////

create table vendorservices
(
	id int not null Identity(1,1) primary key,
	vendorid varchar(11) references vendors(vendorid),
	[name] varchar(100),
	price float
)
go
insert into vendorservices values('V0000000001','daycare',1000)
select * from vendorservices

--////////////////////////////////////////////////////////////////////////////////////////////
--user part of database begins
--////////////////////////////////////////////////////////////////////////////////////////////

--creation of user table

insert into users values('chandan','gowda','chandan@gmail.com','chandan','9945583998','encoded image',0,0),('first','last','chandangowda457@gmail.com','chandangowda457','8073598383','encoded image',0,0);

DROP PROCEDURE user_auth
go
--creation of user_auth procedure
CREATE PROCEDURE user_auth
@Email varchar(200),
@Pass varchar(200),
@Role varchar(20) output
As
BEGIN
	IF (EXISTS(select * from users where email=@Email and pass=@Pass)or exists(select * from users where pno=@Email and pass=@Pass))
	Begin
		set @Role='Logged in successfully'
	End
	Else
	Begin
		IF (EXISTS(select * from users where email=@Email)or  exists(select * from users where pno=@Email ))
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
select * from users
declare  @output varchar(100)
exec user_auth '9945583998','password changed',@output output
go
DROP PROCEDURE user_create
go
--creation if user_create procedure
CREATE PROCEDURE user_create
@first varchar(100),
@last varchar(100),
@email varchar(100),
@pass varchar(100),
@pno varchar(10),
@image varchar(max),
@ret varchar(100) output
As
BEGIN
	IF ((@email != '') and (((SELECT email FROM users WHERE users.email = @email )= @email) or ((SELECT email FROM vendors WHERE vendors.email = @email )= @email) or((SELECT email FROM admins WHERE admins.email = @email )= @email)or ((SELECT email FROM authvendors WHERE authvendors.email = @email )= @email)))
	begin
		set @ret = 'Email alredy exists'
	end
	ELSE
	begin
		if((@pno != '') and (((select pno from users where users.pno=@pno)=@pno)or ((select pno from vendors where vendors.pno=@pno)=@pno) or((select pno from admins where admins.pno=@pno)=@pno) or ((select pno from authvendors where authvendors.pno=@pno)=@pno)))
		begin
			set @ret = 'Phone No alredy exists'
		end
		else
		begin
			insert into users values(@first,@last,@email,@pass,@pno,@image,0,0)
			set @ret ='User added sucessfully'
		end
	end
	select @ret
END
go
--drop procedure get_user
--go
----creation of get_user procedure
--Create procedure get_user
--@userid varchar(100)
--as
--begin
--	if(@userid='all')
--	begin
--		select * from users
--	end
--	begin
--		select * from users where users.userid=@userid
--	end
--end
--go
--checking of procedures creted for users
declare @output varchar(200)
Exec user_create 'quast','blast','','chandan.keelara','994583998','encoded image',@output output
go
declare @lol varchar(200)
Exec user_auth 'chandan.keelara@gmail.com','chandan.keelara',@lol output
go
Exec get_user 'U0000000001'
select * from users
go
--///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
--Pets part of the database
--//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

go
create table pets
(
id int not null Identity(1,1),
petid AS ('PET' + RIGHT('0000000000' + CAST(id AS VARCHAR(10)),10)) persisted primary key,
userid varchar(11) references users(userid),
[name] varchar(200),
[type] varchar(100),
[size] varchar(100),
[gender] varchar(100),
breed varchar(100),
allergies varchar(max),
age float,
moreinfo varchar(max),
isdeleted bit,
)
go
drop procedure add_pet
go
create procedure add_pet
@userid varchar(11),
@name varchar(11),
@type varchar(100),
@size varchar(100),
@gender varchar(100),
@breed varchar(100),
@allergies varchar(100),
@age float,
@moreinfo varchar(max),
@ret varchar(200) out
as
begin
		set @ret ='adding pet failed'
		if(exists(select userid from users where users.userid=@userid ))
			begin
				insert into pets values(@userid,@name,@type,@size,@gender,@breed,@allergies,@age,@moreinfo,0)
				set @ret = 'pet added sucessfully'
			end
		else
			begin
				set @ret = 'Userid doesnt exist'
			end
	select @ret
end
go
declare @ret varchar(max)
exec add_pet 'U0000000001','Blacky','dog','medium','Male','Husky','chicken,turkey,beef','10','Usually doesnt bite anyone XD',@ret out
select * from pets
--////////////////////////////////////////////////////////////////////////////////////////////
--Appointment part of database begins
--////////////////////////////////////////////////////////////////////////////////////////////
go

--creation of appointment table
Create table appointments 
(	
	id int not null Identity(1,1) primary key,
	aptid AS ('Apt' + RIGHT('0000000000' + CAST(id AS VARCHAR(10)),10)),
	userid varchar(11) references users(userid),
	petid varchar(13) references pets(petid),
	phonenumber varchar(200),
	vendorid varchar(11) references vendors(vendorid), 
	category varchar(100),
	servicedatetime datetime,
	servicefees float,
	[address] varchar(100),
	[message] varchar(100),
	ishomeservice bit,
	ispaid bit,
)
go
select * from appointments
go
drop procedure add_appointment
go
--creation of add_appointment method
create procedure add_appointment
@userid varchar(100),
@petid varchar(100),
@phonenumber varchar(200),
@vendorid varchar(11),
@category varchar(100),
@servicedatetime datetime,
@servicefees float,
@address varchar(100),
@message varchar(100),
@ishomeservice bit,
@ispaid bit,
@ret varchar(100) output
As

BEGIN
	set @ret='could not add appointment'
	if((select userid from users where users.userid=@userid )=@userid)
		begin
		IF ((select petid from pets where pets.userid=@userid and pets.petid=@petid)=@petid)
			begin
				if exists (select vendors.vendorid from vendors where vendorid=@vendorid)
				begin
					if exists (select * from vendorservices where vendorid=@vendorid and vendorservices.[name]=@category and vendorservices.price=@servicefees)
				begin
					insert into appointments values(@userid,@petid,@phonenumber,@vendorid,@category,convert(datetime,@servicedatetime),(select price from vendorservices where vendorservices.vendorid=@vendorid and vendorservices.[name]=@category),@address,@message,@ishomeservice,@ispaid)
					set @ret ='appointment created sucessfully'
				end
				else
				begin
					set @ret='Enter Valid services Data'
				end
				end
				else
				begin
					set @ret='Vendor id doesnt exist'
				end
				
			end
		else
			begin
				set @ret ='Pet id doesnt exist'
			end
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
select * from appointments

go
declare @ret varchar(100)
Exec add_appointment 'U0000000001','PET0000000001','8745698725','V0000000001','daycare','20201010',1000,'door no 1119 5th cross 1st main road','My pet is sick again and keeps vomitting',0,0,@ret output
go
select * from pets
go
--drop procedure get_allappointments
--////////////////////////////////////////////////////////////////////////////////////////////
--Admin part of database begins
--////////////////////////////////////////////////////////////////////////////////////////////
go
DROP TABLE admins;
--creation of admin table
CREATE TABLE admins (
	id int not null Identity(1,1),
	adminid AS ('A' + RIGHT('0000000000' + CAST(id AS VARCHAR(10)),10)) persisted primary key,
	email varchar(100) unique,
	pass varchar(100),
	pno varchar(10)unique,
	Constraint dup_email unique(email),
	Constraint dup_phno unique(pno)
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
	IF( EXISTS(select * from admins where email=@Email and pass=@Pass) or  exists(select * from admins where pno=@Email and pass=@pass))
	Begin
		set @Role='Logged in successfully'
	End
	Else
	Begin
		IF (EXISTS(select * from admins where email=@Email) or exists(select * from admins where pno=@Email ))
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
exec admin_auth '','admin',@output output
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
	IF (((SELECT email FROM users WHERE users.email = @email )= @email) or ((SELECT email FROM vendors WHERE vendors.email = @email )= @email) or((SELECT email FROM admins WHERE admins.email = @email )= @email) or ((SELECT email FROM authvendors WHERE authvendors.email = @email )= @email))
	begin
		set @ret = 'Email alredy exists'
	end
	ELSE
	begin
		if(((select pno from users where users.pno=@pno)=@pno)or ((select pno from vendors where vendors.pno=@pno)=@pno) or((select pno from admins where admins.pno=@pno)=@pno) or((select pno from authvendors where authvendors.pno=@pno)=@pno) )
		begin
			set @ret = 'Phone No alredy exists'
		end
		else
		begin
			insert into admins values(@email,@pass,@pno)
			set @ret ='Admin added sucessfully'
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

go
Create table items
(
id int not null Identity(1,1),
itemid AS ('I' + RIGHT('0000000000' + CAST(id AS VARCHAR(10)),10)) persisted primary key,
[name] varchar(100),
[description] varchar(500),
subdescription varchar(max),
foranimal varchar(100),
category varchar(100),
subcategory varchar(100),
price float,
saleprice float,
sku varchar(100),
quantity int,
moa int,
addedby varchar(100),
photo varchar(max),
[length] float,
[breadth] float,
[height]float,
[weight] float,
shippingclass varchar(100),
processingtime varchar(100),
mililitres varchar(100),
packsizeingrams varchar(100),
unitcount int,
upsells varchar(100),
crosssells varchar(100),
policylabel varchar(max),
shippingpolicy varchar(max),
refundpolicy varchar(max),
cancelationpolicy varchar(max),
exchangepolicy varchar(max),
storename varchar(max),
commissionfor varchar(100),
commissionmode varchar(100),
authorizedby varchar(11) references admins(adminid),
authorizedstatus varchar(100),
deletedstatus varchar(100)
);
go
insert into items values('pedigree','food for dog','lots of nutrition for your dog','dog','food','dryfood',10,17,'IDK',10,2,'V0000000001','encoded string',10,10,10,10.5,'shippingclass','processingtime','10ml','100gms',1,'upsells','crosssells','policylabel','shippingpolicy','refundpolicy','cancelationpolicy','exchangepolicy','storename','commissionfor','commissionmode','A0000000001','authorized','notrequested')

insert into items values('dog dry shampoo','shampoo for dog','Shampoo for dry cleaning your dog','dog','shampoo','dry shampoo',10,17,'IDK',10,2,'V0000000001','encoded string',10,10,10,10.5,'shippingclass','processingtime','10ml','100gms',1,'upsells','crosssells','policylabel','shippingpolicy','refundpolicy','cancelationpolicy','exchangepolicy','storename','commissionfor','commissionmode','A0000000001','authorized','notrequested')
insert into items values('dog wet shampoo','shampoo for dog','Shampoo for cleaning your dog','dog','shampoo','wet shampoo',10,17,'IDK',10,2,'V0000000001','encoded string',10,10,10,10.5,'shippingclass','processingtime','10ml','100gms',1,'upsells','crosssells','policylabel','shippingpolicy','refundpolicy','cancelationpolicy','exchangepolicy','storename','commissionfor','commissionmode','A0000000001','authorized','notrequested')
insert into items values('dog Tic shampoo','shampoo for Tic Removal for dog','Shampoo for cleaning your dog and making it tic free','dog','shampoo','tic shampoo',10,17,'IDK',10,2,'V0000000001','encoded string',10,10,10,10.5,'shippingclass','processingtime','10ml','100gms',1,'upsells','crosssells','policylabel','shippingpolicy','refundpolicy','cancelationpolicy','exchangepolicy','storename','commissionfor','commissionmode','A0000000001','authorized','notrequested')
insert into items values('dog hair fall shampoo','shampoo Hairfall reducing for dog','Shampoo for cleaning your dog and making it Hairfall free','dog','shampoo','hairfall shampoo',10,17,'IDK',10,2,'V0000000001','encoded string',10,10,10,10.5,'shippingclass','processingtime','10ml','100gms',1,'upsells','crosssells','policylabel','shippingpolicy','refundpolicy','cancelationpolicy','exchangepolicy','storename','commissionfor','commissionmode','A0000000001','authorized','notrequested')

insert into items values('cat dry shampoo','shampoo for cat','Shampoo for dry cleaning your cat','cat','shampoo','dry shampoo',10,17,'IDK',10,2,'V0000000001','encoded string',10,10,10,10.5,'shippingclass','processingtime','10ml','100gms',1,'upsells','crosssells','policylabel','shippingpolicy','refundpolicy','cancelationpolicy','exchangepolicy','storename','commissionfor','commissionmode','A0000000001','authorized','notrequested')
insert into items values('cat wet shampoo','shampoo for cat','Shampoo for cleaning your cat','cat','shampoo','wet shampoo',10,17,'IDK',10,2,'V0000000001','encoded string',10,10,10,10.5,'shippingclass','processingtime','10ml','100gms',1,'upsells','crosssells','policylabel','shippingpolicy','refundpolicy','cancelationpolicy','exchangepolicy','storename','commissionfor','commissionmode','A0000000001','authorized','notrequested')
insert into items values('cat Tic shampoo','shampoo for Tic Removal for cat','Shampoo for cleaning your cat and making it tic free','cat','shampoo','tic shampoo',10,17,'IDK',10,2,'V0000000001','encoded string',10,10,10,10.5,'shippingclass','processingtime','10ml','100gms',1,'upsells','crosssells','policylabel','shippingpolicy','refundpolicy','cancelationpolicy','exchangepolicy','storename','commissionfor','commissionmode','A0000000001','authorized','notrequested')
insert into items values('cat hair fall shampoo','shampoo Hairfall reducing for cat','Shampoo for cleaning your cat and making it Hairfall free','cat','shampoo','hairfall shampoo',10,17,'IDK',10,2,'V0000000001','encoded string',10,10,10,10.5,'shippingclass','processingtime','10ml','100gms',1,'upsells','crosssells','policylabel','shippingpolicy','refundpolicy','cancelationpolicy','exchangepolicy','storename','commissionfor','commissionmode','A0000000001','authorized','notrequested')

--drop procedure get_items
go
select * from items
--create procedure get_items
--@itemid varchar(11)
--As
--	if(@itemid='all')
--	begin
--		select * from items
--	end
--	else
--	begin
--		select * from items where itemid=@itemid
--	end
--go
drop procedure add_item
go
create procedure add_item
@name varchar(100),
@description varchar(500),
@subdescription varchar(max),
@foranimal varchar(100),
@category varchar(100),
@subcategory varchar(100),
@price float,
@saleprice float,
@sku varchar(100),
@quantity int,
@moa int,
@addedby varchar(100),
@photo varchar(max),
@length float,
@breadth float,
@height float,
@weight float,
@shippingclass varchar(100),
@processingtime varchar(100),
@mililitres varchar(100),
@packsizeingrams varchar(100),
@unitcount int,
@upsells varchar(100),
@crosssells varchar(100),
@policylabel varchar(max),
@shippingpolicy varchar(max),
@refundpolicy varchar(max),
@cancelationpolicy varchar(max),
@exchangepolicy varchar(max),
@storename varchar(max),
@commissionfor varchar(100),
@commissionmode varchar(100),
@authorizedby varchar(11),
@authorizedstatus varchar(100),
@deletedstatus varchar(100),
@ret varchar(100) output
as
begin
	set @ret='could not add item'
	if (exists (select adminid from admins where admins.adminid=@addedby) or exists(select vendorid from vendors where vendors.vendorid=@addedby and vendors.isauthorized=1))
	begin
		
		insert into items values(@name,@description,@subdescription,@foranimal,@category,@subcategory,@price,@saleprice,@sku,@quantity,@moa,@addedby,@photo,@length,@breadth,@height,@weight,@shippingclass,@processingtime,@mililitres,@packsizeingrams,@unitcount,@upsells,@crosssells,@policylabel,@shippingpolicy,@refundpolicy,@cancelationpolicy,@exchangepolicy,@storename,@commissionfor,@commissionmode,@authorizedby,@authorizedstatus,@deletedstatus)
		set @ret ='item created sucessfully'
	end
	else
	begin
		set @ret = 'You are not autorized to add items'
	end
	select @ret	
end
go
declare @ret varchar(100)
exec add_item 'Comb','Used to comb','The subdescrption is long may have => bullets points => and so on','both','grooming','hairgrooming',200.20,300.60,'IDK',20,2,'A0000000001','encoded string',10,10,10,10.5,'shippingclass','processingtime','10ml','100gms',1,'upsells','crosssells','policylabel','shippingpolicy','refundpolicy','cancelationpolicy','exchangepolicy','storename','commissionfor','commissionmode','A0000000001','authorized','notrequested',@ret output
select * from items
go
--//////////////////////////////////////////////////////////////////////////
--Orders 
--/////////////////////////////////////////////////////////////////////////

select * from vendors
go
create table orders
(
id int not null Identity(1,1),
orderid AS ('O' + RIGHT('0000000000' + CAST(id AS VARCHAR(10)),10)) persisted primary key,
userid varchar(11) references users(userid),
itemid varchar(11) references items(itemid),
dt datetime,
quantity int,
[status] varchar(100),
method varchar(100),
total float,
sr_orderid int,
sr_shipmentid int
);
go
drop procedure add_order
go
create procedure add_order
@userid varchar(100),
@itemid varchar(100),
@quantity int,
@method varchar(100),
@sr_orderid int,
@sr_itemid int,
@ret varchar(100) output
as
	begin
	begin tran
	begin try
	begin
		if exists(select userid from users where userid=@userid)
		begin
			If((select quantity from items where itemid='I0000000001')>=@quantity)
				begin
					insert into orders values(@userid,@itemid,GETDATE(),@quantity,'order placed',@method,@quantity*(select saleprice from items where itemid=@itemid),@sr_orderid,@sr_itemid)
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
	commit tran
	end try
	begin catch
		set @ret = 'Exception occured'
		rollback
	end catch
	select @ret
	end
go

declare @ret varchar(100)
exec add_order 'U0000000001','I0000000001',3,'paypal',114586,1145868,@ret output
go
select * from orders
select * from items
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
id int not null Identity(1,1),
authvendorsid AS ('AVID' + RIGHT('0000000000' + CAST(id AS VARCHAR(10)),10)) persisted primary key,
category varchar(100),
[description] varchar(max),
email varchar(100),
pass varchar(100),
pno varchar(100),
);
go
insert into authvendors values('veternary','the best doctor in the world','vendor3@stuffycare.com','vendor3','9945583999')
go
--drop procedure auth_vendoradd

--go
--create procedure auth_vendoradd
--@authvendorsid varchar(100),
--@ret varchar(100) output
--as
--begin
--	if exists(select * from authvendors where authvendors.authvendorsid=@authvendorsid) 
--	begin
--	begin transaction t1
--		declare @category varchar(100)
--		declare @description varchar(max)
--		declare @email varchar(100)
--		declare @pno varchar(100)
--		declare @pass varchar(max)
--		select @category=category,@description=[description],@email=email,@pno=pno,@pass=pass from authvendors where authvendors.authvendorsid=@authvendorsid
--		exec vendor_create @category,@description,@email=@email,@pass=@pass,@pno=@pno,@ret=@ret output
--		delete from authvendors where authvendorsid=@authvendorsid
--	commit transaction t1
--	end
--	else 
--	begin
--		set @ret = 'Can authorize only req vendors'
--	end
--	select @ret
--end 
--go
--select * from vendors
--select * from authvendors
--declare @ret varchar(100)
--exec auth_vendoradd 'AVID0000000003',@ret output
--go
--drop procedure add_authvendor
--go
--create procedure add_authvendor
--@category varchar(100),
--@description varchar(max),
--@email varchar(100),
--@pass varchar(100),
--@pno varchar(100),
--@ret varchar(100) output
--as
--begin
--	IF (((SELECT email FROM users WHERE users.email = @email )= @email) or ((SELECT email FROM vendors WHERE vendors.email = @email )= @email) or((SELECT email FROM admins WHERE admins.email = @email )= @email)or((SELECT email FROM authvendors WHERE authvendors.email = @email )= @email))
--	begin
--		set @ret = 'Email alredy exists'
--	end
--	ELSE
--	begin
--		if(((select pno from users where users.pno=@pno)=@pno)or ((select pno from vendors where vendors.pno=@pno)=@pno) or((select pno from admins where admins.pno=@pno)=@pno) or ((select pno from authvendors where authvendors.pno=@pno)=@pno))
--		begin
--			set @ret = 'Phone No alredy exists'
--		end
--		else
--		begin
--			insert into authvendors values(@category,@description,@email,@pass,@pno)
--			set @ret ='acoount placed for request'
--		end
--	end
--	select @ret
--end
--go
--select * from authvendors
--declare @ret varchar(100)
--exec add_authvendor'daycare','we are the second best daycare available','vendorsecond@stuffycare.com','whatever','9483512645',@ret output
--//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
--auth vendorItems Part of the database
--/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
--Drop table vendoritems
go

Create table vendoritems
(
id int not null Identity(1,1),
itemid AS ('VIID' + RIGHT('0000000000' + CAST(id AS VARCHAR(10)),10)) persisted primary key,
[name] varchar(100),
[description] varchar(500),
category varchar(100),
price float,
sku varchar(100),
saleprice float,
quantity int,
moa int,
own varchar(100),
photo varchar(max),
[length] int,
[breadth] int,
[height]int,
[weight] float
);
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
@length int,
@breadth int,
@height int,
@weight float,
@ret varchar(100) output
as
begin
	set @ret='could not add item'
	if exists (select email from vendors where vendorid=@own)
	begin
	if (not exists (select itemid from items where items.[name]=@name) and not exists (select itemid from vendoritems where vendoritems.[name]=@name))
		begin
			insert into vendoritems values(@name,@description,@category,@price,@sku,@saleprice,@quantity,@moa,@own,convert(varchar,@photo),@length,@breadth,@height,@weight)
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
exec vendoradd_item 'backcomb','used to brush the hair','grooming',20.2,'IDK',25.2,10,2,'V0000000001','encoded string',10,10,10,2.5,@ret output
select * from vendoritems
--go
--drop procedure auth_vendoritem
--go
--create procedure auth_vendoritem
--@itemid varchar(14),
--@email varchar(100),
--@ret varchar(100) output
--as
--begin
--begin tran t1
--begin try
--	if exists(select email from admins where admins.email=@email)
--	begin
--		if exists(select itemid from vendoritems where vendoritems.itemid=@itemid)
--		begin 
--		declare @name varchar(100)
--		declare @desc varchar(100)
--		declare @category varchar(100)
--		declare @price float
--		declare @sku varchar(100)
--		declare @saleprice float
--		declare @quantity int
--		declare @moa int
--		declare @own varchar(100)
--		declare @photo varchar(max)
--		declare @length int
--		declare @breadth int
--		declare @height int
--		declare @weight float
--		select @name=[name],@desc=vendoritems.[description],@category=vendoritems.category,@price=price,@sku=sku,@saleprice=saleprice,@quantity=quantity,@moa=moa,@own=own,@photo=photo,@length=vendoritems.[length],@breadth=vendoritems.[breadth],@height=vendoritems.[height],@weight=vendoritems.[weight] from vendoritems where itemid=@itemid 
--		exec add_item @name,@desc,@category,@price,@sku,@saleprice,@quantity,@moa,@own,@photo,@length,@breadth,@height,@weight,@ret output
--		if(@ret = 'item created sucessfully')
--		begin
--			Delete from vendoritems where itemid=@itemid
--		end
--		else
--		begin
--			set @ret=@ret
--			return
--		end
		
--		set @ret = 'Item authorized sucessfully'
--		end
--		else
--		begin
--			set @ret = 'Item doesnt exits in vendor items'
--		end
--	end
--	else
--	begin
--		set @ret = 'U are not authorized'
--	end
--	select @ret
--	end try
--	begin catch
--		set @ret='Some Exception Occured'
--		Rollback transaction t1
--	end catch
--	Commit transaction t1
--end
--go
--select * from items
--select * from vendors
--select * from vendoritems
--declare @ret varchar(100)
--exec auth_vendoritem 'VIID0000000001','admin@stuffycare.com',@ret out

select * from items
--//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
--Update quantity
--//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
drop procedure update_item_quantity
go
create procedure update_item_quantity
@itemid varchar(11),
@quantity int,
@addedby varchar(100),
@ret varchar(100) output
as
begin
	if(exists(select items.addedby from items where itemid=@itemid and items.authorizedstatus='authorized' and items.deletedstatus<>'approved'))
		begin
		if((select items.addedby from items where itemid=@itemid)=@addedby)
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
			
		end
	else
	begin
		set @ret = 'item no doesnt exist'
	end
	select @ret
end
go
select * from items
declare @ret varchar(100)
exec update_item_quantity 'I0000000001',8,'admin@stuffycare.com',@ret out
--///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
--Reveiws part of the database
--//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
--drop table reveiws
go
create table reveiws
(
id int not null Identity(1,1),
reveiwid AS ('R' + RIGHT('0000000000' + CAST(id AS VARCHAR(10)),10)) persisted primary key,
userid varchar(11) references users(userid),
itemid varchar(11) references items(itemid),
dt datetime,
[title] varchar(200),
[description] varchar(max),
stars float,
photo varchar(max)
)
go
drop procedure add_reveiw
go
create procedure add_reveiw
@userid varchar(11),
@itemid varchar(11),
@title varchar(200),
@description varchar(max),
@stars float,
@photo varchar(max),
@ret varchar(200) out
as
begin
	if (exists(select itemid from items where items.itemid=@itemid))
	begin
		if(exists(select userid from users where users.userid=@userid ))
		begin
			insert into reveiws values(@userid,@itemid,GETDATE(),@title,@description,@stars,@photo)
		end
		else
		begin
			set @ret = 'Userid doesnt exist'
		end
	end
	else
		begin
			set @ret='Item id doesnt exist'
		end
	select @ret
end

--///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
--wishlist part of the database
--//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
--drop table reveiws
go
create table wishlist
(
id int not null Identity(1,1) primary key,
userid varchar(11) references users(userid),
itemid varchar(11) references items(itemid)
)
go
insert into wishlist values('U0000000001','I0000000001')
insert into wishlist values('U0000000001','I0000000002')
insert into wishlist values('U0000000003','I0000000001')


--///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
--cart part of the database
--//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
--drop table reveiws
go
create table cart
(
id int not null Identity(1,1) primary key,
userid varchar(11) references users(userid),
itemid varchar(11) references items(itemid),
quantity int
)
go
insert into cart values('U0000000001','I0000000001',5)

--///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
--Reveiws part of the database
--//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////f
--drop table reveiws

go
create table address
(
id int not null Identity(1,1),
addressid AS ('UA' + RIGHT('0000000000' + CAST(id AS VARCHAR(10)),10)) persisted primary key,
userid varchar(11) references users(userid),
firstname varchar(100),
lastname varchar(100),
[addresslineone] varchar(max),
[addresslinetwo] varchar(max),
landmark varchar(max),
city varchar(100),
pincode varchar(100),
[state] varchar(100),
country varchar(100),
email varchar(100),
phone varchar(100),
isshippingaddress bit,
isdeleted bit,
)
go
drop procedure add_address
go
create procedure add_address
@userid varchar(11) ,
@firstname varchar(100),
@lastname varchar(100),
@address varchar(max),
@address2 varchar(max),
@landmark varchar(max),
@city varchar(100),
@pincode varchar(100),
@state varchar(100),
@country varchar(100),
@email varchar(100),
@phone varchar(100),
@isshippingaddress bit,
@ret varchar(200) out
as
begin
	if (exists(select userid from users where userid=@userid))
	begin
		insert into address values(@userid,@firstname,@lastname,@address,@address2,@landmark,@city,@pincode,@state,@country,@email,@phone,@isshippingaddress,0)
		set @ret='address added sucessfully'
	end
	else
		begin
			set @ret='user doesnt exist'
		end
	select @ret
end
go
select * from address
declare @ret varchar(100)
exec add_address 'U0000000001','chandan','gowda', 'address1','address2','changed','bangalore','560018','karnataka','india','chandan@gmail.com','99448868',0,@ret out
go
--//////////////////////////////////////////////////////////////////////////////////////////////////////////
--
--//////////////////////////////////////////////////////////////////////////////////////////////////////////////////
drop procedure updatepassword
go
create procedure updatepassword
@emailorpno varchar(100),
@pass varchar(max),
@ret varchar(100) out
as
begin
	set @ret = 'could not update password'
	if (exists(select * from users where email=@emailorpno) or exists(select * from users where users.pno=@emailorpno))
	begin
		if exists(select * from users where users.pno=@emailorpno)
		begin
			update users
			set pass=@pass
			where users.pno=@emailorpno
			set @ret ='password updated sucessfuly'
		end
		else
		begin
			update users
			set pass=@pass
			where users.email=@emailorpno
			set @ret ='password updated sucessfully'
		end
	end
	else
	begin
		set @ret='user with the email or phone doesnt exist'
	end
end
go
declare @ret varchar(100)
exec updatepassword 'chandan@gmail.com','password changed',@ret out
go
--/////////////////////////////////////////////////////////////////////////////////////////
--
--//////////////////////////////////////////////////////////////////////////////////////////
drop table otp
go
create table otp
(
	phoneno varchar(10) primary key,
	otpstring varchar(max),
	createdDate datetime,
)
go

--/////////////////////////////////////////////////////////////////////////////////////////
--
--//////////////////////////////////////////////////////////////////////////////////////////
select * from users
select * from vendors
select * from admins
select * from items
select * from orders
select * from appointments
select * from Pets
select * from cart
select * from wishlist
select * from authvendors
select * from vendoritems
select * from otp
select * from address
go
