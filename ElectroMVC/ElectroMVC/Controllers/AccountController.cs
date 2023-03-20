using ElectroMVC.Data;
using ElectroMVC.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ElectroMVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly ElectroMVCContext _context;

        public AccountController(ElectroMVCContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        //GET
        public IActionResult Login()
        {
            return View();
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(User _userFromPage)
        {
            var _user = _context.User.FirstOrDefault(m => m.UserEmail == _userFromPage.UserEmail && m.UserPassword == _userFromPage.UserPassword);
            if (_user == null && !ModelState.IsValid)
            {
                ViewBag.LoginStatus = 0;
                ModelState.AddModelError("UserEmail", "Incorrect UserName or Password!!");
                ModelState.AddModelError("UserPassword", "Incorrect UserName or Password!!");
            }
            else
            {
                var claims = new List<Claim>
                {
					new Claim(ClaimTypes.NameIdentifier, _user.UserId.ToString()),
					new Claim(ClaimTypes.Name, _user.UserEmail),
                    new Claim("FullName", _user.UserName),
                    new Claim(ClaimTypes.Role, _user.UserRole),
                };

                var claimsIdentity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                            
                };

                HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);
                if (_user.UserRole == "Administrator")
					return RedirectToAction("Index", "Admin");
				else
                    return RedirectToAction("Index", "Home");
            }
            return View();
        }
        //GET
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(
            CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
            //testing
        }
    }
}
