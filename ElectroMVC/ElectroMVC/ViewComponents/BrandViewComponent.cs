using ElectroMVC.Data;
using ElectroMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System.ComponentModel;

namespace ElectroMVC.ViewComponents
{
    [ViewComponent(Name ="_Brand")]
    public class BrandViewComponent : ViewComponent
    {
        private readonly ElectroMVCContext _context;
        public BrandViewComponent(ElectroMVCContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {

            var _brand = _context.Brand.ToList();

			List<int> quantity_brand_list = new List<int>();
			foreach (var item in _brand)
			{
				int quantity = _context.Product.Where(x => x.BrandId == item.BrandId).Count();
				quantity_brand_list.Add(quantity);
			}
			ViewBag.quantity_brand_list = quantity_brand_list;

			return View("_Brand", _brand);
        }

    }
}
