using Microsoft.EntityFrameworkCore;
using ManejoUsuariosRoles.Models;

namespace ManejoUsuariosRoles.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<Estado> Estados { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>()
                .HasOne(u => u.Rol)
                .WithMany(r => r.Usuarios)
                .HasForeignKey(u => u.IdRol);

            modelBuilder.Entity<Usuario>()
                .HasOne(u => u.Estado)
                .WithMany()
                .HasForeignKey(u => u.IdEstado);

            modelBuilder.Entity<Rol>()
                .HasOne(r => r.Estado)
                .WithMany()
                .HasForeignKey(r => r.IdEstado);
        }
    }
}
