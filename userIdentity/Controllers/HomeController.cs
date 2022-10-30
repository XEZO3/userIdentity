using Microsoft.AspNetCore.Authorization;
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
        

        public HomeController(ILogger<HomeController> logger, CoursesContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            if (User != null) {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                var count = _context.Cart.Include(x => x.cartItems).FirstOrDefault(x => x.UserId == userId)?.cartItems.Count();
                HttpContext.Session.SetInt32("cart", (int)count);
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