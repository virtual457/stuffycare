--create database WoofyTailsDB
use WoofyTailsDB
drop table dbo.items
drop table dbo.appointments
drop table dbo.pets
drop table dbo.vendorservices
drop table dbo.vendors
drop table dbo.users
drop table dbo.roles
go
--/////////////////////////////////////////////////////////////////////////////////////
-- Roles table having to store different roles of users
--////////////////////////////////////////////////////////////////////////////////////
 create table roles
 (
	roleId int not null Identity(1,1) primary key,
	roleName varchar(100)
 )
 go
 -- inserting different roles into roles table
 insert into roles values('Admin')
 insert into roles values('Sub-Admin')
 insert into roles values('Vendor')
 insert into roles values('User')
 select * from roles
 go
 --//////////////////////////////////////////////////////////////////////////////////////////////////////
 -- Users table to store details of all roles users
 --/////////////////////////////////////////////////////////////////////////////////////////////////////
  create table users
  (
	userId varchar(36) primary key,
	--userid AS ('U' + RIGHT('0000000000' + CAST(id AS VARCHAR(10)),10)) persisted primary key,
	[firstName] varchar(100) constraint chk_user_firstName check (firstName  NOT LIKE '%[^A-Z0-9]%' ),
	[lastName] varchar(100) constraint chk_user_lastName check (lastName  NOT LIKE '%[^A-Z0-9]%' ),
	[emailId] varchar(100) constraint chk_user_emailid	check (emailId Like '%_@__%.__%' or emailId like ''),
	[password] varchar(100) constraint chk_user_password check([password] like '________%'),
	[phoneNumber] varchar(10)  constraint chk_user_phoneNumber check (phoneNumber   LIKE '[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]' or phoneNumber Like ''),
	[gender] varchar(1) constraint chk_user_gender check(gender in('M','F','O')),
	[image] varchar(max),
	[roleId] int references roles(roleid),
	isDeleted int,
  
  )
  --///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
  -- Creating vendors table to hold all the extra details about vendors
  --///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
  create table vendors(
	vendorid varchar(36)  primary key,
	[description] varchar(max),
	[storeName] varchar(100),
	[city] varchar(100),
	[location] varchar(max),
	[yearsofexperience] float,
	[monfrom] varchar(100),
	[monto] varchar(100),
	[tuefrom] varchar(100),
	[tueto] varchar(100),
	[wedfrom] varchar(100),
	[wedto] varchar(100),
	[thurfrom] varchar(100),
	[thurto] varchar(100),
	[frifrom] varchar(100),
	[frito] varchar(100),
	[satfrom] varchar(100),
	[satto] varchar(100),
	[sunfrom] varchar(100),
	[sunto] varchar(100),
	[photo] varchar(max),
	[photoidproof] varchar(max),
	[authorizedby] varchar(36),
	[authorizedstatus] varchar(100) constraint chk_vendor_authorizedstatus check(authorizedstatus in ('pending','confirmed','rejected')),
	[issellingitem] tinyint,
	[homeservice] tinyint,
	[isdeleted] tinyint,
	[isauthorized] tinyint,
  )
  --///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
  -- Creating vendorsservices table to hold all the services provided by each vendors
  --///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
  
create table vendorservices
(
	id int not null Identity(1,1) primary key,
	vendorid varchar(36) references vendors(vendorid),
	[nameofservice] varchar(100),
	price float

)
--///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
-- Creating pets table to store details of the pets of users
--///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
  
create table pets
(
	petid varchar(36) primary key,
	userid varchar(36) references users(userId),
	[name] varchar(200) ,
	[type] varchar(100),
	[size] varchar(100),
	[gender] varchar(100),
	breed varchar(100),
	allergies varchar(max),
	age float,
	moreinfo varchar(max),
	isdeleted tinyint,

)
--/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
-- Creating appointments table to hold all the appointment details booked by users for services of vendors or appointments with vendors
--/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
  
create table appointments
(
	aptid varchar(36) primary key,
	userid varchar(36) references users(userid),
	petid varchar(36) references pets(petid),
	phonenumber varchar(200),
	vendorid varchar(36) references vendors(vendorid), 
	category varchar(100),
	servicedatetime datetime,
	servicefees float,
	[address] varchar(100),
	[message] varchar(100),
	ishomeservice tinyint,
	ispaid tinyint,



)
--///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
-- Creating items table to hold all the details about items available
--///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
  Create table items
(
itemid varchar(36) primary key,
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
addedby varchar(36) references vendors(vendorid),
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
authorizedby varchar(36) references users(userid),
authorizedstatus varchar(100),
deletedstatus tinyint
);
--///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
-- Creating orders table to hold all the details about orders placed by users
--///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
select * from appointments
select * from pets
select * from vendorservices
select * from vendors
select * from users
select * from roles