using ElectroMVC.Data;
using ElectroMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Claims;

namespace ElectroMVC.Controllers
{
    public class HomeController : Controller
    {
		private readonly ElectroMVCContext _context;
		public HomeController(ElectroMVCContext context)
        {
			_context = context;
		}

		public IActionResult Index()
		{
            var product = _context.Product.Include(p=>p.Brand).Include(p=>p.Category).ToList();
			return View(product);
		}
		public IActionResult ProductDetail(int? id) //Use ViewBag
		{
			var product = _context.Product
				.Include(p => p.Brand)
				.Include(p => p.Category)
				.FirstOrDefault(m => m.ProductId == id);
			return View(product);
		}
		[HttpPost]
		public IActionResult AddtoCart(int id) 
		{
			Cart cart;
			var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

			if (userId == null)
			{
				return RedirectToAction("Login", "Account");
			}

			var check_cart = _context.Cart.FirstOrDefault(m => m.ProductId == id);
			if (check_cart == null)
			{
				cart = new Cart
				{
					ProductId = id,
					UserId = int.Parse(userId),
					Quantity = 1,
				};
				if (ModelState.IsValid)
				{
					_context.Cart.Add(cart);
					_context.SaveChanges();
				}
			} 
			else
			{
				check_cart.Quantity += 1;
				if (ModelState.IsValid)
				{
					_context.Cart.Update(check_cart);
					_context.SaveChanges();
				}
			}
			
			return StatusCode(200);
		}
			
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