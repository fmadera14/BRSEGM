using ManejoUsuariosRoles.Data;
using ManejoUsuariosRoles.Data.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ManejoUsuariosRoles.Controllers
{
    [ApiController]
    [Route("users")]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsersController(AppDbContext context)
        {
            _context = context;
        }

        [Authorize(Policy = "PERM_lectura")]
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _context.Usuarios.Include(u => u.Rol).Select(u => new {
                u.IdUsuario,
                u.NombreUsuario,
                Rol = u.Rol.Descripcion,
                u.IdEstado
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

            return Ok();
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
