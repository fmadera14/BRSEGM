using ConfiguracioParametros.Data;
using ConfiguracioParametros.Data.DTOs;
using ConfiguracioParametros.Logic.Interface;
using ConfiguracioParametros.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using System.Security.Claims;

namespace ConfiguracioParametros.Controllers
{
    [ApiController]
    [Route("tipo-parametro")]
    public class TipoParametroController(AppDbContext context, IAuditService auditService, IJwtService jwtService) :  Controller
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

            var tiposParametro = await _context.TiposParametros
                .ToListAsync();

            return Ok(tiposParametro);
        }

        [Authorize(Policy = "PERM_escritura")]
        [HttpPost]
        public async Task<IActionResult> CreateTipoParametro([FromBody] TipoParametroCreateDto dto)
        {
            var userIdValue = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userIdValue))
                return Unauthorized("ID de usuario no encontrado");
            if (!int.TryParse(userIdValue, out int userId))
                return Unauthorized("ID de usuario inválido");

            TipoParametro tipoParametro = new()
            {
                Descripcion = dto.Descripcion
            };

            _context.TiposParametros.Add(tipoParametro);

            await _context.SaveChangesAsync();

            await _auditService.RegistrarAsync("TiposParametro", tipoParametro.IdTipoParametro, "INSERT", userId);

            return CreatedAtAction(nameof(GetParametros), new { id = tipoParametro.IdTipoParametro }, tipoParametro);
        }
    }
}
