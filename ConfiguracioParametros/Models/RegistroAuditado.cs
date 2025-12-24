using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ConfiguracioParametros.Models
{
    [Table("registros_auditados")]
    public class RegistroAuditado
    {
        [Key]
        [Column("id_registro_auditado")]
        public int IdRegistroAuditado { get; set; }
        [Column("tabla_afectada")]
        public string TablaAfectada { get; set; } = null!;
        [Column("id_registro")]
        public int IdRegistro { get; set; }
    }
}
