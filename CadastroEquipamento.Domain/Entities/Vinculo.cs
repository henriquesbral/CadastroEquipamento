using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadastroEquipamento.Domain.Entities
{
    public class Vinculo
    {
        public int CodVinculo { get; set; }
        public int CodUsuario { get; set; }
        public string NomeUsuario { get; set; }
        public int CodEquipamento { get; set; }
        public string NomeEquipamento { get; set; }
        public DateTime DataVinculo { get; set; }
    }
}
