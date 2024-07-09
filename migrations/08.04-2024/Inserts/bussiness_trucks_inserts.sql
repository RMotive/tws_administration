
/****** Generate Inserts ******/
use [TWS Business]

INSERT INTO Maintenances(Anual, Trimestral)
VALUES('2025-08-08', '2024-08-08'),('2025-02-02', '2024-9-09'),('2025-10-10', '2024-10-10');

INSERT INTO Manufacturers(Model,Brand,Year)
VALUES('Volvo','AX200','2024'),('Scania','R-Series','2018'),('MAN','TGX','2020');

INSERT INTO Insurances(Policy,Expiration,Country)
/* Country ISO3 */
VALUES('TestingPolicy1-12331','2024-10-10','MEX'),('TestingPolicy2-32331','2024-09-09','USA'),('TestingPolicy3-53242','2024-11-11','MEX');

INSERT INTO Situations(Name,Description)
VALUES('In Maintenance', 'This unit is out of service'), ('In Transit', 'Work in Transit'), ('Out of service', 'Maintenance Required ');

INSERT INTO SCT(Type,Number,Configuration)
VALUES('Type06','SCTtesting1-1232111513111', 'confTest01'),('Type09','SCTtesting2-2232111513111', 'confTest02'),('Type12','SCTtesting3-3232111513111', 'confTest03');

INSERT INTO Trucks(VIN,Manufacturer,Motor,SCT,Maintenance,Situation,Insurance)
VALUES('VINtest1-13324231',1,'Motortestnumber1',1,1,1,1),('VINtest2-63324231',2,'Motortestnumber2',2,2,2,2),('VINtest3-93324231',3,'Motortestnumber3',3,3,3,3);

INSERT INTO Plates(Identifier,State,Country,Expiration, Truck)
/* State code & Country ISO3 --- Mexico States abreviated to 2 letters*/
VALUES('SADC2423E132','CA','USA','2024-07-01',1),('TMEX2323EST2','BC','MEX','2024-09-02',2), ('TXH214E3ESC1','TX','USA','2024-11-03',3);

/* Select all Data*/
select * from Maintenances;
select * from Plates;
select * from Manufacturers	;
select * from Insurances;
select * from Situations;
select * from Trucks;
select * from SCT;
