using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using userIdentity.Data;
using userIdentity.Models;

namespace userIdentity.Controllers
{
    public class CartController : Controller
    {
        private readonly CoursesContext _context;
        public CartController(CoursesContext context) {
            _context = context;
        }
        public IActionResult Index()
        {

            return View();
        }
        public IActionResult AddToCart(int Id)
        {
            Courses courses = _context.Courses.Find(Id);
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            
            var cart = _context.Cart.Where(x => x.UserId == userId).ToList();
            if (cart.Count == 0) {
                Cart ncart = new Cart()
                {
                    UserId = userId,
                    CreateDate = DateTime.Now,
                };

                _context.Cart.Add(ncart);
                _context.SaveChanges();

            }
            return View();
        }
    }
}
