using CadastroEquipamento.Application.Interfaces;
using CadastroEquipamento.Domain.Entities;
using CadastroEquipamento.Infrastructure.Repositories;
using System.Collections.Generic;

namespace CadastroEquipamento.Application.Services
{
    public class EquipamentoService : IEquipamentoService
    {
        private readonly EquipamentoRepository _repo;

        public EquipamentoService(EquipamentoRepository repo)
        {
            _repo = repo;
        }

        public List<Equipamento> ObterTodos() => _repo.ObterTodos();

        public List<Equipamento> ObterEquipamentoSemVinculo() => _repo.ObterEquipamentoSemVinculo();

        public Equipamento ObterPorId(int id) => _repo.ObterPorId(id);

        public void Adicionar(Equipamento equipamento) => _repo.Adicionar(equipamento);

        public void Atualizar(Equipamento equipamento) => _repo.Atualizar(equipamento);

        public void Remover(int id) => _repo.Remover(id);
    }
}
