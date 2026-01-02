using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Consultas.Models
{
    [Table("tipos_garantia")]
    public class TipoGarantia
    {
        [Key]
        [Column("id_tipo_garantia")]
        public int IdTipoGarantia { get; set; }

        [Column("descripcion")]
        public string Descripcion { get; set; }
    }
}
