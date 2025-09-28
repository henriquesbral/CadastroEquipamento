using CadastroEquipamento.Application.Interfaces;
using CadastroEquipamento.Infrastructure.Repositories;
using CadastroEquipamento.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadastroEquipamento.Application.Services
{
    public class ApiUsuariosService : IApiUsuariosService
    {
        private readonly ApiUsuariosRepository _repo;

        public ApiUsuariosService(ApiUsuariosRepository repo)
        {
            _repo = repo;
        }

        public Task<IEnumerable<ApiUser>> ListarUsuariosAsync() => _repo.ListarUsuariosAsync();
    }
}
