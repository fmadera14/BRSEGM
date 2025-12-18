using ManejoUsuariosRoles.Data;
using ManejoUsuariosRoles.Data.DTOs;
using ManejoUsuariosRoles.Logic.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ManejoUsuariosRoles.Controllers
{
    [ApiController]
    [Route("users")]
    [Authorize]
    public class UsersController(AppDbContext context, IAuditService _auditService) : ControllerBase
    {
        private readonly AppDbContext _context = context;

        [Authorize(Policy = "PERM_lectura")]
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _context.Usuarios.Include(u => u.Rol).Include(u => u.Estado).Select(u => new {
                u.IdUsuario,
                u.NombreUsuario,
                Rol = u.Rol.Descripcion,
                Estado = u.Estado.Descripcion
            }).ToListAsync();

            return Ok(users);
        }

        [Authorize(Policy = "PERM_lectura")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserDetail(int id)
        {
            var user = await _context.Usuarios
                .Include(u => u.Estado)
                .Include(u => u.Rol)
                .Where(u => u.IdUsuario == id)
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
        [HttpPatch("{id}/disable")]
        public async Task<IActionResult> DisableUser(int id)
        {
            var user = await _context.Usuarios.FindAsync(id);
            if (user == null)
                return NotFound();

            user.IdEstado = 5; // INACTIVO
            await _context.SaveChangesAsync();

            // Audit logging can be added here
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            await _auditService.RegistrarAsync(
                tabla: "usuarios",
                idRegistro: user.IdUsuario,
                tipoOperacion: "DISABLE",
                idUsuario: userId
            );

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, UpdateUserDto dto)
        {
            // Obtener ID del usuario autenticado
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
                return Unauthorized();

            int currentUserId = int.Parse(userIdClaim);

            // ¿Es el mismo usuario?
            bool isSelf = currentUserId == id;

            // ¿Tiene permiso de modificación?
            bool hasModifyPermission =
                User.HasClaim("perm_modificacion", "True");

            // Regla de acceso
            if (!isSelf && !hasModifyPermission)
                return Forbid("No tienes permiso para modificar este usuario");

            // Buscar usuario a modificar
            var user = await _context.Usuarios.FindAsync(id);
            if (user == null)
                return NotFound("Usuario no encontrado");

            // Aplicar cambios permitidos
            if (!string.IsNullOrWhiteSpace(dto.NombreUsuario))
                user.NombreUsuario = dto.NombreUsuario;

            // Cambios sensibles: solo si tiene permiso
            if (dto.IdRol.HasValue)
            {
                if (!hasModifyPermission)
                    return Forbid("No puedes cambiar el rol de otro usuario");

                user.IdRol = dto.IdRol.Value;
            }

            if (dto.IdEstado.HasValue)
            {
                if (!hasModifyPermission)
                    return Forbid("No puedes cambiar el estado de otro usuario");

                user.IdEstado = dto.IdEstado.Value;
            }

            await _context.SaveChangesAsync();

            // Auditoría
            await _auditService.RegistrarAsync(
                tabla: "usuarios",
                idRegistro: user.IdUsuario,
                tipoOperacion: "UPDATE",
                idUsuario: currentUserId
            );

            return Ok(new { message = "Usuario actualizado correctamente" });
        }

        [Authorize]
        [HttpGet("ping")]
        public IActionResult Ping()
        {
            return Ok(new
            {
                user = User.Identity?.Name,
                claims = User.Claims.Select(c => new { c.Type, c.Value })
            });
        }
    }

}
