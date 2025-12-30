using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Consultas.Models
{
    [Table("tipos_identificacion")]
    public class TipoIdentificacion
    {
        [Key]
        [Column("id_tipo_identificacion")]
        public int IdTipoIdentificacion { get; set; }

        [Column("descripcion")]
        public string Descripcion { get; set; }
    }
}
