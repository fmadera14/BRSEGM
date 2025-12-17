namespace ManejoUsuariosRoles.Data.DTOs
{
    public class UserDetailDto
    {
        public int Id { get; set; }
        public string NombreUsuario { get; set; }
        public string Estado { get; set; }

        public RolDto Rol { get; set; }
    }
}
