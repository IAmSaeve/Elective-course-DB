USE Partikkels_F2018;
GO
-- create table with one column of type of XML. 
CREATE TABLE StationNew(
 Id int IDENTITY(1,1) PRIMARY KEY,
 StationXml xml, 
);
GO
--Drop Table StationNew;
-- insert XML data to data base

INSERT INTO StationNew(StationXml)
SELECT * FROM OPENROWSET(BULK 'C:\Users\SebastianRønnovPeter\Desktop\DB mandatory 2\Data\Station.xml', SINGLE_BLOB) AS x;

-- Select all data
SELECT * FROM StationNew;

 -- Reads the XML text provided as input, parses the text by using the MSXML parser (Msxmlsql.dll), 
 -- and provides the parsed document in a state ready for consumption. 
 -- This parsed document is a tree representation of the various nodes in 
 -- the XML document: elements, attributes, text, comments, and so on.

 DECLARE @x xml
SELECT @x=StationXml FROM StationNew
DECLARE @hdoc int -- keep the reference to handler
EXEC sp_xml_preparedocument @hdoc OUTPUT, @x -- system extended stored procedure

SELECT * INTO Station_Table FROM OPENXML (@hdoc, '/DocumentElement/Data', 2)
WITH(
    MaaleStedId int,
    Navn varchar,
    Akronym varchar,
    UTMX int,
    UTMY int,
    UTMZone int
   )

EXEC sp_xml_removedocument @hdoc

SELECT COUNT(*) FROM Station_Table;