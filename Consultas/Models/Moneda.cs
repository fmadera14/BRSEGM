using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Consultas.Models
{
    [Table("monedas")]
    public class Moneda
    {
        [Key]
        [Column("id_moneda")]
        public int IdMoneda { get; set; }
        [Column("descripcion")]
        public string Descripcion { get; set; }
    }
}
