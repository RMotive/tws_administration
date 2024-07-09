create database [TWS Business];

/****** Generate Tables ******/
create database [TWS Business];
use [TWS Business]

create table Maintenances(
id int IDENTITY (1,1) PRIMARY KEY NOT NULL,
Anual date NOT NULL,
Trimestral date NOT NULL,
);

create table Manufacturers(
id int IDENTITY (1,1) PRIMARY KEY NOT NULL,
Model varchar(30) NOT NULL,
Brand varchar(15) NOT NULL,
Year date NOT NULL
);

create table Insurances(
id int IDENTITY (1,1) PRIMARY KEY NOT NULL,
Policy varchar(20) NOT NULL,
Expiration date NOT NULL,
Country varchar(3) NOT NULL,
);

create table Situations(
id int IDENTITY (1,1) PRIMARY KEY NOT NULL,
Name nvarchar (25) Unique NOT NULL,
Description nvarchar (100)
);

create table SCT(
id int IDENTITY (1,1) PRIMARY KEY NOT NULL,
Type varchar(6) NOT NULL,
Number varchar(25) NOT NULL,
Configuration varchar(10) NOT NULL
);
create table Trucks(
 id int IDENTITY(1,1) PRIMARY KEY,
 VIN varchar(17) Unique NOT NULL,
 Manufacturer int NOT NULL,
 Motor varchar(16) unique NOT NULL,
 SCT int,
 Maintenance int,
 Situation int,
 Insurance int,

 constraint FK@Trucks_Manufacturers foreign key(Manufacturer) references Manufacturers(id),
 constraint FK@Trucks_Maintenances foreign key(Maintenance) references Maintenances(id),
 constraint FK@Trucks_Insurances foreign key(Insurance) references Insurances(id),
 constraint FK@Trucks_Situations foreign key(Situation) references Situations(id),
 constraint FK@Trucks_SCT foreign key(SCT) references SCT(id),
);

create table Plates(
id int IDENTITY (1,1) PRIMARY KEY NOT NULL,
Identifier varchar(12) NOT NULL,
State varchar(3) NOT NULL,
Country varchar(3) NOT NULL,
Expiration date NOT NULL,
Truck int NOT NULL,

 constraint FK@Plates_Trucks foreign key(Truck) references Trucks(id)

);