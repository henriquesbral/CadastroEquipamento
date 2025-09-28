using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadastroEquipamento.Domain.Entities
{
    public class LogEmailVinculo
    {
        public int? CodLogEmailVinculo { get; set; }

        public string NomeUsuario { get; set; }

        public string NomeEquipamento { get; set; }

        public int? Tipo { get; set; }

        public DateTime DataEnvioEmail { get; set; }
    }
}
