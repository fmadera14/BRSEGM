using ManejoUsuariosRoles.Data.DTOs;
using ManejoUsuariosRoles.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using ManejoUsuariosRoles.Logic.Interface;

namespace ManejoUsuariosRoles.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IJwtService _jwt;

        public AuthController(AppDbContext context, IJwtService jwt)
        {
            _context = context;
            _jwt = jwt;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var user = await _context.Usuarios
                .Include(u => u.Rol)
                .Include(u => u.Estado)
                .FirstOrDefaultAsync(u => u.NombreUsuario == dto.NombreUsuario);

            if (user == null)
                return Unauthorized("Usuario no existe");

            if (!BCrypt.Net.BCrypt.Verify(dto.Password, user.Contraseña))
                return Unauthorized("Credenciales inválidas");

            if (user.Estado == null || user.Estado.IdEstado != 1) // activo
                return Unauthorized("Usuario inactivo");

            var token = string.Empty;

            try
            {
                token = _jwt.GenerateToken(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }

            return Ok(new { token });
        }   

        [Authorize(Policy = "PERM_escritura")]
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            var existingUser = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.NombreUsuario == dto.NombreUsuario);
            if (existingUser != null)
                return BadRequest("El nombre de usuario ya está en uso");
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(dto.Password);
            var newUser = new Models.Usuario
            {
                NombreUsuario = dto.NombreUsuario,
                Contraseña = hashedPassword,
                IdRol = dto.IdRol,
                IdEstado = 1 // activo por defecto
            };
            _context.Usuarios.Add(newUser);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Usuario registrado exitosamente" });
        }
    }
}
