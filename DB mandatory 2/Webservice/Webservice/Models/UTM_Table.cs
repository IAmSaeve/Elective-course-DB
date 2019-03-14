using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Webservice.Models
{
    public partial class UTM_Table
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public UTM_Table()
        {
            Measurement_Table = new HashSet<Measurement_Table>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int GeometriId { get; set; }

        public int? UTMX { get; set; }

        public int? UTMY { get; set; }

        public int? UTMZone { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Measurement_Table> Measurement_Table { get; set; }
    }
}
