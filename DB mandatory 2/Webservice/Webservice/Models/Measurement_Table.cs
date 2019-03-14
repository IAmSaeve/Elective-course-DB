using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Webservice.Models
{
    public partial class Measurement_Table
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public DateTime? datoMaerke { get; set; }

        public int? MaaleStedId { get; set; }

        public int? GeometriId { get; set; }

        public double? Resultat { get; set; }

        public int? EnhedId { get; set; }

        public int? StofId { get; set; }

        public virtual Compound_Table Compound_Table { get; set; }

        public virtual Units_Table Units_Table { get; set; }

        public virtual UTM_Table UTM_Table { get; set; }

        public virtual Station_Table Station_Table { get; set; }
    }
}
