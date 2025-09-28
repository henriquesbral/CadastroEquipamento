using CadastroEquipamento.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadastroEquipamento.Application.Services
{
    public class EmailService : IEmailService
    {
        public void EnviarEmail(string destinatario, string assunto, string mensagem)
        {
            Console.WriteLine($"[E-mail] Para: {destinatario}, Assunto: {assunto}, Mensagem: {mensagem}");
        }
    }
}
