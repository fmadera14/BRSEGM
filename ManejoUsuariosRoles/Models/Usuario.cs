namespace ManejoUsuariosRoles.Models
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public string NombreUsuario { get; set; }
        public string Contraseña { get; set; }
        public int IdRol { get; set; }
        public Rol Rol { get; set; }

        public int IdEstado { get; set; }
        public Estado Estado { get; set; }
    }
}
