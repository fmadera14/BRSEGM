namespace ManejoUsuariosRoles.Models
{
    public class Estado
    {
        public int IdEstado { get; set; }
        public string Descripcion { get; set; }

        public ICollection<Usuario> Usuarios { get; set; }
        public ICollection<Rol> Roles { get; set; }
    }
}
