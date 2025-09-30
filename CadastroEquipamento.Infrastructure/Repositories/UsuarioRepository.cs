using CadastroEquipamento.Domain.Entities;
using CadastroEquipamento.Infrastructure.DataAccess;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadastroEquipamento.Infrastructure.Repositories
{
    public class UsuarioRepository
    {
        private readonly DbConnectionFactory _connectionFactory;

        public UsuarioRepository(DbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public List<Usuario> ObterTodos()
        {
            using var connection = _connectionFactory.CreateConnection();
            return connection.Query<Usuario>("usp_ObterUsuarios", commandType: CommandType.StoredProcedure).ToList();
        }
        public List<Usuario> ObterUsuariosSemVinculos()
        {
            using var connection = _connectionFactory.CreateConnection();
            return connection.Query<Usuario>("usp_ObterUsuariosSemVinculos", commandType: CommandType.StoredProcedure).ToList();
        }

        public Usuario ObterPorId(int codUsuario)
        {
            using var connection = _connectionFactory.CreateConnection();
            return connection.QueryFirstOrDefault<Usuario>("usp_ObterUsuarioPorId",
                new { CodUsuario = codUsuario },
                commandType: CommandType.StoredProcedure);
        }

        public Usuario ObterPorEmail(string email)
        {
            using var connection = _connectionFactory.CreateConnection();
            return connection.QueryFirstOrDefault<Usuario>("usp_ObterPorEmail",
                new { Email = email },
                commandType: CommandType.StoredProcedure);
        }

        public void Adicionar(Usuario usuario)
        {
            using var connection = _connectionFactory.CreateConnection();
            connection.Execute("usp_InserirUsuario",
                new
                {
                    usuario.Nome,
                    usuario.Email,
                    usuario.Departamento
                },
                commandType: CommandType.StoredProcedure);
        }

        public void Atualizar(Usuario usuario)
        {
            using var connection = _connectionFactory.CreateConnection();
            connection.Execute("usp_AtualizarUsuario",
                new
                {
                    usuario.CodUsuario,
                    usuario.Nome,
                    usuario.Email,
                    usuario.Departamento
                },
                commandType: CommandType.StoredProcedure);
        }

        public void Remover(int codUsuario)
        {
            using var connection = _connectionFactory.CreateConnection();
            connection.Execute("usp_ExcluirUsuario",
                new { CodUsuario = codUsuario },
                commandType: CommandType.StoredProcedure);
        }
    }
}
