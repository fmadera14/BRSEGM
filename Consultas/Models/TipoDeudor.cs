using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Consultas.Models
{
    [Table("tipos_deudores")]
    public class TipoDeudor
    {
        [Key]
        [Column("id_tipo_deudor")]
        public int IdTipoDeudor { get; set; }

        [Column("descripcion")]
        public string Descripcion { get; set; }
    }
}
