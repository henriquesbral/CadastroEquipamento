using System.ComponentModel.DataAnnotations;

namespace CadastroEquipamento.Web.Models
{
    public class UsuarioViewModel
    {
        public int CodUsuario { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string Departamento { get; set; }
    }
}
