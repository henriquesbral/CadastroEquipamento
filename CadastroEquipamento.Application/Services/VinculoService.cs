using CadastroEquipamento.Application.Interfaces;
using CadastroEquipamento.Domain.Entities;
using CadastroEquipamento.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CadastroEquipamento.Application.Interfaces;
using CadastroEquipamento.Domain.Entities;
using CadastroEquipamento.Infrastructure.Repositories;

namespace CadastroEquipamento.Application.Services
{
    public class VinculoService : IVinculoService
    {
        private readonly VinculoRepository _repo;

        public VinculoService(VinculoRepository repo)
        {
            _repo = repo;
        }
        public List<Vinculo> ObterTodos() => _repo.ObterTodos();

        public void Vincular(int codEquipamento, int codUsuario) => _repo.Vincular(codEquipamento, codUsuario);

        public void Desvincular(int codEquipamento) => _repo.Desvincular(codEquipamento);
    }
}
