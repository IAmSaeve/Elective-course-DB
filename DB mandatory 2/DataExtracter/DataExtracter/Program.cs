using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DataExtracter
{
    class Program
    {
        static void Main(string[] args)
        {
            // DB data lists
            var compoundDictionary = new Dictionary<dynamic, dynamic>();
            var equipmentDictionary = new Dictionary<dynamic, dynamic>();
            var instrumentDictionary = new Dictionary<dynamic, dynamic>();
            var measurementDictionary = new Dictionary<dynamic, dynamic>();
            var stationDictionary = new Dictionary<dynamic, dynamic>();
            var unitsDictionary = new Dictionary<dynamic, dynamic>();
            var utmDictionary = new Dictionary<dynamic, dynamic>();

            try
            {
                // Build connection string
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder
                {
                    DataSource = "DESKTOP-M1EUKU8",
                    InitialCatalog = "Partikkels_F2018",
                    IntegratedSecurity = true
                };

                // Connect to SQL
                Console.WriteLine("Connecting to SQL Server...");
                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();
                    Console.WriteLine("Done.\n");

                    // Queries
                    const string compoundCommand = "SELECT * FROM Compound_Table";
                    const string equipmentCommand = "SELECT * FROM Equipment_Table";
                    const string instrumentCommand = "SELECT * FROM Instrument_Table";
                    const string measurementCommand = "SELECT * FROM Measurement_Table";
                    const string stationCommand = "SELECT * FROM Station_Table";
                    const string unitsCommand = "SELECT * FROM Units_Table";
                    const string utmCommand = "SELECT * FROM UTM_Table";

                    // Run SQL commands
                    ExecuteCompoundCommand(connection, compoundCommand, compoundDictionary);
                    ExecuteEquipmentCommand(connection, equipmentCommand, equipmentDictionary);
                    ExecuteInstrumentCommand(connection, instrumentCommand, instrumentDictionary);
                    ExecuteMeasurementCommand(connection, measurementCommand, measurementDictionary);
                    ExecuteStationCommand(connection, stationCommand, stationDictionary);
                    ExecuteUnitsCommand(connection, unitsCommand, unitsDictionary);
                    ExecuteUTMCommand(connection, utmCommand, utmDictionary);
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }

            var newData = new ConcurrentBag<object>();
            var newData2 = new ConcurrentBag<object>();
            var mJson = "";
            var mJson2 = "";
            var serialized = false;

            Console.WriteLine("\nBegin building data with relations...");
            Parallel.ForEach(measurementDictionary.Take(1),
                m =>
                {
                    var ma = from x in stationDictionary
                        where x.Key == m.Value.MaaleStedId
                        select new {x.Value.Navn, x.Value.Akronym};
                    Console.WriteLine(ma);
                    Task.Run(() => newData.Add(new
                    {
                        Id = m.Key,
                        m.Value.datoMaerke,
                        MaaleStedId = stationDictionary.Where(s => s.Key == m.Value.MaaleStedId).Select(x => new {x.Value.Navn, x.Value.Akronym}),
                        GeometriId = utmDictionary.Where(g => g.Key == m.Value.GeometriId).Select(g => new {g.Value.UTMX, g.Value.UTMY, g.Value.UTMZone}),
                        m.Value.Resultat,
                        EnhedId = unitsDictionary.Where(u => u.Key == m.Value.EnhedId).Select(u => new {u.Value.Navn}),
                        StofId = compoundDictionary.Where(c => c.Key == m.Value.StofId).Select(c => new {c.Value.StofNavn})
                    }));
                });
            Parallel.ForEach(instrumentDictionary.Take(1), i =>
            {
                Task.Run(() => newData2.Add(new
                {
                    OpstillingId = i.Key,
                    i.Value.Kode,
                    MaaleStedId = stationDictionary.Where(s => s.Key == i.Value.MaaleStedId).Select(x => new { x.Value.Navn, x.Value.Akronym }),
                    UdstyrId = equipmentDictionary.Where(e => e.Key == i.Value.UdstyrId).Select(x => new { x.Value.Navn })
                }));
            });

            while (!serialized)
            {
                if (newData.Count == measurementDictionary.Count)
                {
                    // GC.Collect(); // Call garbage collector here if memory usage is to high
                    Console.WriteLine("\nStarting serializing...");
                    mJson = JsonConvert.SerializeObject(newData);
                    mJson2 = JsonConvert.SerializeObject(newData2);
                    serialized = true;
                }

                Thread.Sleep(1000);
            }

            Console.WriteLine("\nWriting data to file...");
            File.WriteAllText("C:\\Users\\SebastianRønnovPeter\\Desktop\\Measurement.json", mJson);
            File.WriteAllText("C:\\Users\\SebastianRønnovPeter\\Desktop\\Instrument.json", mJson2);
            Console.ReadKey();
        }

        #region Commands
        private static void ExecuteCompoundCommand(SqlConnection connection, string compoundCommand, Dictionary<dynamic, dynamic> compoundDictionary)
        {
            using (SqlCommand command = new SqlCommand(compoundCommand, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // Console.WriteLine(reader.GetValue(0) + " : " + reader.GetValue(1)); // For debugging
                        compoundDictionary.Add(reader.GetValue(0), new
                        {
                            StofNavn = reader.GetValue(1)
                        });
                    }
                    Console.WriteLine("Compound data has been loaded");
                }
            }
        }
        private static void ExecuteEquipmentCommand(SqlConnection connection, string equipmentCommand, Dictionary<dynamic, dynamic> equipmentDictionary)
        {
            using (SqlCommand command = new SqlCommand(equipmentCommand, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        equipmentDictionary.Add(reader.GetValue(0), new
                        {
                            Navn = reader.GetValue(1)
                        });
                    }
                    Console.WriteLine("Equipment data has been loaded");
                }
            }
        }
        private static void ExecuteInstrumentCommand(SqlConnection connection, string instrumentCommand, Dictionary<dynamic, dynamic> instrumentDictionary)
        {
            using (SqlCommand command = new SqlCommand(instrumentCommand, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        instrumentDictionary.Add(reader.GetValue(0), new
                        {
                            Kode = reader.GetValue(1),
                            MaaleStedId = reader.GetValue(2),
                            UdstyrId = reader.GetValue(3)
                        });
                    }
                    Console.WriteLine("Instrument data has been loaded");
                }
            }
        }
        private static void ExecuteMeasurementCommand(SqlConnection connection, string measurementCommand, Dictionary<dynamic, dynamic> measurementDictionary)
        {
            using (SqlCommand command = new SqlCommand(measurementCommand, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        measurementDictionary.Add(reader.GetValue(0), new
                        {
                            datoMaerke = reader.GetValue(1),
                            MaaleStedId = reader.GetValue(2),
                            GeometriId = reader.GetValue(3),
                            Resultat = reader.GetValue(4),
                            EnhedId = reader.GetValue(5),
                            StofId = reader.GetValue(6)
                        });
                    }
                    Console.WriteLine("Measurement data has been loaded");
                }
            }
        }
        private static void ExecuteStationCommand(SqlConnection connection, string stationCommand, Dictionary<dynamic, dynamic> stationDictionary)
        {
            using (SqlCommand command = new SqlCommand(stationCommand, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        stationDictionary.Add(reader.GetValue(0), new
                        {
                            Navn = reader.GetValue(1),
                            Akronym = reader.GetValue(2)
                        });
                    }
                    Console.WriteLine("Station data has been loaded");
                }
            }
        }
        private static void ExecuteUnitsCommand(SqlConnection connection, string unitsCommand, Dictionary<dynamic, dynamic> unitsDictionary)
        {
            using (SqlCommand command = new SqlCommand(unitsCommand, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        unitsDictionary.Add(reader.GetValue(0), new
                        {
                            Navn = reader.GetValue(1)
                        });
                    }
                    Console.WriteLine("Unit data has been loaded");
                }
            }
        }
        private static void ExecuteUTMCommand(SqlConnection connection, string utmCommand, Dictionary<dynamic, dynamic> utmDictionary)
        {
            using (SqlCommand command = new SqlCommand(utmCommand, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        utmDictionary.Add(reader.GetValue(0), new
                        {
                            UTMX = reader.GetValue(1),
                            UTMY = reader.GetValue(2),
                            UTMZone = reader.GetValue(3)
                        });
                    }
                    Console.WriteLine("UTM data has been loaded");
                }
            }
        }
        #endregion
    }
}
