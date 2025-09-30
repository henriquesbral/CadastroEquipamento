using CadastroEquipamento.Domain.Entities;
using CadastroEquipamento.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadastroEquipamento.Application.Interfaces
{
    public interface IUsuarioService
    {
        List<Usuario> ObterTodos();
        List<Usuario> ObterUsuarioSemVinculo();
        Usuario ObterPorId(int id);
        Usuario ObterPorEmail(string email);
        void Adicionar(Usuario usuario);
        void Atualizar(Usuario usuario);
        void Remover(int id);
    }
}
