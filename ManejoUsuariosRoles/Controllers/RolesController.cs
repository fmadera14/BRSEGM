using ManejoUsuariosRoles.Data;
using ManejoUsuariosRoles.Data.DTOs;
using ManejoUsuariosRoles.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ManejoUsuariosRoles.Controllers
{
    [ApiController]
    [Route("roles")]
    [Authorize]
    public class RolesController : Controller
    {
        private readonly AppDbContext _context;

        public RolesController(AppDbContext context)
        {
            _context = context;
        }

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
        public async Task<IActionResult> Create(Rol rol)
        {
            _context.Roles.Add(rol);
            await _context.SaveChangesAsync();
            return Ok(rol);
        }
    }
}
