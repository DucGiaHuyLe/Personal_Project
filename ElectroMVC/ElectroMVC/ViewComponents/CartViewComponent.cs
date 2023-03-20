using ElectroMVC.Data;
using ElectroMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.ComponentModel;
using System.Drawing;
using System.Security.Claims;

namespace ElectroMVC.ViewComponents
{
    [ViewComponent(Name ="_Cart")]
    public class CartViewComponent : ViewComponent
    {
        private readonly ElectroMVCContext _context;
        public CartViewComponent(ElectroMVCContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
			var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
			
			var cart = _context.Cart.Include(p => p.Product).Where(x=>x.UserId.ToString()== userId).ToList();
            if (!cart.Any())
            {
                var empty_cart = new List<Cart>();
                empty_cart.Add(new Cart());

				ViewBag.subtotal = 0;
				return View("_Cart", empty_cart);
			}

            decimal subtotal = 0;
			

			foreach (var item in cart)
			{
                if (item.Product.ProductPrice != null)
                {
					subtotal += (decimal)item.Product.ProductPrice * Convert.ToDecimal(item.Quantity);
				} 
                else
                {
                    subtotal += 0;
                }
			}
			ViewBag.subtotal = subtotal;
			return View("_Cart", cart);
        }

    }
}
