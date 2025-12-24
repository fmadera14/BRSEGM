using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConfiguracioParametros.Models
{
    [Table("tipos_parametro")]
    public class TipoParametro
    {
        [Key]
        [Column("id_tipo_parametro")]
        public int IdTipoParametro { get; set; }

        [Column("descripcion")]
        public string Descripcion { get; set; } = string.Empty;
    }
}
