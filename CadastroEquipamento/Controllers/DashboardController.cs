using CadastroEquipamento.Application.Interfaces;
using CadastroEquipamento.Models;
using CadastroEquipamento.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CadastroEquipamento.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IEquipamentoService _equipamentoService;
        private readonly IVinculoService _vinculoService;

        public DashboardController(IUsuarioService usuarioService, IEquipamentoService equipamentoService,IVinculoService vinculoService )
        {
            _usuarioService = usuarioService;
            _equipamentoService = equipamentoService;
            _vinculoService = vinculoService;
        }

        public IActionResult Index()
        {
            var usuariosCount = _usuarioService.ObterTodos().Count();
            var equipamentosCount = _equipamentoService.ObterTodos().Count();
            var vinculosCount = _equipamentoService.ObterTodos().Count();

            var dashboard = new DashboardViewModel
            {
                TotalUsuarios = usuariosCount,
                TotalEquipamentos = equipamentosCount,
                TotalVinculos = vinculosCount
            };

            return View(dashboard);
        }
    }
}
