namespace ManejoUsuariosRoles.Models
{
    public class Rol
    {
        public int IdRol { get; set; }
        public string Descripcion { get; set; } = string.Empty;
        public bool PermisoLectura { get; set; }
        public bool PermisoEscritura { get; set; }
        public bool PermisoValidacion { get; set; }
        public bool PermisoModificacion { get; set; }
        public bool PermisoProcesar { get; set; }
        public int IdEstado { get; set; }
        public Estado Estado { get; set; }

        public ICollection<Usuario> Usuarios { get; set; }
    }
}
