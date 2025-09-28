using System;
using System.Collections.Generic;

namespace CadastroEquipamento.Application.Interfaces
{
    public interface IEmailService
    {
        void EnviarEmail(string destinatario, string assunto, string mensagem);

        void EnviarEmailVinculo(string destinatario, string nomeUsuario, string nomeEquipamento, DateTime dataVinculo);

        IEnumerable<string> ObterLogs();
    }
}