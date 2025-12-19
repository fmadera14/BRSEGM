using ManejoUsuariosRoles.Data;
using ManejoUsuariosRoles.Models;

namespace ManejoUsuariosRoles.Logic
{
    public static class DatabaseSeeder
    {
        public static async Task SeedAsync(AppDbContext context, IConfiguration config)
        {
            if (context.Usuarios.Any())
                return;

            var adminUsername = config["SeedAdmin:Username"];
            var adminPassword = config["SeedAdmin:Password"];

            if (string.IsNullOrWhiteSpace(adminUsername) || string.IsNullOrWhiteSpace(adminPassword))
            {
                throw new Exception("SeedAdmin credentials not configured");
            }

            var estadoActivo = new Estado
            {
                Descripcion = "ACTIVO"
            };

            var rolAdmin = new Rol
            {
                Descripcion = "ADMIN",
                PermisoLectura = true,
                PermisoEscritura = true,
                PermisoValidacion = true,
                PermisoModificacion = true,
                PermisoProcesar = true,
                Estado = estadoActivo
            };

            var admin = new Usuario
            {
                NombreUsuario = adminUsername,
                Contraseña = BCrypt.Net.BCrypt.HashPassword(adminPassword),
                Rol = rolAdmin,
                Estado = estadoActivo
            };

            context.Estados.Add(estadoActivo);
            context.Roles.Add(rolAdmin);
            context.Usuarios.Add(admin);

            await context.SaveChangesAsync();
        }
    }
}
