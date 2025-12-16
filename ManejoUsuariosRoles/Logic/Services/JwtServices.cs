using ManejoUsuariosRoles.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ManejoUsuariosRoles.Logic.Services
{
    public class JwtService
    {
        private readonly IConfiguration _config;

        public JwtService(IConfiguration config)
        {
            _config = config;
        }

        public string GenerateToken(Usuario user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.IdUsuario.ToString()),
                new Claim("username", user.NombreUsuario),
                new Claim("rol", user.Rol.Descripcion),

                new Claim("perm_lectura", user.Rol.PermisoLectura.ToString()),
                new Claim("perm_escritura", user.Rol.PermisoEscritura.ToString()),
                new Claim("perm_validacion", user.Rol.PermisoValidacion.ToString()),
                new Claim("perm_modificacion", user.Rol.PermisoModificacion.ToString()),
                new Claim("perm_procesar", user.Rol.PermisoProcesar.ToString())
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_config["Jwt:Key"])
            );

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                expires: DateTime.UtcNow.AddHours(4),
                claims: claims,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
