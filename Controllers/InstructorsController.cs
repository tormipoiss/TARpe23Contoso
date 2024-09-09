using Microsoft.AspNetCore.Mvc;

namespace Contoso_University.Controllers
{
    public class InstructorsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
