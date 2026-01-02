using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Consultas.Models
{
    [Table("acreedores")]
    public class Acreedor
    {
        [Key]
        [Column("id_acreedor")]
        public int IdAcreedor { get; set; }

        [Column("identificacion")]
        public string Identificacion { get; set; }

        [Column("id_tipo_identificacion")]
        public int IdTipoIdentificacion { get; set; }
        [ForeignKey("IdTipoIdentificacion")]
        public TipoIdentificacion TipoIdentificacion { get; set; }

        [Column("nombre_acreedor")]
        public string NombreAcreedor { get; set; }

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

        [Column("fecha_exclusion")]
        public DateTime FechaExclusion { get; set; }
    }
}
