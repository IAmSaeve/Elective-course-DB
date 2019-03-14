USE Partikkels_F2018;
GO
-- create table with one column of type of XML. 
CREATE TABLE UnitsNew(
 Id int IDENTITY(1,1) PRIMARY KEY,
 UnitsXml xml, 
);
GO
--Drop Table UnitsNew;
-- insert XML data to data base

INSERT INTO UnitsNew(UnitsXml)
SELECT * FROM OPENROWSET(BULK 'C:\Users\SebastianRønnovPeter\Desktop\DB mandatory 2\Data\Units.xml', SINGLE_BLOB) AS x;

-- Select all data
SELECT * FROM UnitsNew;

SELECT TOP 10 *
  FROM UnitsNew;

-- select single field.
SELECT UnitsXml.query('/DocumentElement/Data/Resultat') FROM UnitsNew;
GO 
SELECT UnitsXml.query('/DocumentElement/Data/datoMaerke') FROM UnitsNew;
GO
 -- Reads the XML text provided as input, parses the text by using the MSXML parser (Msxmlsql.dll), 
 -- and provides the parsed document in a state ready for consumption. 
 -- This parsed document is a tree representation of the various nodes in 
 -- the XML document: elements, attributes, text, comments, and so on.

 DECLARE @x xml
SELECT @x=UnitsXml FROM UnitsNew
DECLARE @hdoc int -- keep the reference to handler
EXEC sp_xml_preparedocument @hdoc OUTPUT, @x -- system extended stored procedure

SELECT * INTO Units_Table FROM OPENXML (@hdoc, '/DocumentElement/Data', 2)
WITH(
    EnhedId int,
    Navn varchar(50)
     )

EXEC sp_xml_removedocument @hdoc

SELECT COUNT(*) FROM Units_Table;