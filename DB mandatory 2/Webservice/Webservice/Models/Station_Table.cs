using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Webservice.Models
{
    public partial class Station_Table
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Station_Table()
        {
            Instrument_Table = new HashSet<Instrument_Table>();
            Measurement_Table = new HashSet<Measurement_Table>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaaleStedId { get; set; }

        [StringLength(1)]
        public string Navn { get; set; }

        [StringLength(1)]
        public string Akronym { get; set; }

        public int? UTMX { get; set; }

        public int? UTMY { get; set; }

        public int? UTMZone { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Instrument_Table> Instrument_Table { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Measurement_Table> Measurement_Table { get; set; }
    }
}
