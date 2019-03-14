USE Partikkels_F2018;
GO

ALTER TABLE dbo.Compound_Table
ALTER COLUMN StofId int NOT NULL;
GO
ALTER TABLE dbo.Compound_Table ADD PRIMARY KEY (StofId);
GO

ALTER TABLE dbo.Equipment_Table
ALTER COLUMN UdstyrId int NOT NULL;
GO
ALTER TABLE dbo.Equipment_Table ADD PRIMARY KEY (UdstyrId);
GO

ALTER TABLE dbo.Instrument_Table
ALTER COLUMN OpstillingId int NOT NULL;
GO
ALTER TABLE dbo.Instrument_Table ADD PRIMARY KEY (OpstillingId);
GO

ALTER TABLE dbo.Measurement_Table
ALTER COLUMN Id int NOT NULL;
GO
ALTER TABLE dbo.Measurement_Table ADD PRIMARY KEY (Id);
GO

ALTER TABLE dbo.Station_Table
ALTER COLUMN MaaleStedId int NOT NULL;
GO
ALTER TABLE dbo.Station_Table ADD PRIMARY KEY (MaaleStedId);
GO

ALTER TABLE dbo.Units_Table
ALTER COLUMN EnhedId int NOT NULL;
GO
ALTER TABLE dbo.Units_Table ADD PRIMARY KEY (EnhedId);
GO

ALTER TABLE dbo.UTM_Table
ALTER COLUMN GeometriId int NOT NULL;
GO
ALTER TABLE dbo.UTM_Table ADD PRIMARY KEY (GeometriId);
GO
