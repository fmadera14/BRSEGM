namespace ManejoUsuariosRoles.Logic.Interface
{
    public interface IJwtService
    {
        string GenerateToken(Models.Usuario user);
    }
}
