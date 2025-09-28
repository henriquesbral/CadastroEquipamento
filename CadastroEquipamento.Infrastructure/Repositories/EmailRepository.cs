using CadastroEquipamento.Infrastructure.Config;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CadastroEquipamento.Infrastructure.Repositories
{
    public class EmailRepository
    {
        private readonly string _caminhoLog;

        public EmailRepository(IOptions<EmailLogSettings> settings)
        {
            _caminhoLog = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, settings.Value.FilePath);

            var pasta = Path.GetDirectoryName(_caminhoLog);
            if (!Directory.Exists(pasta))
                Directory.CreateDirectory(pasta);
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

        public void EnviarEmailVinculo(string destinatario, string nomeUsuario, string nomeEquipamento, DateTime dataVinculo)
        {
            string assunto = $"[Bradesco] Vínculo de Equipamento: {nomeEquipamento}";
            string mensagem = GerarMensagemVinculo(nomeUsuario, nomeEquipamento, dataVinculo);

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
            return $@"Olá {nomeUsuario},
                Informamos que no dia {dataVinculo:dd/MM/yyyy HH:mm} foi realizado o vínculo do equipamento:

                📌 Equipamento: {nomeEquipamento}

                Este equipamento agora está sob sua responsabilidade no sistema de gestão Bradesco.

                Caso não reconheça este vínculo, entre em contato imediatamente com o suporte de TI.

                Atenciosamente,  
                Equipe Bradesco";
        }
    }
}
