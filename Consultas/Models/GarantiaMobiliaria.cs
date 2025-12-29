using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Consultas.Models
{
    [Table("garantias_mobiliarias")]
    public class GarantiaMobiliaria
    {
        [Key]
        [Column("id_garantia_mobiliaria")]
        public int IdGarantiaMobiliaria { get; set; }

        [Column("id_acreedor")]
        public int IdAcreedor { get; set; }

        [Column("id_deudor")]
        public int IdDeudor { get; set; }

        [Column("id_bien")]
        public int IdBien { get; set; }
        [ForeignKey("IdBien")]
        public Bien Bien { get; set; }

        [Column("id_estado")]
        public int IdEstado { get; set; }
        [ForeignKey("IdEstado")]
        public Estado Estado { get; set; }

        [Column("id_operaciones")]
        public int IdOperaciones { get; set; }
        [ForeignKey("IdOperaciones")]
        public Operacion Operacion { get; set; }
    }
}
