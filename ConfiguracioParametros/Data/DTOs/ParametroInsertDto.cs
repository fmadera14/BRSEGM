namespace ConfiguracioParametros.Data.DTOs
{
    public class ParametroInsertDto
    {
        public string Valor { get; set; }
        public string NombreClave { get; set; }
        public string Descripcion { get; set; }
        public int IdTipoParametro { get; set; }
    }
}
