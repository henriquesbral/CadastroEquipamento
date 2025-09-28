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
    public class VinculoRepository
    {
        private readonly DbConnectionFactory _connectionFactory;

        public VinculoRepository(DbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public List<Vinculo> ObterTodos()
        {
            using var connection = _connectionFactory.CreateConnection();
            return connection.Query<Vinculo>("usp_ObterVinculos", commandType: CommandType.StoredProcedure).ToList();
        }

        public void Vincular(int codEquipamento, int codUsuario)
        {
            using var connection = _connectionFactory.CreateConnection();
            connection.Execute("usp_VincularEquipamento",
                new
                {
                    CodEquipamento = codEquipamento,
                    CodUsuario = codUsuario
                },
                commandType: CommandType.StoredProcedure);
        }

        public void Desvincular(int vinculo)
        {
            using var connection = _connectionFactory.CreateConnection();
            connection.Execute("usp_DesvincularEquipamento",
                new
                {
                    CodVinculo = vinculo
                },
                commandType: CommandType.StoredProcedure);
        }
    }
}
