using Microsoft.AspNetCore.Mvc;
using Prodest.Scd.Presentation.Base;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        IPlanoClassificacaoService _service;

        public HomeController(IPlanoClassificacaoService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            _service.Search(null);

            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
