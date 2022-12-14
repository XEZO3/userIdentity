using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Claims;
using userIdentity.Data;
using userIdentity.Models;

namespace userIdentity.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly CoursesContext _context;
        private readonly SignInManager<userAuth> _signInManager;
        private readonly UserManager<userAuth> _UserManager;

        public HomeController(ILogger<HomeController> logger, CoursesContext context,SignInManager<userAuth> signInManager,UserManager<userAuth> userManager)
        {
            _logger = logger;
            _context = context;
            _signInManager = signInManager;
            _UserManager = userManager;
        }

        public IActionResult Index()
        {
            if (_signInManager.IsSignedIn(User)) {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                var count = _context.Cart.Include(x => x.cartItems).FirstOrDefault(x => x.UserId == userId)?.cartItems;
                HttpContext.Session.SetInt32("cart", (count ==null)?0:count.Count());
            }
            
            
            
            var courses = _context.Courses.ToList();
            return View(courses);
        }
        [Authorize(Roles ="Admin")]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}