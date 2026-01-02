using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Consultas.Models
{
    [Table("inscripciones")]
    public class Inscripcion
    {
        [Key]
        [Column("id_inscripcion")]
        public int IdInscripcion { get; set; }

        [Column("numero_registro_micm")]
        public int NumeroRegistroMICM { get; set; }

        [Column("id_estado_ministerio")]
        public int IdEstadoMinisterio { get; set; }
        [ForeignKey("IdEstadoMinisterio")]
        public EstadoMinisterio EstadoMinisterio { get; set; }

        [Column("numero_solicitud")]
        public int NumeroSolicitud { get; set; }
    }
}
