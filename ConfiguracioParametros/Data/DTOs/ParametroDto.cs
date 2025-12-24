namespace ConfiguracioParametros.Data.DTOs
{
    public class ParametroDto
    {
        public int IdParametro { get; set; }
        public string NombreClave { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public string Valor { get; set; } = string.Empty;

        public int IdEstado { get; set; }
        public string Estado { get; set; } = string.Empty;
        
        public int IdTipoParametro { get; set; }
        public string TipoParametro { get; set; } = string.Empty;
    }
}
