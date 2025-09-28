using CadastroEquipamento.Domain.Entities;
using CadastroEquipamento.Infrastructure.Config;
using CadastroEquipamento.Infrastructure.DataAccess;
using Dapper;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;

namespace CadastroEquipamento.Infrastructure.Repositories
{
    public class EmailRepository
    {
        private readonly string _caminhoLog;
        
        private readonly DbConnectionFactory _connectionFactory;

        public EmailRepository(IOptions<EmailLogSettings> settings, DbConnectionFactory connectionFactory)
        {
            _caminhoLog = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, settings.Value.FilePath);
            _connectionFactory = connectionFactory;

            var pasta = Path.GetDirectoryName(_caminhoLog);
            if (!Directory.Exists(pasta))
                Directory.CreateDirectory(pasta);
        }

        public void Adicionar(int codUsuario, int codEquipamento, int tipo)
        {
            using var connection = _connectionFactory.CreateConnection();
            connection.Execute("usp_InserirLogEmailVinculo",
                new
                {
                    codUsuario,
                    codEquipamento,
                    tipo
                },
                commandType: CommandType.StoredProcedure);
        }

        public List<LogEmailVinculo> ObterTodos()
        {
            using var connection = _connectionFactory.CreateConnection();
            return connection.Query<LogEmailVinculo>("usp_EmailsVinculados", commandType: CommandType.StoredProcedure).ToList();
        }

        public void EnviarEmail(string destinatario, string assunto, string mensagem)
        {
            string log = $@"
                    [{DateTime.Now:yyyy-MM-dd HH:mm:ss}]
                    Para: {destinatario}
                    Assunto: {assunto}
                    Mensagem:
                    {mensagem}
                    ------------------------------------------------------------";

            Console.WriteLine(log);
            File.AppendAllText(_caminhoLog, log + Environment.NewLine);
        }

        public void EnviarEmailVinculo(string destinatario, string nomeUsuario, string nomeEquipamento, DateTime dataVinculo, int tipo)
        {
            string assunto;
            string mensagem;
            if (tipo == 1)
            {
                assunto = $"[Bradesco] Vínculo de Equipamento: {nomeEquipamento}";
                mensagem = GerarMensagemVinculo(nomeUsuario, nomeEquipamento, dataVinculo);

                EnviarEmail(destinatario, assunto, mensagem);
            }
            else
            {
                assunto = $"[Bradesco] Desvinculo de Equipamento: {nomeEquipamento}";
                mensagem = GerarMensagemDesvinculo(nomeUsuario, nomeEquipamento, dataVinculo);
            }

            EnviarEmail(destinatario, assunto, mensagem);
        }

        public IEnumerable<string> ObterLogs()
        {
            if (!File.Exists(_caminhoLog))
                return Enumerable.Empty<string>();

            return File.ReadAllLines(_caminhoLog);
        }

        private string GerarMensagemVinculo(string nomeUsuario, string nomeEquipamento, DateTime dataVinculo)
        {
            return $@"
                
                Olá {nomeUsuario},
                Informamos que no dia {dataVinculo:dd/MM/yyyy HH:mm} foi realizado o vínculo do equipamento:

                📌 Equipamento: {nomeEquipamento}

                Este equipamento agora está sob sua responsabilidade no sistema de gestão Bradesco.

                Caso não reconheça este vínculo, entre em contato imediatamente com o suporte de TI.

                Atenciosamente,  
                Equipe Bradesco
             ";
        }
        private string GerarMensagemDesvinculo(string nomeUsuario, string nomeEquipamento, DateTime dataVinculo)
        {
            return $@"
                
                Olá {nomeUsuario},
                Informamos que no dia {dataVinculo:dd/MM/yyyy HH:mm} foi realizado o desvinculo do equipamento:

                📌 Equipamento: {nomeEquipamento}

                Este equipamento voltará ao sistema de gestão do Bradesco.

                Caso não reconheça este vínculo, entre em contato imediatamente com o suporte de TI.

                Atenciosamente,  
                Equipe Bradesco
             ";
        }
    }
}
