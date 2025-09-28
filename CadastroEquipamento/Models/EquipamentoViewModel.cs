using System.ComponentModel.DataAnnotations;

namespace CadastroEquipamento.Web.Models
{
    public class EquipamentoViewModel
    {
        public int CodEquipamento { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public string NumeroSerie { get; set; }

        [Required]
        public string Tipo { get; set; }

        [Required]
        public DateTime DataAquisicao { get; set; }

        public bool Status { get; set; }
    }
}
