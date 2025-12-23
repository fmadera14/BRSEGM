using ConfiguracioParametros.Data;
using ConfiguracioParametros.Logic.Interface;
using ConfiguracioParametros.Models;

namespace ConfiguracioParametros.Logic.Services
{
    public class AuditService(AppDbContext context) : IAuditService
    {
        private readonly AppDbContext _context = context;

        public async Task RegistrarAsync(
        string tabla,
        int idRegistro,
        string tipoOperacion,
        int idUsuario)
        {
            var registro = new RegistroAuditado
            {
                TablaAfectada = tabla,
                IdRegistro = idRegistro
            };

            _context.RegistrosAuditados.Add(registro);
            await _context.SaveChangesAsync();

            var auditoria = new Auditoria
            {
                IdRegistroAuditado = registro.IdRegistroAuditado,
                TipoOperacion = tipoOperacion,
                IdUsuario = idUsuario,
                FechaOperacion = DateTime.UtcNow
            };

            _context.Auditorias.Add(auditoria);
            await _context.SaveChangesAsync();
        }
    }
}
