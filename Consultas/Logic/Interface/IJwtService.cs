using Consultas.Models;

namespace Consultas.Logic.Interface
{
    public interface IJwtService
    {
        string GenerateToken(Usuario user);
    }
}
