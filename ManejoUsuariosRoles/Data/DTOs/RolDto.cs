namespace ManejoUsuariosRoles.Data.DTOs
{
    public class RolDto
    {
        public int Id { get; set; }
        public string Descripcion { get; set; } = string.Empty;

        public bool PermisoLectura { get; set; }
        public bool PermisoEscritura { get; set; }
        public bool PermisoValidacion { get; set; }
        public bool PermisoModificacion { get; set; }
        public bool PermisoProcesar { get; set; }
    }
}
