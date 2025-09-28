namespace CadastroEquipamento.Web.Models
{
    public class LogEmailVinculoViewModel
    {
        public string NomeUsuario { get; set; }
        public string NomeEquipamento { get; set; }
        public int? TipoVinculo { get; set; }
        public DateTime DataEnvioEmail { get; set; }
    }
}
