using ManejoUsuariosRoles.Data.DTOs;
using ManejoUsuariosRoles.Data;
using ManejoUsuariosRoles.Logic.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ManejoUsuariosRoles.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthController : Controller
    {
        private readonly AppDbContext _context;
        private readonly JwtService _jwt;

        public AuthController(AppDbContext context, JwtService jwt)
        {
            _context = context;
            _jwt = jwt;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var user = await _context.Usuarios
                .Include(u => u.Rol)
                .FirstOrDefaultAsync(u => u.NombreUsuario == dto.NombreUsuario);

            if (user == null)
                return Unauthorized("Usuario no existe");

            if (!BCrypt.Net.BCrypt.Verify(dto.Password, user.Contraseña))
                return Unauthorized("Credenciales inválidas");

            if (user.Estado.IdEstado != 1) // activo
                return Unauthorized("Usuario inactivo");

            var token = _jwt.GenerateToken(user);

            return Ok(new { token });
        }
    }
}
