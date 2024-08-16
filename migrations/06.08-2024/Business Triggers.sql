-- Business Update Triggers Declarations --


USE [TWS Business]
GO

CREATE TRIGGER tgr_Trucks_Update
ON Trucks
AFTER UPDATE
AS
BEGIN
IF EXISTS (
        SELECT 1
        FROM inserted i
        JOIN deleted d ON i.id = d.id
        WHERE EXISTS (
            SELECT i.*
            EXCEPT
            SELECT d.*
        )
    )
    BEGIN
     INSERT INTO Trucks_H ([Sequence], Timemark, [Status], Entity,Vin, Manufacturer, Motor, SCT, Maintenance, Situation, Insurance, SCTH, MaintenanceH, InsuranceH)
		SELECT 
			ISNULL((SELECT MAX([Sequence]) FROM Trucks_H WHERE Entity = d.id), 0) + 1,
			SYSDATETIME(),
			d.[Status],
			d.id,
			d.VIN,
			d.Manufacturer,
			d.Motor,
			d.SCT,
			d.Maintenance,
			d.Situation,
			d.Insurance,
			(SELECT TOP 1 scth.id FROM SCT_H scth WHERE scth.Entity = d.SCT ORDER BY scth.Timemark DESC),
			(SELECT TOP 1 maintenanceh.id FROM Maintenances_H maintenanceh WHERE maintenanceh.Entity = d.Maintenance ORDER BY maintenanceh.Timemark DESC),
			(SELECT TOP 1 insuranceh.id FROM Insurances_H insuranceh WHERE insuranceh.Entity = d.Insurance ORDER BY insuranceh.Timemark DESC)
		FROM deleted d;

		-- Update Relationships Status columns
		UPDATE insurances
		SET 
			insurances.[Status] = i.[Status]
		FROM Insurances insurances
		INNER JOIN inserted i ON insurances.id = i.Insurance
		INNER JOIN deleted d ON i.id = d.Insurance
		WHERE d.[Status] <> i.[Status];

		UPDATE maintenances
		SET 
			maintenances.[Status] = i.[Status]
		FROM Maintenances maintenances
		INNER JOIN inserted i ON maintenances.id = i.Maintenance
		INNER JOIN deleted d ON i.id = d.Maintenance
		WHERE d.[Status] <> i.[Status];

		UPDATE sct
		SET 
			sct.[Status] = i.[Status]
		FROM SCT sct
		INNER JOIN inserted i ON sct.id = i.SCT
		INNER JOIN deleted d ON i.id = d.SCT
		WHERE d.[Status] <> i.[Status];

		UPDATE plate
		SET 
			plate.[Status] = i.[Status]
		FROM Plates plate
		INNER JOIN inserted i ON plate.Truck = i.id
		INNER JOIN deleted d ON plate.Truck = d.id
		WHERE d.[Status] <> i.[Status];
    END
END;
GO


CREATE TRIGGER tgr_SCT_Update
ON SCT
AFTER UPDATE
AS
BEGIN
	IF EXISTS (
        SELECT 1
        FROM inserted i
        JOIN deleted d ON i.id = d.id
        WHERE EXISTS (
            SELECT i.*
            EXCEPT
            SELECT d.*
        )
    )
    BEGIN
      INSERT INTO SCT_H ([Sequence], Timemark, [Status], Entity, [Type], Number, [Configuration])
		SELECT 
			ISNULL((SELECT MAX([Sequence]) FROM SCT_H WHERE Entity = d.id), 0) + 1,
			SYSDATETIME(),
			d.[Status],
			d.id,
			d.[Type],
			d.Number,
			d.[Configuration]
		FROM deleted d;
    END
END;
GO

CREATE TRIGGER tgr_Plates_Update
ON Plates
AFTER UPDATE
AS
BEGIN
	IF EXISTS (
		SELECT 1
		FROM inserted i
		JOIN deleted d ON i.id = d.id
		WHERE EXISTS (
			SELECT i.*
			EXCEPT
			SELECT d.*
		)
	)
	BEGIN
		INSERT INTO Plates_H ([Sequence], Timemark, [Status], Entity, Identifier, [State], Country, Expiration, Truck)
		SELECT 
			ISNULL((SELECT MAX([Sequence]) FROM Plates_H WHERE Entity = d.id), 0) + 1,
			SYSDATETIME(),
			d.[Status],
			d.id,
			d.Identifier,
			d.[State],
			d.Country,
			d.Expiration,
			d.Truck
		FROM deleted d;

		
	END

   

	

END;
GO

CREATE TRIGGER tgr_Insurances_Update
ON Insurances
AFTER UPDATE
AS
BEGIN

	IF EXISTS (
		SELECT 1
		FROM inserted i
		JOIN deleted d ON i.id = d.id
		WHERE EXISTS (
			SELECT i.*
			EXCEPT
			SELECT d.*
		)
	)
	BEGIN
    INSERT INTO Insurances_H ([Sequence], Timemark, [Status], Entity, [Policy], Expiration, Country)
    SELECT 
        ISNULL((SELECT MAX([Sequence]) FROM Insurances_H WHERE Entity = d.id), 0) + 1,
		SYSDATETIME(),
		d.[Status],
		d.id,
		d.[Policy],
		d.Expiration,
		d.Country
    FROM deleted d;
	END
    

END;
GO

CREATE TRIGGER tgr_Maintenances_Update
ON Maintenances
AFTER UPDATE
AS
BEGIN
	IF EXISTS (
		SELECT 1
		FROM inserted i
		JOIN deleted d ON i.id = d.id
		WHERE EXISTS (
			SELECT i.*
			EXCEPT
			SELECT d.*
		)
	)
	BEGIN
     INSERT INTO Maintenances_H ([Sequence], Timemark, [Status], Entity, Anual, Trimestral)
	 SELECT 
        ISNULL((SELECT MAX([Sequence]) FROM Maintenances_H WHERE Entity = d.id), 0) + 1,
		SYSDATETIME(),
		d.[Status],
		d.id,
		d.Anual,
		d.Trimestral
    FROM deleted d;
	END
    

END;
GO


UPDATE Insurances
SET Country = 'NN3'
WHERE id = 1;

SELECT * FROM Insurances;
SELECT * FROM Insurances_H;

UPDATE Maintenances
SET Trimestral = '1999-12-11'
WHERE id = 3;

SELECT * FROM Maintenances;
SELECT * FROM Maintenances_H;

UPDATE Plates
SET [State] = 'NN1'
WHERE id = 1;

SELECT * FROM Plates;
SELECT * FROM Plates_H;

UPDATE SCT
SET Number = '1111111112'
WHERE id = 3;

SELECT * FROM SCT;
SELECT * FROM SCT_H;

UPDATE Trucks
SET Vin = 'VinTriggertest113'
WHERE id = 3;

SELECT * FROM Trucks;
SELECT * FROM Trucks_H;

--Update Status column
UPDATE Trucks
SET [Status] = 1
WHERE id = 2;

SELECT * FROM Trucks;
SELECT * FROM SCT;
SELECT * FROM Maintenances;
SELECT * FROM Insurances;
SELECT * FROM Plates;

SELECT * FROM Trucks_H;
SELECT * FROM SCT_H;
SELECT * FROM Maintenances_H;
SELECT * FROM Insurances_H;
SELECT * FROM Plates_H;
