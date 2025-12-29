using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Consultas.Models
{
    [Table("tipos_embargos")]
    public class TipoEmbargo
    {
        [Key]
        [Column("id_tipo_embargo")]
        public int IdTipoEmbargo { get; set; }

        [Column("descripcion")]
        public string Descripcion { get; set; }
    }
}
