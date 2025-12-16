using ManejoUsuariosRoles.Data;
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
        public async Task<IActionResult> GetRoles()
        {
            return Ok(await _context.Roles.ToListAsync());
        }

        [HttpPost]
        [Authorize(Policy = "PERM_modificacion")]
        public async Task<IActionResult> Create(Rol rol)
        {
            _context.Roles.Add(rol);
            await _context.SaveChangesAsync();
            return Ok(rol);
        }
    }
}
