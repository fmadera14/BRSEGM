namespace ManejoUsuariosRoles.Data.DTOs
{
    public class LoginDto
    {
        public string NombreUsuario { get; set; }
        public string Password { get; set; }
    }

    public class RegisterDto
    {
        public string NombreUsuario { get; set; }
        public string Password { get; set; }
        public int IdRol { get; set; }
    }
}
