using CadastroEquipamento.Domain.Entities;
using CadastroEquipamento.Infrastructure.DataAccess;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace CadastroEquipamento.Infrastructure.Repositories
{
    public class EquipamentoRepository
    {
        private readonly DbConnectionFactory _connectionFactory;

        public EquipamentoRepository(DbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public List<Equipamento> ObterTodos()
        {
            using var connection = _connectionFactory.CreateConnection();
            return connection.Query<Equipamento>("usp_ObterEquipamentos", commandType: CommandType.StoredProcedure).ToList();
        }

        public List<Equipamento> ObterEquipamentoSemVinculo()
        {
            using var connection = _connectionFactory.CreateConnection();
            return connection.Query<Equipamento>("usp_ObterEquipamentosSemVinculos", commandType: CommandType.StoredProcedure).ToList();
        }

        public Equipamento ObterPorId(int codEquipamento)
        {
            using var connection = _connectionFactory.CreateConnection();
            return connection.QueryFirstOrDefault<Equipamento>("usp_ObterEquipamentoPorId",
                new { CodEquipamento = codEquipamento },
                commandType: CommandType.StoredProcedure);
        }

        public void Adicionar(Equipamento equipamento)
        {
            using var connection = _connectionFactory.CreateConnection();
            connection.Execute("usp_InserirEquipamento",
                new
                {
                    equipamento.Nome,
                    equipamento.NumeroSerie,
                    equipamento.Tipo,
                    equipamento.DataAquisicao,
                    equipamento.Status
                },
                commandType: CommandType.StoredProcedure);
        }

        public void Atualizar(Equipamento equipamento)
        {
            using var connection = _connectionFactory.CreateConnection();
            connection.Execute("usp_AtualizarEquipamento",
                new
                {
                    equipamento.CodEquipamento,
                    equipamento.Nome,
                    equipamento.NumeroSerie,
                    equipamento.Tipo,
                    equipamento.DataAquisicao,
                    equipamento.Status
                },
                commandType: CommandType.StoredProcedure);
        }

        public void Remover(int codEquipamento)
        {
            using var connection = _connectionFactory.CreateConnection();
            connection.Execute("usp_ExcluirEquipamento",
                new { CodEquipamento = codEquipamento },
                commandType: CommandType.StoredProcedure);
        }
    }
}
