using ManejoUsuariosRoles.Data;
using ManejoUsuariosRoles.Logic.Interface;
using ManejoUsuariosRoles.Models;

namespace ManejoUsuariosRoles.Logic.Services
{
    public class AuditService : IAuditService
    {
        private readonly AppDbContext _context;

        public AuditService(AppDbContext context)
        {
            _context = context;
        }

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
