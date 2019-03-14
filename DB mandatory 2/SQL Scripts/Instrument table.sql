USE Partikkels_F2018;
GO
-- create table with one column of type of XML. 
CREATE TABLE InstrumentNew(
 Id int IDENTITY(1,1) PRIMARY KEY,
 InstrumentXml xml, 
);
GO
--Drop Table InstrumentNew;
-- insert XML data to data base

INSERT INTO InstrumentNew(InstrumentXml)
SELECT * FROM OPENROWSET(BULK 'C:\Users\SebastianRønnovPeter\Desktop\DB mandatory 2\Data\Instrument.xml', SINGLE_BLOB) AS x;

-- Select all data
SELECT * FROM InstrumentNew;

SELECT TOP 10 *
  FROM InstrumentNew;

-- select single field.
SELECT InstrumentXml.query('/DocumentElement/Data/Resultat') FROM InstrumentNew;
GO 
SELECT InstrumentXml.query('/DocumentElement/Data/datoMaerke') FROM InstrumentNew;
GO
 -- Reads the XML text provided as input, parses the text by using the MSXML parser (Msxmlsql.dll), 
 -- and provides the parsed document in a state ready for consumption. 
 -- This parsed document is a tree representation of the various nodes in 
 -- the XML document: elements, attributes, text, comments, and so on.

 DECLARE @x xml
SELECT @x=InstrumentXml FROM InstrumentNew
DECLARE @hdoc int -- keep the reference to handler
EXEC sp_xml_preparedocument @hdoc OUTPUT, @x -- system extended stored procedure

SELECT * INTO Instrument_Table FROM OPENXML (@hdoc, '/DocumentElement/Data', 2)
WITH(
    OpstillingId int,
    kode nvarchar(50),
    MaalestedId int,
    UdstyrId int
    )

EXEC sp_xml_removedocument @hdoc

SELECT COUNT(*) FROM Instrument_Table;