create database [TWS Business];

/****** Generate Tables ******/
use [TWS Business]

create table Maintenances(
id int IDENTITY (1,1) PRIMARY KEY NOT NULL,
Anual date NOT NULL,
Trimestral date NOT NULL,
);

create table Plates(
id int IDENTITY (1,1) PRIMARY KEY NOT NULL,
Identifier varchar(12) NOT NULL,
State varchar(3) NOT NULL,
Country varchar(3) NOT NULL,
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
Country varchar(3) NOT NULL,
);

create table Situations(
id int IDENTITY (1,1) PRIMARY KEY NOT NULL,
Name nvarchar (25) Unique NOT NULL,
Description nvarchar (100)
);

create table Trucks(
 id int IDENTITY(1,1) PRIMARY KEY,
 VIN varchar(17) Unique NOT NULL,
 Plate int NOT NULL, 
 Manufacturer int NOT NULL,
 Motor varchar(16) unique NOT NULL,
 SCT varchar(25) unique NOT NULL,
 Maintenance int NOT NULL,
 Situation int NOT NULL,
 Insurance int NOT NULL,

 constraint FK@Trucks_Plates foreign key(Plate) references Plates(id),
 constraint FK@Trucks_Manufacturers foreign key(Manufacturer) references Manufacturers(id),
 constraint FK@Trucks_Maintenances foreign key(Maintenance) references Maintenances(id),
 constraint FK@Trucks_Insurances foreign key(Insurance) references Insurances(id),
 constraint FK@Trucks_Situations foreign key(Situation) references Situations(id)
);

/****** Generate Inserts ******/
use [TWS Business]

INSERT INTO Maintenances(Anual, Trimestral)
VALUES('2025-08-08', '2024-08-08'),('2025-02-02', '2024-9-09'),('2025-10-10', '2024-10-10');

INSERT INTO Plates(Identifier,State,Country,Expiration)
/* State code & Country ISO*/
VALUES('SADC2423E132','CA','ENU','2024-07-01'),('TMEX2323EST2','002','ESM','2024-09-02'), ('TXH214E3ESC1','TX','ENU','2024-11-03');

INSERT INTO Manufacturers(Model,Brand,Year)
VALUES('Volvo','AX200','2024'),('Scania','R-Series','2018'),('MAN','TGX','2020');

INSERT INTO Insurances(Policy,Expiration,Country)
VALUES('TestingPolicy1-12331','2024-10-10','ESM'),('TestingPolicy2-32331','2024-09-09','ENU'),('TestingPolicy3-53242','2024-11-11','ESM');

INSERT INTO Situations(Name,Description)
VALUES('In Maintenance', 'This unit is in his scheduled maintenance'), ('In Transit', 'Work in Transit'), ('Out of service', 'Require Maintenance');

INSERT INTO Trucks(VIN,Plate,Manufacturer,Motor,SCT,Maintenance,Situation,Insurance)
VALUES('VINtest1-13324231',1,1,'Motortestnumber1','SCTtesting1-1232111513111',1,1,1),('VINtest2-63324231',2,2,'Motortestnumber2','SCTtesting2-7232111513111',2,2,2),('VINtest3-93324231',3,3,'Motortestnumber3','SCTtesting3-9232111513111',3,3,3);

/* Select all Data*/
select * from Maintenances;
select * from Plates;
select * from Manufacturers	;
select * from Insurances;
select * from Situations;
select * from Trucks;
