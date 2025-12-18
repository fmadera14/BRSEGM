namespace ManejoUsuariosRoles.Data.DTOs
{
    public class UpdateRolDto
    {
        public string Descripcion { get; set; } = null!;
        public bool PermisoLectura { get; set; }
        public bool PermisoEscritura { get; set; }
        public bool PermisoValidacion { get; set; }
        public bool PermisoModificacion { get; set; }
        public bool PermisoProcesar { get; set; }
        public int IdEstado { get; set; }
    }
}