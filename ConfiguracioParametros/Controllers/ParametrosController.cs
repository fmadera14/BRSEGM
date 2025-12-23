using ConfiguracioParametros.Data;
using ConfiguracioParametros.Data.DTOs;
using ConfiguracioParametros.Logic.Interface;
using ConfiguracioParametros.Models;
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

        [Authorize(Policy = "PERM_escritura")]
        [HttpPost]
        public async Task<IActionResult> CreateParametro([FromBody] ParametroInsertDto dto)
        {
            var userIdValue = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userIdValue))
                return Unauthorized("ID de usuario no encontrado");
            if (!int.TryParse(userIdValue, out int userId))
                return Unauthorized("ID de usuario inválido");

            SEGMParametro parametro = new()
            {
                IdUsuario = userId,
                Valor = dto.Valor,
                NombreClave = dto.NombreClave,
                Descripcion = dto.Descripcion,
                IdEstado = 1
            };
            _context.SEGMParametros.Add(parametro);
            await _context.SaveChangesAsync();

            await _auditService.RegistrarAsync("SEGMParametros", parametro.IdParametro, "INSERT", userId);
            return CreatedAtAction(nameof(GetParametros), new { id = parametro.IdParametro }, parametro);
        }
    }
}
