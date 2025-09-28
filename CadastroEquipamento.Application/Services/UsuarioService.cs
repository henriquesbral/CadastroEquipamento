using CadastroEquipamento.Application.Interfaces;
using CadastroEquipamento.Domain.Entities;
using CadastroEquipamento.Infrastructure.Repositories;
using System.Collections.Generic;

namespace CadastroEquipamento.Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly UsuarioRepository _repo;

        public UsuarioService(UsuarioRepository repo)
        {
            _repo = repo;
        }
        public List<Usuario> ObterTodos() => _repo.ObterTodos();

        public List<Usuario> ObterUsuarioSemVinculo() => _repo.ObterUsuariosSemVinculos();

        public Usuario ObterPorId(int id) => _repo.ObterPorId(id);

        public void Adicionar(Usuario Usuario) => _repo.Adicionar(Usuario);

        public void Atualizar(Usuario Usuario) => _repo.Atualizar(Usuario);

        public void Remover(int id) => _repo.Remover(id);
    }
}
