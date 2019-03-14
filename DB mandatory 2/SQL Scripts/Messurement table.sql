--CREATE DATABASE Partikkels_F2018;
--GO

USE Partikkels_F2018;
GO
-- create table with one column of type of XML. 
CREATE TABLE MeasurementsNew(
 Id int IDENTITY(1,1) PRIMARY KEY,
 MeasureXml xml, 
);
GO
--Drop Table MeasurementsNew;
-- insert XML data to data base

INSERT INTO MeasurementsNew(MeasureXml)
SELECT * FROM OPENROWSET(BULK 'C:\Users\SebastianRønnovPeter\Desktop\DB mandatory 2\Data\Measurements.xml', SINGLE_BLOB) AS x;

-- Select all data
SELECT * FROM MeasurementsNew;

-- select single field.
SELECT MeasureXml.query('/DocumentElement/Data/Resultat') FROM MeasurementsNew;
GO 
SELECT MeasureXml.query('/DocumentElement/Data/datoMaerke') FROM MeasurementsNew;
GO
 -- Reads the XML text provided as input, parses the text by using the MSXML parser (Msxmlsql.dll), 
 -- and provides the parsed document in a state ready for consumption. 
 -- This parsed document is a tree representation of the various nodes in 
 -- the XML document: elements, attributes, text, comments, and so on.

DECLARE @x xml
SELECT @x=MeasureXml FROM MeasurementsNew
DECLARE @hdoc int -- keep the reference to handler
EXEC sp_xml_preparedocument @hdoc OUTPUT, @x -- system extended stored procedure

SELECT * INTO Measurement_Table FROM OPENXML (@hdoc, '/DocumentElement/Data', 2)
WITH(
     Id int,
     datoMaerke  dateTime,
	 MaaleStedId int,
	 GeometriId  int,
     Resultat    float,
	 EnhedId     int,
	 StofId      int
     )

EXEC sp_xml_removedocument @hdoc

SELECT COUNT(*) FROM Measurement_Table;


select * from Measurement_Table;
select MaaleStedId from Measurement_Table GROUP BY MaaleStedId
select GeometriId from Measurement_Table GROUP BY GeometriId;
select COUNT(GeometriId) from Measurement_Table WHERE GeometriId=77 GROUP BY GeometriId;
select GeometriId from UTM_Table GROUP BY GeometriId;
select * from UTM_Table order BY GeometriId;

ALTER TABLE Measurement_Table ADD
            CONSTRAINT IMeasurement_Table_FK00 FOREIGN KEY (MaaleStedId) REFERENCES Station_Table(MaaleStedId)

ALTER TABLE Measurement_Table ADD --Conflict missing 77
            CONSTRAINT IMeasurement_Table_FK01 FOREIGN KEY (GeometriId) REFERENCES UTM_Table(GeometriId)

ALTER TABLE Measurement_Table 
  ALTER COLUMN StofId varchar(4) NOT NULL
GO

ALTER TABLE Measurement_Table ADD 
            CONSTRAINT IMeasurement_Table_FK02 FOREIGN KEY (StofId) REFERENCES Stof_Table(StofId)

ALTER TABLE Measurement_Table ADD 
            CONSTRAINT IMeasurement_Table_FK03 FOREIGN KEY (EnhedId) REFERENCES Unit_Table(EnhedId)

--CREATE TABLE LVS_Table_final(
--     Id int IDENTITY(1,1) PRIMARY KEY,
--     MaalestedId varchar(4),
--	 Maalested   varchar(50),
--     DatoMaerke  dateTime,
--	 StofId      varchar(4),
--	 StofNavn    varchar(10),
--	 EnhedId     varchar(4),
--	 Enhed       varchar(10),
--     Resultat    float,
--	 UdstyrId    varchar(4),
--	 Navn        varchar(10)
--);
--GO

--INSERT INTO LVS_Table_final(MaalestedId,Maalested,DatoMaerke,StofId,StofNavn,EnhedId,Enhed,Resultat,UdstyrId,Navn)
--SELECT DISTINCT MaalestedId,Maalested,DatoMaerke,StofId,StofNavn,EnhedId,Enhed,Resultat,UdstyrId,Navn FROM LVS_Table;	


--select * from LVS_Table_final;

--DROP TABLE LVS_Table;
--GO

--Create View Resault_PM10 AS
--SELECT Id, DatoMaerke, StofNavn, Resultat FROM LVS_Table_final WHERE StofId='65';
--GO
--SELECT * FROM Resault_PM10;
--GO

--Create View Resault_PM2_5 AS
--SELECT Id, DatoMaerke, StofNavn, Resultat FROM LVS_Table_final WHERE StofId='66';
--GO
--SELECT * FROM Resault_PM2_5;
--GO