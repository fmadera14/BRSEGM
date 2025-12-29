using Consultas.Models;
using Microsoft.EntityFrameworkCore;

namespace Consultas.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<Estado> Estados { get; set; }
        public DbSet<TipoConciliacion> TiposConciliacion { get; set; }
        public DbSet<EstadoMinisterio> EstadosMinisterio { get; set; }
        public DbSet<GarantiaMobiliaria> GarantiasMobiliarias { get; set; }
        public DbSet<Inscripcion> Inscripciones { get; set; }
        public DbSet<Moneda> Monedas { get; set; }
        public DbSet<Operacion> Operaciones { get; set; }
        public DbSet<TipoAvisoInscripcion> TiposAvisoInscripcion { get; set; }
        public DbSet<TipoEmbargo> TiposEmbargos { get; set; }
        public DbSet<TipoGarantia> TiposGarantias { get; set; }
    }
}
