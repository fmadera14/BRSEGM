namespace ConfiguracioParametros.Data.DTOs
{
    public class ParametroDto
    {
        public int IdParametro { get; set; }
        public string NombreClave { get; set; }
        public string Descripcion { get; set; }
        public string Valor { get; set; }

        public int IdEstado { get; set; }
        public string Estado { get; set; }
    }
}
