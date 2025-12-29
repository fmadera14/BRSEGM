using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Consultas.Models
{
    [Table("estado_ministerio")]
    public class EstadoMinisterio
    {
        [Key]
        [Column("id_estado_ministerio")]
        public int IdEstadoMinisterio { get; set; }

        [Column("descripcion")]
        public string Descripcion { get; set; }
    }
}
