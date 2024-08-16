
/****** Generate Tables ******/
create database [TWS Business];

use [TWS Business]
DROP TABLE PlatesTrucksH;
DROP TABLE Plates_H;
DROP TABLE Trucks_H;
DROP TABLE Carriers_H;
DROP TABLE USDOT_H;
DROP TABLE Contacts_H;
DROP TABLE Maintenances_H;
DROP TABLE Insurances_H;
DROP TABLE SCT_H;
DROP TABLE Plates;
DROP TABLE Trucks;
DROP TABLE Carriers;
DROP TABLE Contacts;
DROP TABLE USDOT;
DROP TABLE SCT;
DROP TABLE Addresses;
DROP TABLE Maintenances;
DROP TABLE Manufacturers;
DROP TABLE Insurances;
DROP TABLE Situations;
DROP TABLE Statuses;

create table Statuses(
id int IDENTITY (1,1) PRIMARY KEY NOT NULL,
[Name] nvarchar (25) UNIQUE NOT NULL,
[Description] nvarchar (150)
);

create table Maintenances(
id int IDENTITY (1,1) PRIMARY KEY NOT NULL,
[Status] int not null,
Anual date NOT NULL,
Trimestral date NOT NULL,

constraint FK_Maintenances_Statuses foreign key([Status]) references Statuses(id)


);

create table Manufacturers(
id int IDENTITY (1,1) PRIMARY KEY NOT NULL,
Model varchar(30) NOT NULL,
Brand varchar(15) NOT NULL,
[Year] date NOT NULL

);

create table Insurances(
id int IDENTITY (1,1) PRIMARY KEY NOT NULL,
[Status] int not null,
[Policy] varchar(20) NOT NULL,
Expiration date NOT NULL,
Country varchar(3) NOT NULL,

constraint FK_Insurances_Statuses foreign key([Status]) references Statuses(id)

);

create table Situations(
id int IDENTITY (1,1) PRIMARY KEY NOT NULL,
[Name] nvarchar (25) UNIQUE NOT NULL,
[Description] nvarchar (100)
);

create table SCT(
id int IDENTITY (1,1) PRIMARY KEY NOT NULL,
[Status] int not null,
[Type] varchar(6) NOT NULL,
Number varchar(25) NOT NULL,
[Configuration] varchar(10) NOT NULL,

constraint FK_SCT_Statuses foreign key([Status]) references Statuses(id)

);

Create table Contacts(
id int IDENTITY (1,1) PRIMARY KEY NOT NULL,
[Status] int not null,
Enterprise varchar(13),
Personal varchar(13),
Alternative varchar(30),
Email varchar(30) not null,

constraint FK_Contacts_Statuses foreign key([Status]) references Statuses(id)

);

Create table Addresses(
id int IDENTITY (1,1) PRIMARY KEY NOT NULL,
Street varchar(100),
AltStreet varchar(100),
City varchar(30),
ZIP varchar(5),
Country varchar(3) not null,
Colonia varchar(30),

);

create table USDOT(
id int IDENTITY (1,1) PRIMARY KEY NOT NULL,
[Status] int not null,
MC varchar(7),
SCAC varchar(4)

constraint FK_USDOT_Statuses foreign key([Status]) references Statuses(id),

);

create table Carriers(
id int IDENTITY (1,1) PRIMARY KEY NOT NULL,
[Status] int not null,
[Name] Varchar(20) not null,
Contact int not null,
[Address] int not null,
USDOT int,
SCT int,

constraint FK_Carriers_Statuses foreign key([Status]) references Statuses(id),
constraint FK_Carriers_Contacts foreign key(Contact) references Contacts(id),
constraint FK_Carriers_Addresses foreign key([Address]) references Addresses(id),
constraint FK_Carriers_USDOT foreign key(USDOT) references USDOT(id),
constraint FK_Carriers_SCT foreign key(SCT) references SCT(id),

);

create table Trucks(
 id int IDENTITY(1,1) PRIMARY KEY,
 [Status] int not null,
 VIN varchar(17) UNIQUE NOT NULL,
 Manufacturer int NOT NULL,
 Motor varchar(16),
 Economic varchar(16) NOT NULL,
 Maintenance int,
 Situation int,
 Insurance int,
 Carrier int
 constraint FK_Trucks_Manufacturers foreign key(Manufacturer) references Manufacturers(id),
 constraint FK_Trucks_Maintenances foreign key(Maintenance) references Maintenances(id),
 constraint FK_Trucks_Insurances foreign key(Insurance) references Insurances(id),
 constraint FK_Trucks_Situations foreign key(Situation) references Situations(id),
 constraint FK_Trucks_Statuses foreign key([Status]) references Statuses(id),
 constraint FK_Trucks_Carriers foreign key(Carrier) references Carriers(id)
);
create table Plates(
id int IDENTITY (1,1) PRIMARY KEY NOT NULL,
Identifier varchar(12) NOT NULL,
[Status] int not null,

[State] varchar(4) NOT NULL,
Country varchar(3) NOT NULL,
Expiration date NOT NULL,
Truck int NOT NULL,

 constraint FK@Plates_Trucks foreign key(Truck) references Trucks(id)

);

-- Historical Tables --
 create table Insurances_H(
 id int IDENTITY (1,1) PRIMARY KEY NOT NULL,
 [Sequence] int not null DEFAULT 1,
 Timemark datetime2 not null,
 [Status] int not null,

 Entity int not null,
 [Policy] varchar(20) NOT NULL,
 Expiration date NOT NULL,
 Country varchar(3) NOT NULL,

 constraint FK_InsurancesH_Insurances foreign key(Entity) references Insurances(id),
 constraint FK_InsurancesH_Statuses foreign key([Status]) references Statuses(id)

 );
 create table Maintenances_H(
 id int IDENTITY (1,1) PRIMARY KEY NOT NULL,
 [Sequence] int not null DEFAULT 1,
 Timemark datetime2 not null,
 [Status] int not null,

 Entity int not null,
 Anual date NOT NULL,
 Trimestral date NOT NULL,

 constraint FK_MaintenancesH_Maintenances foreign key(Entity) references Maintenances(id),
 constraint FK_MaintenancesH_Statuses foreign key([Status]) references Statuses(id)


 );
 create table Plates_H(
 id int IDENTITY (1,1) PRIMARY KEY NOT NULL,
 [Sequence] int not null DEFAULT 1,
 Timemark datetime2 not null,
 [Status] int not null,

  Entity int not null,
 Identifier varchar(12) NOT NULL,
[State] varchar(4) NOT NULL,
Country varchar(3) NOT NULL,
Expiration date NOT NULL,
Truck int NOT NULL,

 constraint FK_PlatesH_Trucks foreign key(Truck) references Trucks(id),
 constraint FK_PlatesH_Plates foreign key(Entity) references Plates(id),
 constraint FK_PlatesH_Statuses foreign key([Status]) references Statuses(id)
 );
 create table SCT_H(
 id int IDENTITY (1,1) PRIMARY KEY NOT NULL,
 [Sequence] int not null DEFAULT 1,
 Timemark datetime2 not null,
 [Status] int not null,

  Entity int not null,
[Type] varchar(6) NOT NULL,
Number varchar(25) NOT NULL,
[Configuration] varchar(10) NOT NULL

 constraint FK_SCTH_SCT foreign key(Entity) references SCT(id),
 constraint FK_SCTH_Statuses foreign key([Status]) references Statuses(id)


 );

 create table USDOT_H(
 id int IDENTITY (1,1) PRIMARY KEY NOT NULL,
 [Sequence] int not null DEFAULT 1,
 Timemark datetime2 not null,
 [Status] int not null,

 Entity int not null,
 MC varchar(7),
 SCAC varchar(4)

 constraint FK_USDOTH_USDOT foreign key(Entity) references USDOT(id),
 constraint FK_USDOTH_Statuses foreign key([Status]) references Statuses(id),

 );

 Create table Contacts_H(
  id int IDENTITY (1,1) PRIMARY KEY NOT NULL,
  [Sequence] int not null DEFAULT 1,
 Timemark datetime2 not null,
 [Status] int not null,

 Entity int not null,
 Enterprise varchar(13),
Personal varchar(13),
Alternative varchar(30),
Email varchar(30) not null,

 constraint FK_ContactsH_Contacts foreign key(Entity) references Contacts(id),
 constraint FK_ContactsH_Statuses foreign key([Status]) references Statuses(id)
 );

 create table Carriers_H(
 id int IDENTITY (1,1) PRIMARY KEY NOT NULL,
 [Sequence] int not null DEFAULT 1,
 Timemark datetime2 not null,
 [Status] int not null,

 Entity int not null,
 [Name] Varchar(20) not null,
 ContactH int,
 [Address] int not null,
 USDOTH int,
 SCTH int,
 constraint FK_CarrierH_Carrier foreign key(Entity) references Carriers(id),
 constraint FK_CarrierH_Statuses foreign key([Status]) references Statuses(id),
 constraint FK_CarrierH_ContactsH foreign key(ContactH) references Contacts_H(id),
 constraint FK_CarrierH_Address foreign key([Address]) references Addresses(id),
 constraint FK_CarrierH_USDOTH foreign key(USDOTH) references USDOT_H(id),
 constraint FK_CarrierH_SCTH foreign key(SCTH) references SCT_H(id),
 );

 create table Trucks_H(
 id int IDENTITY (1,1) PRIMARY KEY NOT NULL,
 [Sequence] int not null DEFAULT 1,
 Timemark datetime2 not null,
 [Status] int not null,

 Entity int not null,
 VIN varchar(17) NOT NULL,
 Motor varchar(16),
 Economic Varchar(16) NOT NULL,
 Manufacturer int NOT NULL,
 Situation int,
 MaintenanceH int,
 InsuranceH int,
 CarrierH int,
 constraint FK_TrucksH_Manufacturers foreign key(Manufacturer) references Manufacturers(id),
 constraint FK_TrucksH_Situations foreign key(Situation) references Situations(id),
  constraint FK_TrucksH_MaintenancesH foreign key(MaintenanceH) references Maintenances_H(id),
 constraint FK_TrucksH_InsurancesH foreign key(InsuranceH) references Insurances_H(id),
  constraint FK_TrucksH_CarriersH foreign key(CarrierH) references Carriers_H(id),

 constraint FK_TruckH_Trucks foreign key(Entity) references Trucks(id),
 constraint FK_TrucksH_Statuses foreign key([Status]) references Statuses(id)

 );

 create table PlatesTrucksH(
  Plateid int not null,
  Truckhid int not null,

  constraint FK_PlatesTrucksH_Plates foreign key(Plateid) references Plates_H(id),
  constraint FK_PlatesTrucksH_TrucksH foreign key (Truckhid) references Trucks_H(id)
 )

INSERT INTO Statuses([Name], [Description])
VALUES('Enable','Currently Active in this Solution.'), ('Disable', 'A deleted status. Stored for historical propurses . A disable record has limited features and visibility settings.');

INSERT INTO Maintenances(Anual, Trimestral, [Status])
VALUES('2025-08-08', '2024-08-08', 1),('2025-02-02', '2024-9-09', 1),('2025-10-10', '2024-10-10', 1);

INSERT INTO Manufacturers(Model, Brand, [Year])
VALUES('Volvo','AX200','2024'),('Scania','R-Series','2018'),('MAN','TGX','2020');

INSERT INTO Insurances([Policy], Expiration, Country, [Status])
VALUES('TestingPolicy1-12331','2024-10-10','MEX', 1),('TestingPolicy2-32331','2024-09-09','USA', 1),('TestingPolicy3-53242','2024-11-11','MEX', 1);

INSERT INTO Situations([Name],[Description])
VALUES('In Maintenance', 'This unit is out of service'), ('In Transit', 'Work in Transit'), ('Out of service', 'Maintenance Required ');

INSERT INTO SCT([Type], Number, [Configuration], [Status])
VALUES('Type06','SCTtesting1-1232111513111', 'confTest01', 1),('Type09','SCTtesting2-2232111513111', 'confTest02', 1),('Type12','SCTtesting3-3232111513111', 'confTest03', 1);

INSERT INTO USDOT(MC, SCAC, [Status])
VALUES('MCtest1','SCA1', 1),('MCtest1','SCA1', 1),('MCtest1','SCA1', 1);

INSERT INTO Contacts(Enterprise, Personal, Alternative, Email, [Status])
VALUES('526631220311','112345678911', 'Alternative contact 1', 'mail@default1.com', 1),('5266312203422','112345678912', 'Alternative contact 2', 'mail@default3.com', 1),('5266312203433','112345678913', 'Alternative contact 3', 'mail@default3.com', 1);

INSERT INTO Addresses(Street, AltStreet, City, ZIP, Country, Colonia)
VALUES('First street', 'First alt street', 'Tijuana', 'ZIP11', 'MEX', 'Colonia 1'), ('Second street', 'Second alt street', 'San diego' ,'ZIP22', 'USA', 'Colonia 2'), ('Third street', 'Third alt street', 'Ensenada', 'ZIP33', 'MEX', 'Colonia 3');

INSERT INTO Carriers([Name], Contact, [Address], USDOT, SCT, [Status])
VALUES('TWSA', 1, 1, 1, 1, 1), ('TWS2', 2, 2, 2, 2, 1),('TWS3', 3, 3, 3, 3, 1);

INSERT INTO Trucks(VIN, Manufacturer, Motor, Maintenance, Situation, Insurance, [Status], Carrier, Economic)
VALUES('VINtest1-13324231',1,'Motortestnumber1',1,1,1,1,1, 'Economic 1'),('VINtest2-63324231',2,'Motortestnumber2',2,2,2,1,2, 'Econmic 2'),('VINtest3-93324231',3,'Motortestnumber3',3,3,3,1,3, 'Economic 3');

INSERT INTO Plates(Identifier,[State],Country,Expiration, Truck, [Status])
VALUES('SADC2423E132','CA','USA','2024-07-01',1, 1),('TMEX2323EST2','BC','MEX','2024-09-02',2, 1), ('TXH214E3ESC1','TX','USA','2024-11-03',3, 1),
('SADC2423E132','CA2','USA','2024-07-01',1, 1),('TMEX2323EST2','BC2','MEX','2024-09-02',2, 1), ('TXH214E3ESC1','TX2','USA','2024-11-03',3, 1);
