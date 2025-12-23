using ConfiguracioParametros.Data;
using ConfiguracioParametros.Logic.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ConfiguracioParametros.Controllers
{
    [ApiController]
    [Route("parametros")]
    public class ParametrosController(AppDbContext context, IJwtService jwtService, IAuditService auditService) : Controller
    {
        private readonly AppDbContext _context = context;
        private readonly IJwtService _jwtService = jwtService;
        private readonly IAuditService _auditService = auditService;

        [Authorize(Policy = "PERM_lectura")]
        [HttpGet]
        public async Task<IActionResult> GetParametros()
        {
            var userIdValue = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userIdValue))
                return Unauthorized("ID de usuario no encontrado");

            if (!int.TryParse(userIdValue, out int userId))
                return Unauthorized("ID de usuario inválido");

            var parametros = await _context.SEGMParametros
                .Include(p => p.Usuario)
                .Where(p => p.Usuario.IdUsuario == userId)
                .ToListAsync();

            return Ok(parametros);
        }
    }
}
