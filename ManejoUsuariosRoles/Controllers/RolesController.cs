using ManejoUsuariosRoles.Data;
using ManejoUsuariosRoles.Data.DTOs;
using ManejoUsuariosRoles.Logic.Interface;
using ManejoUsuariosRoles.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ManejoUsuariosRoles.Controllers
{
    [ApiController]
    [Route("roles")]
    [Authorize]
    public class RolesController(AppDbContext context, IAuditService _auditService) : Controller
    {
        private readonly AppDbContext _context = context;

        [HttpGet]
        [Authorize(Policy = "PERM_lectura")]
        public async Task<IActionResult> GetRoles()
        {
            var roles = await _context.Roles
            .Include(r => r.Estado)
            .Select(r => new RolListDto
            {
                IdRol = r.IdRol,
                Descripcion = r.Descripcion,

                PermisoLectura = r.PermisoLectura,
                PermisoEscritura = r.PermisoEscritura,
                PermisoValidacion = r.PermisoValidacion,
                PermisoModificacion = r.PermisoModificacion,
                PermisoProcesar = r.PermisoProcesar,

                IdEstado = r.IdEstado,
                Estado = r.Estado.Descripcion
            })
            .ToListAsync();

            return Ok(roles);
        }

        [HttpPost]
        [Authorize(Policy = "PERM_escritura")]
        public async Task<IActionResult> Create(CreateRolDto dto)
        {
            var rol = new Rol
            {
                Descripcion = dto.Descripcion,
                PermisoLectura = dto.PermisoLectura,
                PermisoEscritura = dto.PermisoEscritura,
                PermisoValidacion = dto.PermisoValidacion,
                PermisoModificacion = dto.PermisoModificacion,
                PermisoProcesar = dto.PermisoProcesar,
                IdEstado = 1
            };

            try
            {
                _context.Roles.Add(rol);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex) when (
                ex.InnerException is Npgsql.PostgresException pg &&
                pg.SqlState == "23505"
            )
            {
                return Conflict(new
                {
                    message = "Ya existe un rol con esa descripción"
                });
            }

            // Audit logging can be added here
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            await _auditService.RegistrarAsync(
                tabla: "usuarios",
                idRegistro: rol.IdRol,
                tipoOperacion: "INSERT",
                idUsuario: userId
            );

            RolCreateDetailDto rolDetail = new RolCreateDetailDto
            {
                IdRol = rol.IdRol,
                Descripcion = rol.Descripcion,
                PermisoLectura = rol.PermisoLectura,
                PermisoEscritura = rol.PermisoEscritura,
                PermisoValidacion = rol.PermisoValidacion,
                PermisoModificacion = rol.PermisoModificacion,
                PermisoProcesar = rol.PermisoProcesar,
                IdEstado = rol.IdEstado,
            };

            return Ok(rolDetail);
        }
    }
}
