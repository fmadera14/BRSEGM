using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Consultas.Models
{
    [Table("operaciones")]
    public class Operacion
    {
        [Key]
        [Column("id_operaciones")]
        public int IdOperacion { get; set; }

        [Column("fecha_vencimiento")]
        public DateTime FechaVencimiento { get; set; }

        [Column("comentario")]
        public string Comentario { get; set; }

        [Column("monto")]
        public decimal Monto { get; set; }

        [Column("estado_embargo_no_dispa_dmjud")]
        public string EstadoEmbargoNoDispaDMJUD { get; set; }

        [Column("ejecucion_descripcion_obligacion_garantizada")]
        public string EjecucionDescripcionObligacionGarantizada { get; set; }

        [Column("ejecucion_descripcion_incumplimiento_deudor")]
        public string EjecucionDescripcionIncumplimientoDeudor { get; set; }

        [Column("ejecucion_descripcion_monto_saldo")]
        public decimal EjecucionDescripcionMontoSaldo { get; set; }

        [Column("ejecucion_descripcion_monto_fijado")]
        public decimal EjecucionDescripcionMontoFijado { get; set; }

        [Column("ejecucion_costa_procesales")]
        public decimal EjecucionCostalProcesales { get; set; }

        [Column("nombre_sucursal")]
        public string NombreSucursal { get; set; }

        [Column("otro")]
        public string Otro { get; set; }

        [Column("id_tipo_aviso_inscripcion")]
        public int IdTipoAvisoInscripcion { get; set; }
        [ForeignKey("IdTipoAvisoInscripcion")]
        public TipoAvisoInscripcion TipoAvisoInscripcion { get; set; }

        [Column("id_tipo_conciliacion")]
        public int IdTipoConciliacion { get; set; }
        [ForeignKey("IdTipoConciliacion")]
        public TipoConciliacion TipoConciliacion { get; set; }

        [Column("id_moneda")]
        public int IdMoneda { get; set; }
        [ForeignKey("IdMoneda")]
        public Moneda Moneda { get; set; }

        [Column("id_tipo_garantia")]
        public int IdTipoGarantia { get; set; }
        [ForeignKey("IdTipoGarantia")]
        public TipoGarantia TipoGarantia { get; set; }

        [Column("id_tipo_embargo")]
        public int IdTipoEmbargo { get; set; }
        [ForeignKey("IdTipoEmbargo")]
        public TipoEmbargo TipoEmbargo { get; set; }

        [Column("id_inscripcion")]
        public int IdInscripcion { get; set; }
        [ForeignKey("IdInscripcion")]
        public Inscripcion Inscripcion { get; set; }
    }
}
