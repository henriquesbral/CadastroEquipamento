using CadastroEquipamento.Application.Interfaces;
using CadastroEquipamento.Domain.Entities;
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

        public void EnviarEmailVinculo(string destinatario, string nomeUsuario, string nomeEquipamento, DateTime dataVinculo, int tipo)
            => _repo.EnviarEmailVinculo(destinatario, nomeUsuario, nomeEquipamento, dataVinculo, tipo);

        public IEnumerable<string> ObterLogs()
            => _repo.ObterLogs();

        public List<LogEmailVinculo> ObterTodos()
            => _repo.ObterTodos();

        public void Adicionar(int codUsuario, int codEquipamento, int tipo) 
            => _repo.Adicionar(codUsuario, codEquipamento, tipo);
    }
}
