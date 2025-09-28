using Microsoft.AspNetCore.Mvc;

namespace CadastroEquipamento.Web.Controllers
{
    public class ApiUsuariosController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
