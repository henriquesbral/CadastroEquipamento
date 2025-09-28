using CadastroEquipamento.Application.Services;
using CadastroEquipamento.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace CadastroEquipamento.Web.Controllers
{
    public class ApiUsuariosController : Controller
    {
        private readonly ApiUsuariosService _service;

        public ApiUsuariosController(ApiUsuariosService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var usuarios = await _service.ListarUsuariosAsync();

            var viewModel = usuarios.Select(u => new ApiUsuariosViewModel
            {
                Id = u.Id,
                Nome = u.Name,
                Email = u.Email,
                Telefone = u.Phone,
                Empresa = u.Company.Name,
                Cidade = u.Address.City
            });

            return View(viewModel);
        }

    }
}
