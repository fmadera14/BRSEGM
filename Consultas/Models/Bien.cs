using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Consultas.Models
{
    [Table("bienes")]
    public class Bien
    {
        [Key]
        [Column("id_bien")]
        public int IdBien { get; set; }

        [Column("id_tipo_propiedad")]
        public int IdTipoPropiedad { get; set; }
        [ForeignKey("IdTipoPropiedad")]
        public TipoPropiedad TipoPropiedad { get; set; }

        [Column("id_tipo_bien")]
        public int IdTipoBien { get; set; }
        [ForeignKey("IdTipoBien")]
        public TipoBien TipoBien { get; set; }

        [Column("numero_serial")]
        public string NumeroSerial { get; set; }

        [Column("descripcion_bien")]
        public string DescripcionBien { get; set; }

        [Column("incorporacion_inmueble")]
        public string IncorporacionInmueble { get; set; }

        [Column("incorporacion_inmueble_matricula")]
        public string IncorporacionInmuebleMatricula { get; set; }

        [Column("incorporacion_inmueble_distrito_catastral")]
        public string IncorporacionInmuebleDistritoCatastral { get; set; }

        [Column("incorporacion_inmueble_nueva_parcela")]
        public string IncorporacionInmuebleNuevaParcela { get; set; }

        [Column("registro_donde_se_encuentra_inscrito")]
        public string RegistroDondeSeEncuentraInscrito { get; set; }

        [Column("ubicacion")]
        public string Ubicacion { get; set; }

        [Column("id_inscripcion")]
        public int IdInscripcion { get; set; }
        [ForeignKey("IdInscripcion")]
        public Inscripcion Inscripcion { get; set; }
    }
}
