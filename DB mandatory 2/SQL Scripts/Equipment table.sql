USE Partikkels_F2018;
GO
-- create table with one column of type of XML. 
CREATE TABLE EquipmentNew(
 Id int IDENTITY(1,1) PRIMARY KEY,
 EquipmentXml xml, 
);
GO
--Drop Table EquipmentNew;
-- insert XML data to data base

INSERT INTO EquipmentNew(EquipmentXml)
SELECT * FROM OPENROWSET(BULK 'C:\Users\SebastianRønnovPeter\Desktop\DB mandatory 2\Data\Equipment.xml', SINGLE_BLOB) AS x;

-- Select all data
SELECT * FROM EquipmentNew;

SELECT TOP 10 *
  FROM EquipmentNew;


 -- Reads the XML text provided as input, parses the text by using the MSXML parser (Msxmlsql.dll), 
 -- and provides the parsed document in a state ready for consumption. 
 -- This parsed document is a tree representation of the various nodes in 
 -- the XML document: elements, attributes, text, comments, and so on.

 DECLARE @x xml
SELECT @x=EquipmentXml FROM EquipmentNew
DECLARE @hdoc int -- keep the reference to handler
EXEC sp_xml_preparedocument @hdoc OUTPUT, @x -- system extended stored procedure

SELECT * INTO Equipment_Table FROM OPENXML (@hdoc, '/DocumentElement/Data', 2)
WITH(
 UdstyrId int,
    Navn varchar
     )

EXEC sp_xml_removedocument @hdoc

SELECT COUNT(*) FROM Equipment_Table;