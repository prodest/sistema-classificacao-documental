using Microsoft.AspNetCore.Mvc;
using Prodest.Scd.Presentation.Base;
using System.Threading.Tasks;

namespace Prodest.Scd.Web.Controllers
{
    public class HomeController : Controller
    {
        IPlanoClassificacaoService _service;

        public HomeController(IPlanoClassificacaoService service)
        {
            _service = service;
        }

        public IActionResult TemplateMVC()
        {
            return View();
        }

        public IActionResult Index()
        {

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
