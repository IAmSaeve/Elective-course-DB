using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Webservice.Models
{
    public partial class Instrument_Table
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int OpstillingId { get; set; }

        [StringLength(50)]
        public string kode { get; set; }

        public int? MaalestedId { get; set; }

        public int? UdstyrId { get; set; }

        public virtual Equipment_Table Equipment_Table { get; set; }

        public virtual Station_Table Station_Table { get; set; }
    }
}
