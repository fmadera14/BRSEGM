using Microsoft.AspNetCore.Mvc;

namespace ConfiguracioParametros.Controllers
{
    public class ParametrosController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
