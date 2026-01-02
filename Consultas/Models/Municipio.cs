using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Consultas.Models
{
    [Table("municipios_segm")]
    public class Municipio
    {
        [Key]
        [Column("id_municipio")]
        public int IdMunicipio { get; set; }

        [Column("municipio")]
        public string municipio { get; set; }

        [Column("id_region")]
        public int IdRegion { get; set; }

        [Column("id_provincia")]
        public int IdProvincia { get; set; }
    }
}
