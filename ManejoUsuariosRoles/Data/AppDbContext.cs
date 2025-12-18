using ManejoUsuariosRoles.Models;
using Microsoft.EntityFrameworkCore;

namespace ManejoUsuariosRoles.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<Estado> Estados { get; set; }
    }
}
