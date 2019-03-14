USE Partikkels_F2018
GO

CREATE TABLE CompoundNew(
 Id int IDENTITY(1,1) PRIMARY KEY,
 CompoundXml xml, 
);
GO
--Drop Table CompoundNew;
-- insert XML data to data base

INSERT INTO CompoundNew(CompoundXml)
SELECT * FROM OPENROWSET(BULK 'C:\Users\SebastianRønnovPeter\Desktop\DB mandatory 2\Data\Compound.xml', SINGLE_BLOB) AS x;

-- Select all data
SELECT * FROM CompoundNew;

SELECT TOP 10 *
  FROM CompoundNew;


 -- Reads the XML text provided as input, parses the text by using the MSXML parser (Msxmlsql.dll), 
 -- and provides the parsed document in a state ready for consumption. 
 -- This parsed document is a tree representation of the various nodes in 
 -- the XML document: elements, attributes, text, comments, and so on.

 DECLARE @x xml
SELECT @x=CompoundXml FROM CompoundNew
DECLARE @hdoc int -- keep the reference to handler
EXEC sp_xml_preparedocument @hdoc OUTPUT, @x -- system extended stored procedure

SELECT * INTO Compound_Table FROM OPENXML (@hdoc, '/DocumentElement/Data', 2)
WITH(
     StofId int,
     StofNavn varchar(50)
     )

EXEC sp_xml_removedocument @hdoc

SELECT COUNT(*) FROM Compound_Table;

SELECT * FROM Compound_Table;