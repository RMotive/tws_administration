
/****** Generate Tables ******/
create database [TWS Business];
use [TWS Business]

DROP TABLE Plates;
DROP TABLE Trucks;
DROP TABLE Maintenances;
DROP TABLE Manufacturers;
DROP TABLE Insurances;
DROP TABLE Situations;
DROP TABLE SCT;
DROP TABLE HP_Trucks;
DROP TABLE Status;


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
Name nvarchar (25) UNIQUE NOT NULL,
Description nvarchar (100)
);

create table SCT(
id int IDENTITY (1,1) PRIMARY KEY NOT NULL,
Type varchar(6) NOT NULL,
Number varchar(25) NOT NULL,
Configuration varchar(10) NOT NULL
);

create table HP_Trucks(
id int IDENTITY (1,1) PRIMARY KEY NOT NULL,
Creation datetime2 NOT NULL,
VIN varchar(17) UNIQUE NOT NULL,
Motor varchar(16) UNIQUE NOT NULL,
);

create table Status(
id int IDENTITY (1,1) PRIMARY KEY NOT NULL,
Name nvarchar (25) UNIQUE NOT NULL,
Description nvarchar (150)
);

create table Trucks(
 id int IDENTITY(1,1) PRIMARY KEY,
 VIN varchar(17) NOT NULL,
 Manufacturer int NOT NULL,
 Motor varchar(16) NOT NULL,
 HP int NOT NULL,
 Modified DateTime2 NOT NULL,
 Status int NOT NULL,
 SCT int,
 Maintenance int,
 Situation int,
 Insurance int,
 constraint FK@Trucks_Manufacturers foreign key(Manufacturer) references Manufacturers(id),
 constraint FK@Trucks_Maintenances foreign key(Maintenance) references Maintenances(id),
 constraint FK@Trucks_Insurances foreign key(Insurance) references Insurances(id),
 constraint FK@Trucks_Situations foreign key(Situation) references Situations(id),
 constraint FK@Trucks_SCT foreign key(SCT) references SCT(id),
 constraint FK@Trucks_HP foreign key(HP) references HP_Trucks(id),
 constraint FK@Trucks_Status foreign key(Status) references Status(id),

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

INSERT INTO Maintenances(Anual, Trimestral)
VALUES('2025-08-08', '2024-08-08'),('2025-02-02', '2024-9-09'),('2025-10-10', '2024-10-10');

INSERT INTO Manufacturers(Model,Brand,Year)
VALUES('Volvo','AX200','2024'),('Scania','R-Series','2018'),('MAN','TGX','2020');

INSERT INTO Insurances(Policy,Expiration,Country)
VALUES('TestingPolicy1-12331','2024-10-10','MEX'),('TestingPolicy2-32331','2024-09-09','USA'),('TestingPolicy3-53242','2024-11-11','MEX');

INSERT INTO Situations(Name,Description)
VALUES('In Maintenance', 'This unit is out of service'), ('In Transit', 'Work in Transit'), ('Out of service', 'Maintenance Required ');

INSERT INTO Status(Name, Description)
VALUES('Enable','Currently Active in this Solution.'), ('Disable', 'A deleted status. Stored for historical propurses. A disable record has limited features and visibility settings.');

INSERT INTO SCT(Type,Number,Configuration)
VALUES('Type06','SCTtesting1-1232111513111', 'confTest01'),('Type09','SCTtesting2-2232111513111', 'confTest02'),('Type12','SCTtesting3-3232111513111', 'confTest03');

INSERT INTO HP_Trucks(Creation, VIN, Motor)
VALUES(SYSDATETIME(),'VINtest1-13324231', 'Motortestnumber1'), (SYSDATETIME(),'VINtest2-63324231', 'Motortestnumber2'), (SYSDATETIME(),'VINtest3-93324231', 'Motortestnumber3');

INSERT INTO Trucks(VIN, Manufacturer, Motor, SCT, Maintenance, Situation, Insurance, HP, Modified, Status)
VALUES('VINtest1-13324231',1,'Motortestnumber1',1,1,1,1,1,SYSDATETIME(),1),('VINtest2-63324231',2,'Motortestnumber2',2,2,2,2,2,SYSDATETIME(),1),('VINtest3-93324231',3,'Motortestnumber3',3,3,3,3,3,SYSDATETIME(),1);

INSERT INTO Plates(Identifier,State,Country,Expiration, Truck)
VALUES('SADC2423E132','CA','USA','2024-07-01',1),('TMEX2323EST2','BC','MEX','2024-09-02',2), ('TXH214E3ESC1','TX','USA','2024-11-03',3);
