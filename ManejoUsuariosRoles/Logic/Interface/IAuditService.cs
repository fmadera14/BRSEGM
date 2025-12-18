namespace ManejoUsuariosRoles.Logic.Interface
{
    public interface IAuditService
    {
        Task RegistrarAsync(
            string tabla,
            int idRegistro,
            string tipoOperacion,
            int idUsuario
        );
    }
}
