using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Consultas.Models
{
    [Table("deudores")]
    public class Deudor
    {
        [Key]
        [Column("id_deudor")]
        public int IdDeudor { get; set; }

        [Column("id_tipo_deudor")]
        public int IdTipoDeudor { get; set; }
        [ForeignKey("IdTipoDeudor")]
        public TipoDeudor TipoDeudor { get; set; }

        [Column("id_tipo_identificacion")]
        public int IdTipoIdentificacion { get; set; }
        [ForeignKey("IdTipoIdentificacion")]
        public TipoIdentificacion TipoIdentificacion { get; set; }

        [Column("identificacion")]
        public string Identificacion { get; set; }

        [Column("nombre_deudor")]
        public string NombreDeudor { get; set; }

        [Column("id_municipio")]
        public int IdMunicipio { get; set; }
        [ForeignKey("IdMunicipio")]
        public Municipio Municipio { get; set; }

        [Column("domicilio")]
        public string Domicilio { get; set; }

        [Column("correo_electronico")]
        public string CorreoElectronico { get; set; }

        [Column("telefono")]
        public string Telefono { get; set; }

        [Column("es_nacional")]
        public bool EsNacional { get; set; }

        [Column("id_inscripcion")]
        public int IdInscripcion { get; set; }
        [ForeignKey("IdInscripcion")]
        public Inscripcion Inscripcion { get; set; }
    }
}
