using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Consultas.Models
{
    [Table("tipos_bien_segm")]
    public class TipoBien
    {
        [Key]
        [Column("id_tipo_bien")]
        public int IdTipoBien { get; set; }
        [Column("descripcion")]
        public string Descripcion { get; set; }
    }
}
