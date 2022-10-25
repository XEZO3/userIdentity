using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using userIdentity.Models;
using userIdentity.Models.VM;
using userIdentity.Utilty;

namespace userIdentity.Controllers
{
    public class UserController : Controller
    {
        private readonly SignInManager<userAuth> _signInManager;
        private readonly UserManager<userAuth> _UserManager;
        //UserController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> UserManager) {
        //    _signInManager = signInManager;
        //    _UserManager = UserManager;
        //}
        public UserController(SignInManager<userAuth> signInManager, UserManager<userAuth> UserManager) {
            _signInManager = signInManager;
            _UserManager = UserManager;
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            userAuth user =  new userAuth()
            {
                Name = registerVM.Name,
                Email = registerVM.Email,
                UserName = registerVM.UserName,
                EmailConfirmed = true
            };
           var result=  await _UserManager.CreateAsync(user,registerVM.Password);
            if (result.Succeeded)
            {
                await _UserManager.AddToRoleAsync(user, userIdentity.Utilty.Utilty.Role_User);
                return RedirectToAction("Index", "Home");
            }
            else {
                foreach (var error in result.Errors)
                { 
                ModelState.AddModelError(String.Empty,error.Description);
                }
                return View();
            }
            return View();
        }
        [HttpGet]
        public IActionResult login() {
            return View();
        }
        public IActionResult logout() {
            _signInManager.SignOutAsync();
            return RedirectToAction("login");
        }
        public async Task<IActionResult> login(LoginVM loginVM)
        {
            var userName = await _UserManager.FindByNameAsync(loginVM.UserName);
            var isLogin = await _signInManager.PasswordSignInAsync(userName, loginVM.Password,true,false);

            if (isLogin.Succeeded)
            {
               
                return RedirectToAction("Index", "Home");
            }
            else { 
            return View();
            }
            
        }
    }
}
