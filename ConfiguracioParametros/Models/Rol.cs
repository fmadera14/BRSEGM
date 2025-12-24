using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ConfiguracioParametros.Models
{
    [Table("roles")]
    public class Rol
    {
        [Key]
        [Column("id_rol")]
        public int IdRol { get; set; }

        [Column("descripcion")]
        public string Descripcion { get; set; }

        [Column("permiso_lectura")]
        public bool PermisoLectura { get; set; }

        [Column("permiso_escritura")]
        public bool PermisoEscritura { get; set; }

        [Column("permiso_validacion")]
        public bool PermisoValidacion { get; set; }

        [Column("permiso_modificacion")]
        public bool PermisoModificacion { get; set; }

        [Column("permiso_procesar")]
        public bool PermisoProcesar { get; set; }

        [Column("id_estado")]
        public int IdEstado { get; set; }

        [ForeignKey("IdEstado")]
        public Estado Estado { get; set; }

        public ICollection<Usuario> Usuarios { get; set; }
    }
}
