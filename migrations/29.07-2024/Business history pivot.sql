
/****** Generate Tables ******/
create database [TWS Business];

use [TWS Business]
DROP TABLE PlatesTrucksH;
DROP TABLE Plates_H;
DROP TABLE Trucks_H;
DROP TABLE Carriers_H;
DROP TABLE USDOT_H;
DROP TABLE Approaches_H;
DROP TABLE Maintenances_H;
DROP TABLE Insurances_H;
DROP TABLE SCT_H;
DROP TABLE Trucks_Inventories;
DROP TABLE Yard_Logs;
DROP TABLE Sections;
DROP TABLE Load_Types;
DROP TABLE Plates;
DROP TABLE Trailers;
DROP TABLE Trailers_Externals;
DROP TABLE Trailers_Commons;
DROP TABLE Trailer_Classes;
DROP TABLE Axes;
DROP TABLE Trucks;
DROP TABLE Trucks_Externals;
DROP TABLE Trucks_Commons;
DROP TABLE Drivers;
DROP TABLE Drivers_Externals;
DROP TABLE Drivers_Commons;
DROP TABLE Employees;
DROP TABLE Identifications;
DROP TABLE Carriers;
DROP TABLE Locations;
DROP TABLE Addresses;
DROP TABLE Approaches;
DROP TABLE USDOT;
DROP TABLE SCT;
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

Create table Approaches(
id int IDENTITY (1,1) PRIMARY KEY NOT NULL,
[Status] int not null,
Email varchar(30) not null,
Enterprise varchar(14),
Personal varchar(13),
Alternative varchar(30),

constraint FK_Approaches_Statuses foreign key([Status]) references Statuses(id)

);

Create table Addresses(
id int IDENTITY (1,1) PRIMARY KEY NOT NULL,
Country varchar(3) not null,
[State] varchar(4),
Street varchar(100),
AltStreet varchar(100),
City varchar(30),
ZIP varchar(5),
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
Approach int not null,
[Address] int not null,
USDOT int,
SCT int,

constraint FK_Carriers_Statuses foreign key([Status]) references Statuses(id),
constraint FK_Carriers_Approaches foreign key(Approach) references approaches(id),
constraint FK_Carriers_Addresses foreign key([Address]) references Addresses(id),
constraint FK_Carriers_USDOT foreign key(USDOT) references USDOT(id),
constraint FK_Carriers_SCT foreign key(SCT) references SCT(id),

);

create table Locations(
id int IDENTITY (1,1) PRIMARY KEY NOT NULL,
[Status] int NOT NULL,
[Name] varchar(30) UNIQUE NOT NULL,
[Address] int NOT NULL,

constraint FK_Locations_Addresses foreign key([Address]) references Addresses(id),
constraint FK_Locations_Statuses foreign key([Status]) references Statuses(id)

);

create table Axes(
id int IDENTITY (1,1) PRIMARY KEY NOT NULL,
[Name] varchar(30) NOT NULL,
Quantity int NOT NULL,
[Description] varchar(100)
)

create table Trailer_Classes(
id int IDENTITY (1,1) PRIMARY KEY NOT NULL,
[Name] varchar(30) NOT NULL,
Axis int NOT NULL,
[Description] varchar(100),

constraint FK_Trailers_Axes foreign key(Axis) references Axes(id),

);
Create table  Trailers_Commons(
id int IDENTITY (1,1) PRIMARY KEY NOT NULL,
[Status] int NOT NULL,
Economic Varchar(16) NOT NULL,
Class int NOT NULL,
Carrier int NOT NULL,
Situation int NOT NULL,
[Location] int,

constraint FK_TrailersCommons_TrailersClass foreign key(Class) references Trailer_Classes(id),
constraint FK_TrailersCommons_Carriers foreign key(Carrier) references Carriers(id),
constraint FK_TrailersCommons_Situations foreign key(Situation) references Situations(id),
constraint FK_TrailersCommons_Locations foreign key([Location]) references Locations(id),
constraint FK_TrailersCommons_Statuses foreign key([Status]) references Statuses(id),

);

Create table Trailers_Externals(
id int IDENTITY (1,1) PRIMARY KEY NOT NULL,
[Status] int NOT NULL,
Common int NOT NULL,
constraint FK_TrailersExternals_TrailersCommons foreign key(Common) references Trailers_Commons(id),
constraint FK_TrailersExternals_Statuses foreign key([Status]) references Statuses(id),

);

create table Trailers(
id int IDENTITY (1,1) PRIMARY KEY NOT NULL,
[Status] int NOT NULL,
Common int NOT NULL,
Manufacturer int NOT NULL,
Maintenance int,

constraint FK_Trailers_Manufacturers foreign key(Manufacturer) references Manufacturers(id),
constraint FK_Trailers_Maintenances foreign key(Maintenance) references Maintenances(id),
constraint FK_Trailers_TrailersCommons foreign key(Common) references Trailers_Commons(id),
constraint FK_Trailers_Statuses foreign key([Status]) references Statuses(id),

);
Create table Identifications(
id int IDENTITY(1,1) PRIMARY KEY,
[Status] int NOT NULL,
[Name] varchar(32) NOT NULL,
FatherLastname varchar(32) NOT NULL,
MotherLastName varchar(32) NOT NULL,
Birthday date,

constraint FK_Identifications_Statuses foreign key([Status]) references Statuses(id),

);

create table Employees(
id int IDENTITY(1,1) PRIMARY KEY,
[Status] int NOT NULL,
Identification int NOT NULL,
[Address] int NOT NULL,
Approach int NOT NULL,
CURP varchar(18) UNIQUE NOT NULL,
AntecedentesNoPenaleseExp date NOT NULL,
RFC varchar(12) UNIQUE NOT NULL,
NSS varchar(11) UNIQUE NOT NULL,
IMSSRegistrationDate date NOT NULL,
HiringDate date,
TerminationDate date,

 constraint FK_Employees_Statuses foreign key([Status]) references Statuses(id),
 constraint FK_Employees_Identifications foreign key(Identification) references Identifications(id),
 constraint FK_Employees_Addresses foreign key([Address]) references Addresses(id),
 constraint FK_Employees_Approaches foreign key(Approach) references Approaches(id),
);

create table Drivers_Commons(
id int IDENTITY(1,1) PRIMARY KEY,
[Status] int NOT NULL,
Situation int,
License varchar(12) UNIQUE NOT NULL,

constraint FK_DriversCommons_Situations foreign key(Situation) references Situations(id),
constraint FK_DriversCommons_Statuses foreign key([Status]) references Statuses(id),

);

create table Drivers_Externals(
id int IDENTITY(1,1) PRIMARY KEY,
[Status] int NOT NULL,
Common int NOT NULL,
Identification int NOT NULL

constraint FK_DriversExternals_Statuses foreign key([Status]) references Statuses(id),
constraint FK_DriversExternals_DriversCommons foreign key(Common) references Drivers_Commons(id),
constraint FK_DriversExternal_Identifications foreign key(Identification) references Identifications(id),
);

create table Drivers(
id int IDENTITY(1,1) PRIMARY KEY,
[Status] int NOT NULL,
Employee int NOT  NULL,
Common int NOT NULL,
DriverType varchar(12) NOT NULL,
LicenseExpiration date NOT NULL,
DrugalcRegistrationDate date NOT NULL,
PullnoticeRegistrationDate date NOT NULL,
TWIC varchar(12),
TWICExpiration date,
VISA varchar(12),
VISAExpiration date,
[FAST] varchar(14),
FASTExpiration date,
ANAM varchar(24),
ANAMExpiration date,

constraint FK_Drivers_Statuses foreign key([Status]) references Statuses(id),
constraint FK_Drivers_DriversCommons foreign key(Common) references Drivers_Commons(id),
constraint FK_Drivers_Employees foreign key(Employee) references Employees(id),

);

create table Trucks_Commons(
 id int IDENTITY(1,1) PRIMARY KEY,
 [Status] int NOT NULL,
 VIN varchar(17) UNIQUE NOT NULL,
 Economic varchar(16) NOT NULL,
 Carrier int NOT NULL,
 Situation int,
 [Location] int,

 constraint FK_TrucksCommons_Situations foreign key(Situation) references Situations(id),
 constraint FK_TrucksCommons_Carriers foreign key(Carrier) references Carriers(id),
 constraint FK_TrucksCommons_Locations foreign key([Location]) references Locations(id),
 constraint FK_TrucksCommons_Statuses foreign key([Status]) references Statuses(id),

);


create table Trucks_Externals(
 id int IDENTITY(1,1) PRIMARY KEY,
 [Status] int NOT NULL,
 Common int NOT NULL,

 constraint FK_TrucksExternals_TrucksCommons foreign key(Common) references Trucks_Commons(id),
);

create table Trucks(
 id int IDENTITY(1,1) PRIMARY KEY,
 [Status] int NOT NULL,
 Common int NOT NULL,
 Manufacturer int NOT NULL,
 Motor varchar(16) UNIQUE NOT NULL,
 Maintenance int,
 Insurance int,

 constraint FK_Trucks_TrucksCommons foreign key(Common) references Trucks_Commons(id),
 constraint FK_Trucks_Manufacturers foreign key(Manufacturer) references Manufacturers(id),
 constraint FK_Trucks_Maintenances foreign key(Maintenance) references Maintenances(id),
 constraint FK_Trucks_Insurances foreign key(Insurance) references Insurances(id),
 constraint FK_Trucks_Statuses foreign key([Status]) references Statuses(id),

);
create table Plates(
id int IDENTITY (1,1) PRIMARY KEY NOT NULL,
[Status] int NOT NULL,
Identifier varchar(12) NOT NULL,
[State] varchar(4) NOT NULL,
Country varchar(3) NOT NULL,
Expiration date NOT NULL,
Truck int,
Trailer int,

 constraint FK_Plates_TrucksCommons foreign key(Truck) references Trucks_Commons(id),
 constraint FK_Plates_TrailersCommons foreign key(Trailer) references Trailers_Commons(id),
 constraint FK_Plates_Statuses foreign key([Status]) references Statuses(id),

);


create table Load_Types(
id int IDENTITY (1,1) PRIMARY KEY NOT NULL,
[Name] varchar(32) UNIQUE NOT NULL,
[Description] varchar(100)
);

create table Sections(
id int IDENTITY (1,1) PRIMARY KEY NOT NULL,
[Status] int not null,
Yard int NOT NULL,
[Name] varchar(32) NOT NULL,
Capacity int NOT NULL,
Ocupancy int NOT NULL,

constraint FK_Sections_Statuses foreign key([Status]) references Statuses(id),
constraint FK_Sections_Locations foreign key(Yard) references Locations(id),
);

create table Yard_Logs(
id int IDENTITY (1,1) PRIMARY KEY NOT NULL,
[Entry] bit NOT NULL,
[Timestamp] datetime2,
Truck int,
TruckExternal int,
Trailer int,
TrailerExternal int,
LoadType int NOT NULL,
Guard int NOT NULL,
Gname varchar(100) NOT NULL,
Section int NOT NULL,
FromTo varchar(100) NOT NULL,
Damage bit NOT NULL,
TTPicture varchar(MAX) NOT NULL,
DmgEvidence varchar(MAX),
Driver int,
DriverExternal int,

constraint FK_YardLogs_Drivers foreign key(Driver) references Drivers(id),
constraint FK_YardLogs_DriversExternals foreign key(DriverExternal) references Drivers_Externals(id),
constraint FK_YardLogs_Truck foreign key(Truck) references Trucks(id),
constraint FK_YardLogs_TruckExternals foreign key(TruckExternal) references Trucks_Externals(id),

constraint FK_YardLogs_Trailer foreign key(Trailer) references Trailers(id),
constraint FK_YardLogs_TrailerExternals foreign key(TrailerExternal) references Trailers_Externals(id),
constraint FK_YardLogs_LoadType foreign key(LoadType) references Load_Types(id),
constraint FK_YardLogs_Sections foreign key(Section) references Sections(id),

);

create table Trucks_Inventories(
id int IDENTITY (1,1) PRIMARY KEY NOT NULL,
EntryDate datetime2 NOT NULL,
section int NOT NULL,
truck int,
truckExternal int,

constraint FK_TrucksInventory_TrucksExternal foreign key(truckExternal) references Trucks_Externals(id),
constraint FK_TrucksInventory_Trucks foreign key(Truck) references Trucks(id),

constraint FK_TrucksInventory_Sections foreign key(Section) references Sections(id),
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

 Create table Approaches_H(
  id int IDENTITY (1,1) PRIMARY KEY NOT NULL,
  [Sequence] int not null DEFAULT 1,
 Timemark datetime2 not null,
 [Status] int not null,

 Entity int not null,
 Enterprise varchar(13),
Personal varchar(13),
Alternative varchar(30),
Email varchar(30) not null,

 constraint FK_ApproachesH_Approaches foreign key(Entity) references approaches(id),
 constraint FK_ApproachesH_Statuses foreign key([Status]) references Statuses(id)
 );

 create table Carriers_H(
 id int IDENTITY (1,1) PRIMARY KEY NOT NULL,
 [Sequence] int not null DEFAULT 1,
 Timemark datetime2 not null,
 [Status] int not null,

 Entity int not null,
 [Name] Varchar(20) not null,
 ApproachH int,
 [Address] int not null,
 USDOTH int,
 SCTH int,
 constraint FK_CarrierH_Carrier foreign key(Entity) references Carriers(id),
 constraint FK_CarrierH_Statuses foreign key([Status]) references Statuses(id),
 constraint FK_CarrierH_ApproachesH foreign key(ApproachH) references Approaches_H(id),
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
 );

USE [TWS Business]
GO

CREATE PROCEDURE Set_Situation
	@commonID INT,
	@commonTableName varchar(max),
	@SituationName varchar(max)
AS BEGIN
	DECLARE @situationID INT; 
	SELECT @situationID = id FROM Situations where [Name] = @SituationName;
	DECLARE @Query NVARCHAR(max) = 'Update ' + @commonTableName + ' SET ' + 'Situation = ' + @situationID  + ' Where id = ' + @commonID;
	EXEC sp_executesql @Query ;
END;
GO
CREATE PROCEDURE Set_Location
	@sectionID INT,
	@truckID INT
AS BEGIN
	DECLARE @locationID INT;
	DECLARE @truckCommonID INT;
	SELECT @locationID = Yard From Sections where id = @sectionID;
	SELECT @truckCommonID = common From Trucks where id = @truckID;
	-- Check if the common table has a diferent value to modify in location column;
	IF EXISTS(
		SELECT 1 FROM Trucks_Commons where id = @truckCommonID AND (
		([Location] <> @locationID)
		OR ([Location] IS NULL AND @locationID IS NOT NULL)
		OR ([Location] IS NOT NULL AND @locationID IS NULL)
		)
	) BEGIN
		UPDATE Trucks_Commons 
		SET [location] = @locationID Where id = @truckCommonID;
	END
END;
GO

CREATE PROCEDURE Set_SectionOcupancy
	@sectionID INT,
	@added Bit
	AS BEGIN
	-- Validate if values are added or not
	DECLARE @trucksTable Varchar(6) = 'Trucks';
	DECLARE @trucksExternalTable Varchar(16) = 'Trucks_Externals';
	
	
	-- Check id add or substract value;
	IF @added = 1
	BEGIN
		UPDATE Sections 
		SET Ocupancy = Ocupancy + 1 WHERE id = @sectionID;
	END
	ELSE BEGIN
		UPDATE Sections 
		SET Ocupancy = Ocupancy - 1 WHERE id = @sectionID;
	END

	--Check if Situation value has changed;
END;
GO

CREATE TRIGGER tgr_YardLogs_Insert
ON Yard_Logs
AFTER INSERT
AS BEGIN
	UPDATE Yard_Logs
    SET Timestamp = GETDATE()
    FROM inserted i
    WHERE Yard_Logs.Id = i.Id;
END;
GO

CREATE TRIGGER YardLogs_InsertInto_TrucksInventories
ON Yard_Logs
AFTER INSERT
AS BEGIN
	DECLARE @entryBit BIT;
	DECLARE @truckKey INT;
	DECLARE @truckExternalKey INT;
	DECLARE @newSectionID INT;

	SELECT @entryBit = i.Entry, @truckKey = i.Truck, @truckExternalKey = i.TruckExternal, @newSectionID = i.Section FROM inserted i;
	
	IF @entryBit = 1
	BEGIN
		-- Insert new record into inventories if @entryBit is true;
		INSERT INTO Trucks_Inventories(EntryDate, truck, truckExternal, section)
		SELECT SYSDATETIME(), i.Truck, i.TruckExternal, i.Section FROM inserted i;
	END
	ELSE BEGIN
		-- Remove old record from inventories if @entryBit is false;
		DELETE FROM Trucks_Inventories where truckExternal = @truckExternalKey OR truck = @truckKey;
		EXEC Set_SectionOcupancy @sectionID = @newSectionID, @added = 0; -- IF a previous record not exist, then subtract -1 to its section.

	END
	
END;
GO

CREATE TRIGGER TruckInventories_Management
ON Trucks_Inventories
AFTER INSERT
AS BEGIN

	BEGIN TRANSACTION;

	DECLARE @newTruck INT;
	DECLARE @newExternalTruck INT;
	DECLARE @newID INT;
	DECLARE @newSectionID INT;
	DECLARE @oldSectionID INT;

	DECLARE insert_cursor CURSOR FOR
    SELECT Truck, truckExternal, id, section FROM inserted;

	OPEN insert_cursor;
	FETCH NEXT FROM insert_cursor INTO @newTruck, @newExternalTruck, @newID, @newSectionID;

	WHILE @@FETCH_STATUS = 0
    BEGIN
		BEGIN TRY
			-- Check if new truck is already stored in the inventory;
			IF EXISTS (
				SELECT 1
				FROM Trucks_Inventories
				WHERE (truck = @newTruck OR truckExternal = @newExternalTruck) AND Id <> @newID
			) 
			BEGIN

			-- Get old section id;
            SELECT @oldSectionID = section
            FROM Trucks_Inventories
            WHERE (truck = @newTruck OR truckExternal = @newExternalTruck) AND Id <> @newID;

			-- Remove old duplicated record based on foreign keys.
			DELETE FROM Trucks_Inventories WHERE (truck = @newTruck OR truckExternal = @newExternalTruck) AND Id <> @NewId;
			
			-- Check if the section ID has changed;
			-- Note: Not is necesary to update the section column, as the insert transaction itself does this.
                IF @oldSectionID <> @newSectionID
                BEGIN
                    -- subtract -1 to old section ocupancy AND sum + 1 to new section;
					EXEC Set_SectionOcupancy @sectionID = @newSectionID, @added = 1;
					EXEC Set_SectionOcupancy @sectionID = @oldSectionID, @added = 0;
					-- Set location
					EXEC Set_Location @sectionID = @newSectionID, @truckID = @newTruck;
					-- VALIDATE FOR EXTERNAL TRUCKS
					DECLARE @truckCommonID INT;
					SELECT @truckCommonID = common From Trucks where id = @newTruck;
					EXEC Set_Situation @commonID = @truckCommonID, @commonTableName = 'Trucks', @situationName = 'In Transit';

                END --ELSE DO NOTHING
				--UPDATE TRUCK LOCATION

			END
			ELSE BEGIN
				-- IF a previous record not exist, then sum +1 to its section.
				EXEC Set_SectionOcupancy @sectionID = @newSectionID, @added = 1;
				-- Set location
				EXEC Set_Location @sectionID = @newSectionID, @truckID = @newTruck;
			END

			COMMIT TRANSACTION;
		END TRY
		BEGIN CATCH
			ROLLBACK TRANSACTION;
		END CATCH;
		FETCH NEXT FROM insert_cursor INTO @newTruck, @newExternalTruck, @newID, @newSectionID;
	END;

	CLOSE insert_cursor;
    DEALLOCATE insert_cursor;
END;
GO

-- Declaration for trucks and trailers inventory situations;
DECLARE @situationParked varchar(max) = 'Parked in the yard';
DECLARE @situationInTransit varchar(max) = 'In Transit';

INSERT INTO Statuses([Name], [Description])
VALUES('Enable','Currently Active in this Solution.'), ('Disable', 'A deleted status. Stored for historical propurses . A disable record has limited features and visibility settings.');

INSERT INTO Maintenances(Anual, Trimestral, [Status])
VALUES('2025-08-08', '2024-08-08', 1),('2025-02-02', '2024-9-09', 1),('2025-10-10', '2024-10-10', 1);

INSERT INTO Manufacturers(Model, Brand, [Year])
VALUES('Volvo','AX200','2024'),('Scania','R-Series','2018'),('MAN','TGX','2020');

INSERT INTO Insurances([Policy], Expiration, Country, [Status])
VALUES('TestingPolicy1-12331','2024-10-10','MEX', 1),('TestingPolicy2-32331','2024-09-09','USA', 1),('TestingPolicy3-53242','2024-11-11','MEX', 1);

INSERT INTO Situations([Name],[Description])
VALUES(@situationInTransit, 'This unit is parked in some section in the yard.'), (@situationInTransit, 'Work in Transit'), ('Out of service', 'Maintenance Required ');

INSERT INTO SCT([Type], Number, [Configuration], [Status])
VALUES('Type06','SCTtesting1-1232111513111', 'confTest01', 1),('Type09','SCTtesting2-2232111513111', 'confTest02', 1),('Type12','SCTtesting3-3232111513111', 'confTest03', 1);

INSERT INTO USDOT(MC, SCAC, [Status])
VALUES('MCtest1','SCA1', 1),('MCtest1','SCA1', 1),('MCtest1','SCA1', 1);

INSERT INTO approaches(Enterprise, Personal, Alternative, Email, [Status])
VALUES('526631220311','112345678911', 'Alternative contact 1', 'mail@default1.com', 1),('5266312203422','112345678912', 'Alternative contact 2', 'mail@default3.com', 1),('5266312203433','112345678913', 'Alternative contact 3', 'mail@default3.com', 1);

INSERT INTO Addresses(Street, AltStreet, City, ZIP, Country, Colonia)
VALUES('First street', 'First alt street', 'Tijuana', 'ZIP11', 'MEX', 'Colonia 1'), ('Second street', 'Second alt street', 'San diego' ,'ZIP22', 'USA', 'Colonia 2'), ('Third street', 'Third alt street', 'Ensenada', 'ZIP33', 'MEX', 'Colonia 3');

INSERT INTO Carriers([Name], Approach, [Address], USDOT, SCT, [Status])
VALUES('TWSA', 1, 1, 1, 1, 1), ('TWS2', 2, 2, 2, 2, 1),('TWS3', 3, 3, 3, 3, 1);

INSERT INTO Locations([Status], [Name], [Address])
VALUES(1, 'TWSA HQ', 1),(1, 'LA Yard', 2), (1, 'Tijuana Yard #2', 1);

INSERT INTO Axes([Name], [Description], Quantity)
VALUES('Single axle', 'Single axle. For small trailers boxes', 1), ('Tandem axle', 'Multiple axle positioned near each other,  ', 2),  ('Triple axle', 'Triple Axle. axis are positiones at equal distances each other through the trailer', 3);

INSERT INTO Trailer_Classes([Name], [Description], Axis)
VALUES('Dry van', 'Full closed trailer, ideal for dry load.', 1),('Reefer', 'Refrigerated closed trailer', 2),('Flatbed', 'Open flat platform. ideal for big vehicules or materials transport.', 1);

INSERT INTO Trailers_Commons([Status],Class, Carrier, Situation, [Location], Economic)
VALUES (1,1,1,1,1, 'Economic 1'), (1,2,2,2,2, 'Economic 2'), (1,3,3,3,3, 'Economic 3');
 
INSERT INTO Trailers_Externals([Status], Common)
VALUES (1, 1), (1, 2), (1, 3);

INSERT INTO Trailers([Status], Common, Manufacturer, Maintenance)
VALUES (1,1,1,1), (1,2,2,2), (1,3,3,3);

INSERT INTO Identifications([Status], [Name], FatherLastname, MotherLastName, Birthday)
VALUES(1, 'ARTURO', 'RAMIREZ', 'MANCILLAS', SYSDATETIME()), (1, 'CARLOS JAVIER', 'SANCHEZ', 'GUZMAN', SYSDATETIME()), (1, 'URIAS', 'ARMENTA', 'CESAR', null);

INSERT INTO Employees([Status], Identification, CURP, AntecedentesNoPenaleseExp, [Address], Approach, RFC, NSS, IMSSRegistrationDate, HiringDate, TerminationDate)
VALUES(1, 1, 'RAMA830213HBCMNR03', SYSDATETIME(), 1, 1, 'RFC TEST1111', 'NSS test 11', SYSDATETIME(), SYSDATETIME(), SYSDATETIME()), (1, 2, 'SAGC771004HBCNZR07', SYSDATETIME(), 2, 2, 'RFC TEST2222', 'NSS test 22', SYSDATETIME(), SYSDATETIME(), SYSDATETIME()), (1, 3, 'UIAC770225HSLRRS06', SYSDATETIME(), 3, 3, 'RFC TEST3333', 'NSS test 33', SYSDATETIME(), SYSDATETIME(), SYSDATETIME());

INSERT INTO Drivers_Commons([Status], License, Situation)
VALUES(1, 'BCN0212895', 1), (1, 'BCN207292', 2), (1, 'BCN0215006', 3);

INSERT INTO Drivers_Externals([Status], Common, Identification)
VALUES(1, 1, 1),(1, 2, 2),(1, 3, 3);

INSERT INTO Drivers([Status], Employee, DriverType, Common, LicenseExpiration, TWIC,TWICExpiration, VISA, VISAExpiration, [FAST], FASTExpiration, ANAM, ANAMExpiration, DrugalcRegistrationDate, PullnoticeRegistrationDate)
VALUES(1, 1, 'Binational', 1, SYSDATETIME(), '28250230', SYSDATETIME(), 'TJT005336269', SYSDATETIME(), '411000013467', SYSDATETIME(), 'SATGN2017091940000001885', SYSDATETIME(), SYSDATETIME(), SYSDATETIME()), (1, 2, 'Mexican', 2, SYSDATETIME(), '28237819', SYSDATETIME(), 'TJT005044433', SYSDATETIME(), '41100103485400', SYSDATETIME(), 'SATGN2018060440000020884', SYSDATETIME(), SYSDATETIME(), SYSDATETIME()), (1, 3, 'Mexican', 3, SYSDATETIME(), '28247760', SYSDATETIME(), 'MEX041410210', SYSDATETIME(), '41100220935700', SYSDATETIME(), 'SATGN2022032740000116226', SYSDATETIME(), SYSDATETIME(), SYSDATETIME());

INSERT INTO Trucks_Commons([Status], VIN, Economic, Carrier, Situation)
VALUES (1, 'VINtest1-13324231',  'Economic 1', 1, 1), (1, 'VINtest2-63324231', 'Economic 2', 2, 2), (1, 'VINtest3-93324231', 'Economic 3', 2, 2);

INSERT INTO Trucks_Externals([Status], Common)
VALUES (1,1),(1,2),(1,3);

INSERT INTO Trucks(Common, Manufacturer, Motor, Maintenance, Insurance, [Status])
VALUES(1,1,'Motortestnumber1',1,1,1),(2,2,'Motortestnumber2',2,2,1),(3,3,'Motortestnumber3',3,3,1);

INSERT INTO Plates(Identifier,[State],Country,Expiration, Truck, Trailer, [Status])
VALUES
('SADC2423E132','CA','USA','2024-07-01',1,null,1),('TMEX2323EST2','BC','MEX','2024-09-02',2, null,1), ('TXH214E3ESC1','TX','USA','2024-11-03',3, null, 1),
('SADC2423E132','CA2','USA','2024-07-01',1, null, 1),('TMEX2323EST2','BC2','MEX','2024-09-02',2,null, 1), ('TXH214E3ESC1','TX2','USA','2024-11-03',3, null, 1),

-- Trailer plates MXN/USA--
('TRAILER23E13','CAT','USA','2024-07-01',null, 1, 1),('TRAILERTMEX2','BC2','USA','2024-09-02', null, 2, 1), ('TRAILERTXH24','TX2','USA','2024-11-03', null, 3, 1),
('TRAILER23E11','CAT','MEX','2024-07-01',null, 1, 1),('TRAILERTMEX2','BC2','MEX','2024-09-02', null, 2, 1), ('TRAILERTXH21','TX2','MEX','2024-11-03', null, 3, 1);

INSERT INTO Load_Types([Name], [Description])
VALUES('Loaded', 'DESCRIPTION 1'), ('Empty', 'DESCRIPTION 2'), ('Botado', 'DESCRIPTION 3');

INSERT INTO Sections([Name], Capacity, Ocupancy, [Status], Yard)
VALUES('A', 20, 0, 1, 1), ('B', 10, 0, 1, 2), ('C', 25, 0, 1, 3);

INSERT INTO Yard_Logs([Entry], Truck, TruckExternal, Trailer, TrailerExternal, LoadType, Guard, Gname, Section, FromTo, Damage, TTPicture, DmgEvidence, Driver, DriverExternal)
VALUES(1, 1, null, 1, null, 1, 1,'Guard 1', 1, 'Coca Cola el Florido', 0, 'Truck and trailer picture 1', null, 1, null), (1, 2, null, 2, null, 2, 2,'Guard 2', 2, 'Coca Cola el Florido 2', 1, 'Truck and trailer picture 1', 'Damage picture 1', 2, null), (1, 3, null, 3, null, 3, 3,'Guard 3', 3, 'Coca Cola el Florido 3', 0, 'Truck and trailer picture 1', null, 2, null);

