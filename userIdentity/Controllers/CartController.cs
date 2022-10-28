using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using userIdentity.Data;
using userIdentity.Models;

namespace userIdentity.Controllers
{
    public class CartController : Controller
    {
        private readonly CoursesContext _context;
        public CartController(CoursesContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {

            return View();
        }
        public async  Task<IActionResult> AddToCart(int Id)
        {
            //Courses courses = _context.Courses.Find(Id);
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var cart = _context.Cart.FirstOrDefault(x => x.UserId == userId);
            if (cart == null)
            {
                cart = new Cart()
                {
                    UserId = userId,
                    CreateDate = DateTime.Now,
                };
                cart.cartItems = new List<CartItems>()
                {
                    new CartItems(){CartId=cart.Id,CoursesId=1}
                };
                _context.Cart.Add(cart);
               await _context.SaveChangesAsync();


            }
            return View();
        }
    }
}
