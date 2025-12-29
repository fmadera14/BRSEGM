using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Consultas.Models
{
    [Table("tipos_conciliacion")]
    public class TipoConciliacion
    {
        [Key]
        [Column("id_tipo_conciliacion")]
        public int IdTipoConciliacion { get; set; }

        [Column("descripcion")]
        public string Descripcion { get; set; }
    }
}
