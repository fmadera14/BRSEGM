namespace ManejoUsuariosRoles.Data.DTOs
{
    public class ResetPasswordDto
    {
        public int IdUsuario { get; set; }
        public string NewPassword { get; set; } = null!;
    }
}
