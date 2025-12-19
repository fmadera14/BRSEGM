using ManejoUsuariosRoles.Data;
using ManejoUsuariosRoles.Data.DTOs;
using ManejoUsuariosRoles.Logic.Interface;
using ManejoUsuariosRoles.Logic.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ManejoUsuariosRoles.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthController(AppDbContext context, IJwtService jwt, IAuditService auditService) : Controller
    {
        private readonly AppDbContext _context = context;
        private readonly IJwtService _jwt = jwt;
        private readonly IAuditService _auditService = auditService;

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
                IdEstado = 1
            };
            _context.Usuarios.Add(newUser);
            await _context.SaveChangesAsync();

            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            await _auditService.RegistrarAsync(
                tabla: "usuarios",
                idRegistro: newUser.IdUsuario,
                tipoOperacion: "INSERT",
                idUsuario: userId
            );

            return Ok(new { message = "Usuario registrado exitosamente" });
        }

        [Authorize]
        [HttpGet("me")]
        public async Task<IActionResult> DetailMe()
        {
            var userIdValue = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userIdValue))
                return Unauthorized("ID de usuario no encontrado");

            if (!int.TryParse(userIdValue, out int userId))
                return Unauthorized("ID de usuario inválido");

            var user = await _context.Usuarios
                .Include(u => u.Estado)
                .Include(u => u.Rol)
                .Where(u => u.IdUsuario == userId)
                .Select(u => new UserDetailDto
                {
                    Id = u.IdUsuario,
                    NombreUsuario = u.NombreUsuario,
                    Estado = u.Estado.Descripcion,
                    Rol = new RolDto
                    {
                        Id = u.Rol.IdRol,
                        Descripcion = u.Rol.Descripcion,
                        PermisoLectura = u.Rol.PermisoLectura,
                        PermisoEscritura = u.Rol.PermisoEscritura,
                        PermisoValidacion = u.Rol.PermisoValidacion,
                        PermisoModificacion = u.Rol.PermisoModificacion,
                        PermisoProcesar = u.Rol.PermisoProcesar
                    }
                })
                .FirstOrDefaultAsync();

            if (user == null)
                return NotFound("Usuario no encontrado");

            return Ok(user);
        }

        [Authorize(Policy = "PERM_modificacion")]
        [HttpPatch("change-password")]
        public async Task<IActionResult> ChangePassword(ChangePasswordDto dto)
        {
            // Obtener usuario autenticado
            var userIdValue = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userIdValue))
                return Unauthorized("Usuario no autenticado");

            int userId = int.Parse(userIdValue);

            var user = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.IdUsuario == userId);

            if (user == null)
                return Unauthorized("Usuario no encontrado");

            // Validar contraseña actual
            if (!BCrypt.Net.BCrypt.Verify(dto.CurrentPassword, user.Contraseña))
                return BadRequest("La contraseña actual es incorrecta");

            // Evitar reutilizar contraseña
            if (BCrypt.Net.BCrypt.Verify(dto.NewPassword, user.Contraseña))
                return BadRequest("La nueva contraseña no puede ser igual a la actual");

            // Hash nueva contraseña
            user.Contraseña = BCrypt.Net.BCrypt.HashPassword(dto.NewPassword);

            await _context.SaveChangesAsync();

            // Auditoría
            await _auditService.RegistrarAsync(
                tabla: "usuarios",
                idRegistro: user.IdUsuario,
                tipoOperacion: "CHANGE_PASSWORD",
                idUsuario: userId
            );

            return Ok(new
            {
                message = "Contraseña actualizada correctamente"
            });
        }

        [Authorize(Policy = "PERM_escritura")]
        [Authorize(Policy = "PERM_modificacion")]
        [Authorize(Policy = "PERM_validacion")]
        [Authorize(Policy = "PERM_procesar")]
        [Authorize(Policy = "PERM_lectura")]
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto dto)
        {
            // Usuario que ejecuta la acción
            var adminIdValue = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(adminIdValue))
                return Unauthorized();

            int adminId = int.Parse(adminIdValue);

            // Usuario objetivo
            var user = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.IdUsuario == dto.IdUsuario);

            if (user == null)
                return NotFound("Usuario no encontrado");

            // Resetear contraseña
            user.Contraseña = BCrypt.Net.BCrypt.HashPassword(dto.NewPassword);

            await _context.SaveChangesAsync();

            // Auditoría
            await _auditService.RegistrarAsync(
                tabla: "usuarios",
                idRegistro: user.IdUsuario,
                tipoOperacion: "RESET_PASSWORD",
                idUsuario: adminId
            );

            return Ok(new
            {
                message = "Contraseña reseteada correctamente"
            });
        }

    }
}
