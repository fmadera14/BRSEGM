using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Consultas.Models
{
    [Table("tipos_avisos_inscripcion")]
    public class TipoAvisoInscripcion
    {
        [Key]
        [Column("id_tipo_aviso")]
        public int IdTipoAvisoInscripcion { get; set; }

        [Column("descripcion")]
        public string Descripcion { get; set; }
    }
}
