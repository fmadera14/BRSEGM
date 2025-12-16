using ManejoUsuariosRoles.Data;
using ManejoUsuariosRoles.Data.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ManejoUsuariosRoles.Logic;

namespace ManejoUsuariosRoles.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : Controller
    {
        private readonly AppDbContext _context;

        public UsuarioController(AppDbContext context)
        {
            _context = context;
        }

        // Sign up
        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromBody] RegisterDto dto)
        {
            var exists = await _context.Usuarios.AnyAsync(u => u.NombreUsuario == dto.NombreUsuario);

            if (exists) return BadRequest("El usuario ya existe");

            var usuario = new Models.Usuario
            {
                NombreUsuario = dto.NombreUsuario,
                Hash_Password = Utils.HashPassword(dto.Password),
                IdRol = dto.IdRol
            };

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return Ok("Usuario creado exitosamente");
        }

        // Login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.NombreUsuario == dto.NombreUsuario);
            if (usuario == null)
            {
                return Unauthorized("Credenciales inválidas");
            }

            var hash = Utils.HashPassword(dto.Password);

            if (usuario.Hash_Password != hash)
            {
                return Unauthorized("Credenciales inválidas");
            }

            return Ok(new { usuario.IdUser, usuario.NombreUsuario, usuario.IdRol });
        }

        // Listar usuarios
        [HttpGet]
        public async Task<IActionResult> GetUsuarios()
        {
            var usuarios = await _context.Usuarios
                .Select(u => new { u.IdUser, u.NombreUsuario, u.IdRol })
                .ToListAsync();
            return Ok(usuarios);
        }
    }
}
