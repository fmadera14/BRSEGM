using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManejoUsuariosRoles.Models
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
