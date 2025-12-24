using ConfiguracioParametros.Models;
using Microsoft.EntityFrameworkCore;

namespace ConfiguracioParametros.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<Estado> Estados { get; set; }
        public DbSet<Auditoria> Auditorias { get; set; }
        public DbSet<RegistroAuditado> RegistrosAuditados { get; set; }
        public DbSet<SEGMParametro> SEGMParametros { get; set; }
        public DbSet<TipoParametro> TiposParametros { get; set; }
    }
}
