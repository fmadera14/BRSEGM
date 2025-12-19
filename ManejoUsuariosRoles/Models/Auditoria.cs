using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManejoUsuariosRoles.Models
{
    [Table("auditorias")]
    public class Auditoria
    {
        [Key]
        [Column("id_auditoria")]
        public int IdAuditoria { get; set; }
        [Column("id_registro_auditado")]
        public int IdRegistroAuditado { get; set; }
        [ForeignKey("IdRegistroAuditado")]
        public RegistroAuditado RegistroAuditado { get; set; }

        [Column("tipo_operacion")]
        public string TipoOperacion { get; set; } = string.Empty;
        [Column("id_usuario")]
        public int IdUsuario { get; set; }
        [ForeignKey("IdUsuario")]
        public Usuario Usuario { get; set; }
        [Column("fecha_operacion")]
        public DateTime FechaOperacion { get; set; }
    }
}
