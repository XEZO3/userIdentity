using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using userIdentity.Data;
using userIdentity.Models;

namespace userIdentity.Controllers
{
    public class OrderController : Controller
    {
        
        private readonly CoursesContext _context;
        private readonly SignInManager<userAuth> _signInManager;
        private readonly UserManager<userAuth> _UserManager;
        public OrderController(CoursesContext context, SignInManager<userAuth> signInManager, UserManager<userAuth> userManager) { 
            _context = context;
            _signInManager = signInManager;
            _UserManager = userManager;
        }
        public IActionResult Index()
        {
            var orders = _context.Order.Where(x=>x.UserId == _UserManager.GetUserId(User)).ToList();
            return View(orders);
        }
        public IActionResult Details(int Id)
        { 
            var order=_context.Order.Include(x=>x.orderItems).ThenInclude(x=>x.courses).FirstOrDefault(x=>x.Id == Id).orderItems.ToList();
            
            return View(order);
        }
    }
}
