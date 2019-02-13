-- Say which fucking db to use stupid ass software!
USE SchoolDB;
GO

-- Create shit table
CREATE TABLE [Subject] (
 Id INT  IDENTITY (1,1) PRIMARY KEY NOT NULL,
 SubjectName NVARCHAR(50)
)
GO

-- Create shit table
CREATE TABLE Faculty (
 Id INT  IDENTITY (1,1) PRIMARY KEY NOT NULL,
  KnowledgeDevision NVARCHAR(50)
)
GO

-- Create shit table
CREATE TABLE Department (
 Id INT  IDENTITY (1,1) PRIMARY KEY NOT NULL,
 FacId INT FOREIGN KEY REFERENCES Faculty(Id),
 [Address] NVARCHAR(50)
)
GO

-- Create shit table
CREATE TABLE Staff (
 Id INT  IDENTITY (1,1) PRIMARY KEY NOT NULL,
  SubId INT FOREIGN KEY REFERENCES [Subject](Id),
  FacId INT FOREIGN KEY REFERENCES Faculty(Id),
  FirstName NVARCHAR(50),
  LastName NVARCHAR(50),
  [Address] NVARCHAR(50),
  Profession NVARCHAR(50)
)
GO

-- Create shit table
CREATE TABLE Buildings (
 Id INT  IDENTITY (1,1) PRIMARY KEY NOT NULL,
 BuildingName NVARCHAR(50)
)
GO

-- Create shit table
CREATE TABLE ClassRooms (
 BuildId INT FOREIGN KEY REFERENCES Buildings(Id),
 RoomNr INT,
 PRIMARY KEY (BuildId, RoomNr)
)
GO

-- Create shit table
CREATE TABLE Classes (
 Id INT  IDENTITY (1,1) PRIMARY KEY NOT NULL,
 RoomNr INT,
 BuildId INT,
 FOREIGN KEY (BuildId, RoomNr) REFERENCES ClassRooms(BuildId, RoomNr),
 SubId INT FOREIGN KEY REFERENCES [Subject](Id),
 ClassName NVARCHAR(50)
)
GO

-- Create shit table
CREATE TABLE Students (
 Id INT  IDENTITY (1,1) PRIMARY KEY NOT NULL,
 ClassId INT FOREIGN KEY REFERENCES Classes(Id),
 FirstName NVARCHAR(50),
 LastName NVARCHAR(50),
 [Address] NVARCHAR(50)
)
GO

-- Insert shit
INSERT INTO Buildings (BuildingName) VALUES ('Dick DORTHE');
GO

INSERT INTO [Subject](SubjectName) VALUES ('How to rip your dick right off');
GO

INSERT INTO Staff(SubId, FacId, FirstName, LastName, [Address], Profession)
	   VALUES (1, 1, 'Mr. Stein', 'Strong man', 'Køge', 'SQL with a pinch of web'); -- Eller put comma her
	   -- (1, 1, 'Mr. Stein', 'Strong man', 'Køge', 'SQL with a pinch of web'); So far and so on, yes
GO

INSERT INTO Classes(RoomNr, BuildId, SubId, ClassName) VALUES (125, 1, 1, 'Dick Rippers');
GO

INSERT INTO Students(ClassId, FirstName, LastName, [Address]) VALUES (2, 'Sæve', 'Inteligenstman', 'Vera smart ppl stret');
GO

-- Insert lotz of shit
DECLARE @i int = 0

WHILE @i < 1001
BEGIN
    SET @i = @i + 1;
	INSERT INTO ClassRooms (BuildId, RoomNr) VALUES (1, FLOOR(@i));
END
GO

-- Delete shit
DELETE FROM ClassRooms;
GO

-- Select shit from every where
EXEC sp_MSForEachTable 'SELECT ''?'', COUNT(*) FROM ?'
GO
