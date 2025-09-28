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
        private readonly IApiUsuariosService _apiUsuariossService;

        public DashboardController(IUsuarioService usuarioService, 
            IEquipamentoService equipamentoService,
            IVinculoService vinculoService,
            IApiUsuariosService apiUsuariosService
            )
        {
            _usuarioService = usuarioService;
            _equipamentoService = equipamentoService;
            _vinculoService = vinculoService;
            _apiUsuariossService = apiUsuariosService;
        }

        public IActionResult Index()
        {
            var usuariosCount = _usuarioService.ObterTodos().Count();
            var equipamentosCount = _equipamentoService.ObterTodos().Count();
            var vinculosCount = _vinculoService.ObterTodos().Count();
            var apiUsuariosCount = _apiUsuariossService.ListarUsuariosAsync().Result.Count();

            var dashboard = new DashboardViewModel
            {
                TotalUsuarios = usuariosCount,
                TotalEquipamentos = equipamentosCount,
                TotalVinculos = vinculosCount,
                TotalUsuariosAPI = apiUsuariosCount
            };

            return View(dashboard);
        }
    }
}
