using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadastroEquipamento.Domain.Entities
{
    public class Usuario
    {
        public int CodUsuario { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Departamento { get; set; }
    }
}
