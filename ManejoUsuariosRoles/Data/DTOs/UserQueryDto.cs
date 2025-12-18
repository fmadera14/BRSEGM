
namespace ManejoUsuariosRoles.Data.DTOs
{
    public class UserQueryDto
    {
        public string? Nombre { get; set; }
        public int? IdRol { get; set; }
        public int? IdEstado { get; set; }

        public string? OrderBy { get; set; } = "IdUsuario";
        public string? OrderDir { get; set; } = "asc";
    }

}
