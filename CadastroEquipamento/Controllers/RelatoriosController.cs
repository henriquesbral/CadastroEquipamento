using CadastroEquipamento.Application.Interfaces;
using CadastroEquipamento.Application.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace CadastroEquipamento.Web.Controllers
{
    public class RelatoriosController : Controller
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IEquipamentoService _equipamentoService;
        private readonly IVinculoService _vinculoService;
        private readonly IApiUsuariosService _apiUsuariosService;
        private readonly IEmailService _emailService;

        public RelatoriosController(
            IUsuarioService usuarioService,
            IEquipamentoService equipamentoService,
            IApiUsuariosService apiUsuariosService,
            IVinculoService vinculoService,
            IEmailService emailService
        )
        {
            _usuarioService = usuarioService;
            _equipamentoService = equipamentoService;
            _apiUsuariosService = apiUsuariosService;
            _vinculoService = vinculoService;
            _emailService = emailService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ExportarCsv(string tipo)
        {
            if (string.IsNullOrEmpty(tipo))
                return Content("Tipo de exportação não informado.");

            StringBuilder sb = new StringBuilder();
            string nomeArquivo = "Exportacao.csv";

            switch (tipo)
            {
                case "Usuarios":
                    var usuarios = _usuarioService.ObterTodos();
                    if (!usuarios.Any()) return Content("Nenhum usuário encontrado.");
                    sb.AppendLine("Nome,Email,Departamento");
                    foreach (var u in usuarios)
                        sb.AppendLine($"{u.Nome},{u.Email},{u.Departamento}");
                    nomeArquivo = "Usuarios.csv";
                    break;

                case "Equipamentos":
                    var equipamentos = _equipamentoService.ObterTodos();
                    if (!equipamentos.Any()) return Content("Nenhum equipamento encontrado.");
                    sb.AppendLine("Nome,Número de Série,Tipo,DataAquisição,Status");
                    foreach (var e in equipamentos)
                        sb.AppendLine($"{e.Nome},{e.NumeroSerie},{e.Tipo},{e.DataAquisicao},{(e.Status ? "Ativo" : "Inativo")}");
                    nomeArquivo = "Equipamentos.csv";
                    break;

                case "ApiUsuarios":
                    // Simulação: chamar método do service que busca API
                    var apiUsuarios = _apiUsuariosService.ListarUsuariosAsync().Result;
                    if (!apiUsuarios.Any()) return Content("Nenhum usuário da API encontrado.");
                    sb.AppendLine("Nome,Email,Email");
                    foreach (var u in apiUsuarios)
                        sb.AppendLine($"{u.Name},{u.Email},{u.Email}");
                    nomeArquivo = "ApiUsuarios.csv";
                    break;

                case "Vinculos":
                    var vinculos = _vinculoService.ObterTodos();
                    if (!vinculos.Any()) return Content("Nenhum vínculo encontrado.");
                    sb.AppendLine("Usuário,Equipamento,DataVinculo");
                    foreach (var v in vinculos)
                        sb.AppendLine($"{v.NomeUsuario},{v.NomeEquipamento},{v.DataVinculo:yyyy-MM-dd HH:mm}");
                    nomeArquivo = "Vinculos.csv";
                    break;

                case "Emails":
                    var emails = _emailService.ObterTodos(); // Ajuste para retornar lista de logs
                    if (!emails.Any()) return Content("Nenhum e-mail registrado.");
                    sb.AppendLine("Usuário,Equipamento,TipoVinculo,DataEnvio");
                    foreach (var e in emails)
                        sb.AppendLine($"{e.NomeUsuario},{e.NomeEquipamento},{(e.Tipo == 1 ? "Vínculo" : "Desvinculo")},{e.DataEnvioEmail:yyyy-MM-dd HH:mm}");
                    nomeArquivo = "Emails.csv";
                    break;

                default:
                    return Content("Tipo de exportação inválido.");
            }

            var bytes = Encoding.UTF8.GetPreamble().Concat(Encoding.UTF8.GetBytes(sb.ToString())).ToArray();
            return File(bytes, "text/csv", nomeArquivo);
        }
    }
}
