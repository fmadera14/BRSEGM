using Consultas.Logic.Interface;
using Consultas.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Consultas.Logic.Services
{
    public class JwtService(IConfiguration config) : IJwtService
    {
        private readonly IConfiguration _config = config;

        public string GenerateToken(Usuario user)
        {
            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub, user.IdUsuario.ToString()),
                new("username", user.NombreUsuario),
                new("rol", user.Rol.Descripcion),

                new("perm_lectura", user.Rol.PermisoLectura.ToString()),
                new("perm_escritura", user.Rol.PermisoEscritura.ToString()),
                new("perm_validacion", user.Rol.PermisoValidacion.ToString()),
                new("perm_modificacion", user.Rol.PermisoModificacion.ToString()),
                new("perm_procesar", user.Rol.PermisoProcesar.ToString())
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_config["Jwt:Key"])
            );

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                expires: DateTime.UtcNow.AddHours(4),
                claims: claims,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
