using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConfiguracioParametros.Models
{
    [Table("segm_parametros")]
    public class SEGMParametro
    {
        [Key]
        [Column("id_parametro")]
        public int IdParametro { get; set; }
        [Column("valor")]
        public string Valor { get; set; } = string.Empty;
        [Column("nombre_clave")]
        public string NombreClave { get; set; } = string.Empty;
        [Column("descripcion")]
        public string Descripcion { get; set; } = string.Empty;
        [Column("id_estado")]
        public int IdEstado { get; set; }
        [ForeignKey("IdEstado")]
        public Estado Estado { get; set; }
        [Column("id_usuario")]
        public int IdUsuario { get; set; }
        [ForeignKey("IdUsuario")]
        public Usuario Usuario { get; set; }
    }
}
