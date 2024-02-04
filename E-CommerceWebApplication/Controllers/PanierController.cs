using Microsoft.AspNetCore.Mvc;

namespace E_CommerceWebApplication.Controllers
{
    public class PanierController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
