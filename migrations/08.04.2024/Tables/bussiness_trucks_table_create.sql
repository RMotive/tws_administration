create database [TWS Business];

/****** Generate Table ******/
use [TWS Business]

create table Trucks(
 id int IDENTITY(1,1) PRIMARY KEY,
 VIN varchar(17) NOT NULL,
 Plate int NOT NULL, 
 Manufacturer int NOT NULL,
 Motor varchar(16) NOT NULL,
 SCT varchar(25) NOT NULL,
 Maintenance int NOT NULL,
 Situation varchar(20) NOT NULL,
 Insurance int NOT NULL,

 constraint FK@Trucks_Plates foreign key(Plate) references Plates(id),
 constraint FK@Trucks_Manufacturers foreign key(Manufacturer) references Manufacturers(id),
 constraint FK@Trucks_Maintenances foreign key(Maintenance) references Maintenances(id),
 constraint FK@Trucks_Insurances foreign key(Insurance) references Insurances(id)
);

create table Maintenances(
id int IDENTITY (1,1) PRIMARY KEY NOT NULL,
Anual date NOT NULL,
Trimestral date NOT NULL,
);

create table Plates(
id int IDENTITY (1,1) PRIMARY KEY NOT NULL,
Identifier varchar(12) NOT NULL,
State varchar(30) NOT NULL,
Country varchar(30) NOT NULL,
Expiration date NOT NULL,
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
Country varchar(30) NOT NULL,
);
