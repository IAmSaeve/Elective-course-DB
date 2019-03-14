using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Webservice.Models
{
    public partial class UTM_Table_Original
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int GeometriId { get; set; }

        public int? UTMX { get; set; }

        public int? UTMY { get; set; }

        public int? UTMZone { get; set; }
    }
}
