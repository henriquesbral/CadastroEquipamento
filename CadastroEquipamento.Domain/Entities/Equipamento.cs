using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadastroEquipamento.Domain.Entities
{
    public class Equipamento
    {
        public int CodEquipamento { get; set; }
        public string Nome { get; set; }
        public string NumeroSerie { get; set; }
        public string Tipo { get; set; }
        public DateTime DataAquisicao { get; set; }
        public bool Status { get; set; }
    }
}
