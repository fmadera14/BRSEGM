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
    }
}
