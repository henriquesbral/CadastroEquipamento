namespace CadastroEquipamento.Web.Models
{
    public class ApiUsuariosViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;
        public string Empresa { get; set; } = string.Empty;
        public string Cidade { get; set; } = string.Empty;
    }
}
