using Consultas.Models;
using Microsoft.EntityFrameworkCore;

namespace Consultas.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<Estado> Estados { get; set; }
    }
}
