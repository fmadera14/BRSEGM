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
                .Where(p => p.IdUsuario == userId)
                .Where(p => p.IdEstado != 9)
                .Include(p => p.Estado)
                .Include(p => p.TipoParametro)
                .Select(p => new ParametroDto
                {
                    IdParametro = p.IdParametro,
                    NombreClave = p.NombreClave,
                    Descripcion = p.Descripcion,
                    Valor = p.Valor,
                    IdEstado = p.IdEstado,
                    Estado = p.Estado.Descripcion,
                    IdTipoParametro = p.IdTipoParametro,
                    TipoParametro = p.TipoParametro.Descripcion
                })
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

            bool existeParametro = await _context.SEGMParametros
                .AnyAsync(p =>
                    p.IdUsuario == userId &&
                    p.NombreClave == dto.NombreClave &&
                    p.IdEstado != 9
                );

            if (existeParametro)
                return Conflict($"Ya existe un parámetro con la clave '{dto.NombreClave}' para este usuario.");

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

        [Authorize(Policy = "PERM_modificacion")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateParametro(int id, [FromBody] ParametroUpdateDto dto)
        {
            var userIdValue = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userIdValue))
                return Unauthorized("ID de usuario no encontrado");
            if (!int.TryParse(userIdValue, out int userId))
                return Unauthorized("ID de usuario inválido");

            bool existeParametro = await _context.SEGMParametros
                .AnyAsync(p =>
                    p.IdUsuario == userId &&
                    p.NombreClave == dto.NombreClave &&
                    p.IdEstado != 9 &&
                    p.IdParametro != id
                );

            if (existeParametro)
                return Conflict($"Ya existe un parámetro con la clave '{dto.NombreClave}' para este usuario.");

            var parametro = await _context.SEGMParametros.FindAsync(id);
            if (parametro == null)
                return NotFound("Parámetro no encontrado");

            parametro.Valor = dto.Valor;
            parametro.Descripcion = dto.Descripcion;
            parametro.IdEstado = dto.IdEstado;
            parametro.IdTipoParametro = dto.IdTipoParametro;

            _context.SEGMParametros.Update(parametro);
            await _context.SaveChangesAsync();
            await _auditService.RegistrarAsync("SEGMParametros", parametro.IdParametro, "UPDATE", userId);
            return Ok(new { message = "Parametro modificado exitosamente" });
        }

        [Authorize(Policy = "PERM_modificacion")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> SoftDeleteParametro(int id)
        {
            var userIdValue = User.FindFirstValue(ClaimTypes.NameIdentifier);
            
            if (string.IsNullOrEmpty(userIdValue))
                return Unauthorized("ID de usuario no encontrado");
            if (!int.TryParse(userIdValue, out int userId))
                return Unauthorized("ID de usuario inválido");
            var parametro = await _context.SEGMParametros.FindAsync(id);
            if (parametro == null)
                return NotFound("Parámetro no encontrado");

            parametro.IdEstado = 9;
            
            _context.SEGMParametros.Update(parametro);
            await _context.SaveChangesAsync();
            
            await _auditService.RegistrarAsync("SEGMParametros", parametro.IdParametro, "SOFT_DELETE", userId);

            return Ok(new {message = "Parametro eliminado exitosamente"});
        }
    }
}
