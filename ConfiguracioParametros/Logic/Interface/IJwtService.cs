using ConfiguracioParametros.Models;

namespace ConfiguracioParametros.Logic.Interface
{
    public interface IJwtService
    {
        string GenerateToken(Usuario user);
    }
}
