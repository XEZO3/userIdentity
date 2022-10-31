using Microsoft.AspNetCore.Mvc;

namespace userIdentity.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
