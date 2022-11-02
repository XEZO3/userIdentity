using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using userIdentity.Data;
using userIdentity.Models;

namespace userIdentity.Controllers
{
    public class CartController : Controller
    {
        private readonly CoursesContext _context;
        
        private readonly SignInManager<userAuth> _signInManager;
        private readonly UserManager<userAuth> _UserManager;
        public CartController(CoursesContext context, SignInManager<userAuth> signInManager, UserManager<userAuth> UserManager)
        {
            _context = context;
            _signInManager = signInManager;
            _UserManager = UserManager;
        }
        public IActionResult Index()
        {

            var items = _context.cartItems.Include(x=>x.cart).Include(x=>x.courses).Where(x=>x.cart.UserId == _UserManager.GetUserId(User));
            return View(items);
        }
        public async Task<int> AddToCart(int Id)
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
                    new CartItems(){CartId=cart.Id,CoursesId=Id}
                };
                _context.Cart.Add(cart);
                await _context.SaveChangesAsync();


            }
            else
            {
                var course = _context.cartItems.Include(x => x.cart).FirstOrDefault(x => x.CoursesId == Id && x.cart.UserId == userId);
                if (course == null)
                {

                    cart.cartItems.Add(new CartItems() { CartId = cart.Id, CoursesId = Id, });

                }
                else
                {
                    return 0;
                }
                _context.Cart.Update(cart);
                await _context.SaveChangesAsync();

            }
            var count = _context.cartItems.Include(x => x.cart).Where(x => x.CartId == cart.Id).Count();
            HttpContext.Session.SetInt32("cart", count);
            return count;
        }
        public async Task<List<dynamic>> RemoveFromCart(int Id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var cart = _context.cartItems.FirstOrDefault(x=>x.CoursesId == Id);
            if (cart == null) {
                return new List<dynamic> { "empty",0};
            }
            else
            {
                _context.cartItems.Remove(cart);
                await _context.SaveChangesAsync();
                var count = _context.Cart.Include(x => x.cartItems).FirstOrDefault(x => x.UserId == userId)?.cartItems.Count();
                HttpContext.Session.SetInt32("cart", (int)count);
                return new List<dynamic> { "full", count };
            }
            
           
        }
        public IActionResult checkOut() {
            var cartItem = _context.Cart.Include(x => x.cartItems).ThenInclude(x=>x.courses).FirstOrDefault(x => x.UserId == _UserManager.GetUserId(User)).cartItems.ToList();
            var total = cartItem.Sum(x => x.courses.Price);
            Order order = new Order() {
                 State = "pending", UserId = _UserManager.GetUserId(User),Totalprice = total
        };
            
            foreach (var item in cartItem) {
            
            order.orderItems.Add(new OrderItem() { OrderId = order.Id, CoursesId = item.CoursesId });
            }
            _context.Order.Add(order);
            _context.SaveChanges();
        return RedirectToAction("index");
        }
       
    }
}
