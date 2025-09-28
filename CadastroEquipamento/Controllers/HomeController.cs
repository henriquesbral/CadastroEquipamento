using Microsoft.AspNetCore.Mvc;

namespace CadastroEquipamento.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
