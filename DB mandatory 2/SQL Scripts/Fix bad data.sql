USE Partikkels_F2018;
GO

-- Find conflicts
SELECT * 
FROM Instrument_Table
WHERE MaalestedId NOT IN (SELECT MaalestedId FROM Station_Table);
GO

-- Find conflicts
SELECT * 
FROM Measurement_Table
WHERE GeometriId NOT IN (SELECT GeometriId FROM UTM_Table);
GO

-- Create copy of Intrument table
SELECT *
INTO Instrument_Table_Original
FROM Instrument_Table;
GO

-- Delete bad data
DELETE FROM Instrument_Table
WHERE MaalestedId NOT IN (SELECT MaalestedId FROM Station_Table);
GO

-- Create copy of UTM table
SELECT *
INTO UTM_Table_Original
FROM UTM_Table;
GO

-- Add missing id 235
INSERT INTO UTM_Table (GeometriId)
VALUES (235);
GO

-- Add missing id 77
INSERT INTO UTM_Table (GeometriId)
VALUES (77);
GO