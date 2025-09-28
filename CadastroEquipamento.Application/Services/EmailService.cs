using CadastroEquipamento.Application.Interfaces;
using CadastroEquipamento.Infrastructure.Repositories;
using System;
using System.Collections.Generic;

namespace CadastroEquipamento.Application.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailRepository _repo;

        public EmailService(EmailRepository repo)
        {
            _repo = repo;
        }

        public void EnviarEmail(string destinatario, string assunto, string mensagem)
            => _repo.EnviarEmail(destinatario, assunto, mensagem);

        public void EnviarEmailVinculo(string destinatario, string nomeUsuario, string nomeEquipamento, DateTime dataVinculo)
            => _repo.EnviarEmailVinculo(destinatario, nomeUsuario, nomeEquipamento, dataVinculo);

        public IEnumerable<string> ObterLogs()
            => _repo.ObterLogs();
    }
}
