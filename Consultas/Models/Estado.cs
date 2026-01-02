using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Consultas.Models
{
    [Table("estados")]
    public class Estado
    {
        [Key]
        [Column("id_estado")]
        public int IdEstado { get; set; }

        [Column("descripcion")]
        public string Descripcion { get; set; }
    }
}
