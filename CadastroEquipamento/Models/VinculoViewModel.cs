using System.ComponentModel.DataAnnotations;

namespace CadastroEquipamento.Web.Models
{
    public class VinculoViewModel
    {
        public int CodVinculo { get; set; }

        [Required]
        public int EquipamentoId { get; set; }

        [Required]
        public int UsuarioId { get; set; }

        // Campos para exibição na tabela
        public string NomeEquipamento { get; set; }
        public string NomeUsuario { get; set; }
        public DateTime? DataVinculo { get; set; }

        // Lista de usuários disponíveis para vincular
        public IEnumerable<UsuarioDropdownViewModel> UsuariosDisponiveis { get; set; } = new List<UsuarioDropdownViewModel>();
        public IEnumerable<EquipamentoDropdownViewModel> EquipamentoDisponiveis { get; set; } = new List<EquipamentoDropdownViewModel>();
    }
    public class UsuarioDropdownViewModel
    {
        public int CodUsuario { get; set; }
        public string Nome { get; set; }
    }
    public class EquipamentoDropdownViewModel
    {
        public int CodEquipamento { get; set; }
        public string Nome { get; set; }
    }
    public class DesvincularRequest
    {
        public int EquipamentoId { get; set; }
        public int UsuarioId { get; set; }
        public int VinculoId { get; set; }
    }
}
