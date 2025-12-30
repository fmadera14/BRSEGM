using Consultas.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Consultas.Controllers
{
    [ApiController]
    [Route("consultas")]
    public class ConsultaController(AppDbContext context) : Controller
    {
        private readonly AppDbContext _context = context;

        [HttpGet]
        [Authorize(Policy = "PERM_lectura")]
        public async Task<IActionResult> GetGarantias()
        {
            var garantias = await _context.GarantiasMobiliarias
                .Include(g => g.Acreedor)
                .Include(g => g.Deudor)
                .Include(g => g.Bien)
                .Include(g => g.Estado)
                .Include(g => g.Operacion)
                .AsNoTracking()
                .ToListAsync();

            return Ok(garantias);
        }
    }
}
