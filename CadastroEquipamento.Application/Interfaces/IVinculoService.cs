using CadastroEquipamento.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadastroEquipamento.Application.Interfaces
{
    public interface IVinculoService
    {
        List<Vinculo> ObterTodos();
        void Vincular(int codEquipamento, int codUsuario);
        void Desvincular(int codEquipamento);
    }
}
