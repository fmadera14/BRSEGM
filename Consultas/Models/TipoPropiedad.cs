using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Consultas.Models
{
    [Table("tipos_propiedad")]
    public class TipoPropiedad
    {
        [Key]
        [Column("id_tipo_propiedad")]
        public int IdTipoPropiedad { get; set; }
        [Column("descripcion")]
        public string Descripcion { get; set; }
    }
}
