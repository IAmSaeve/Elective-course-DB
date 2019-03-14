using Webservice.Models;

namespace Webservice
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ParticleModel : DbContext
    {
        public ParticleModel()
            : base("name=ParticleModel")
        {
        }

        public virtual DbSet<Compound_Table> Compound_Table { get; set; }
        public virtual DbSet<Equipment_Table> Equipment_Table { get; set; }
        public virtual DbSet<Instrument_Table> Instrument_Table { get; set; }
        public virtual DbSet<Measurement_Table> Measurement_Table { get; set; }
        public virtual DbSet<Station_Table> Station_Table { get; set; }
        public virtual DbSet<Units_Table> Units_Table { get; set; }
        public virtual DbSet<UTM_Table> UTM_Table { get; set; }
        public virtual DbSet<Instrument_Table_Original> Instrument_Table_Original { get; set; }
        public virtual DbSet<UTM_Table_Original> UTM_Table_Original { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Compound_Table>()
                .Property(e => e.StofNavn)
                .IsUnicode(false);

            modelBuilder.Entity<Equipment_Table>()
                .Property(e => e.Navn)
                .IsUnicode(false);

            modelBuilder.Entity<Station_Table>()
                .Property(e => e.Navn)
                .IsUnicode(false);

            modelBuilder.Entity<Station_Table>()
                .Property(e => e.Akronym)
                .IsUnicode(false);

            modelBuilder.Entity<Units_Table>()
                .Property(e => e.Navn)
                .IsUnicode(false);
        }
    }
}
