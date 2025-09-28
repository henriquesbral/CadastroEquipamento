using CadastroEquipamento.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadastroEquipamento.Application.Interfaces
{
    public interface IEquipamentoService
    {
        List<Equipamento> ObterTodos();
        List<Equipamento> ObterEquipamentoSemVinculo();
        Equipamento ObterPorId(int id);
        void Adicionar(Equipamento equipamento);
        void Atualizar(Equipamento equipamento);
        void Remover(int id);
    }
}

