using Microsoft.AspNetCore.Mvc;

namespace CadastroEquipamento.Web.Controllers
{
    public class RelatoriosController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
