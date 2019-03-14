USE Partikkels_F2018;
GO

ALTER TABLE dbo.Instrument_Table
ADD FOREIGN KEY (UdstyrId)
REFERENCES dbo.Equipment_Table(UdstyrId);
GO

ALTER TABLE dbo.Instrument_Table
ADD FOREIGN KEY (MaaleStedId)
REFERENCES dbo.Station_Table(MaaleStedId);
GO

ALTER TABLE dbo.Measurement_Table
ADD FOREIGN KEY (StofId)
REFERENCES dbo.Compound_Table(StofId);
GO

ALTER TABLE dbo.Measurement_Table
ADD FOREIGN KEY (MaaleStedId)
REFERENCES dbo.Station_Table(MaaleStedId);
GO

ALTER TABLE dbo.Measurement_Table
ADD FOREIGN KEY (EnhedId)
REFERENCES dbo.Units_Table(EnhedId);
GO

ALTER TABLE dbo.Measurement_Table
ADD FOREIGN KEY (GeometriId)
REFERENCES dbo.UTM_Table(GeometriId);
GO
