using System.Text;
using System.Security.Cryptography;

namespace ManejoUsuariosRoles.Logic
{
    public class Utils
    {
        static public string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }
    }
}
