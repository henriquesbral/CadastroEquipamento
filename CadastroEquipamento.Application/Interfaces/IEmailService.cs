using CadastroEquipamento.Domain.Entities;
using System;
using System.Collections.Generic;

namespace CadastroEquipamento.Application.Interfaces
{
    public interface IEmailService
    {
        List<LogEmailVinculo> ObterTodos();

        void Adicionar(int codUsuario, int codEquipamento, int tipo);

        void EnviarEmail(string destinatario, string assunto, string mensagem);

        void EnviarEmailVinculo(string destinatario, string nomeUsuario, string nomeEquipamento, DateTime dataVinculo, int tipo);

        IEnumerable<string> ObterLogs();
    }
}