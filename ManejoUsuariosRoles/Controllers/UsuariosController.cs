using ManejoUsuariosRoles.Data;
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
    }

}
