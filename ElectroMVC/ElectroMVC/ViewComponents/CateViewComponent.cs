using ElectroMVC.Data;
using ElectroMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System.ComponentModel;

namespace ElectroMVC.ViewComponents
{
    [ViewComponent(Name ="_Cate")]
    public class CateViewComponent : ViewComponent
    {
        private readonly ElectroMVCContext _context;
        public CateViewComponent(ElectroMVCContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {

            var _category = _context.Category.ToList();

			List<int> quantity_list = new List<int>();
			foreach (var item in _category)
			{
				int quantity = _context.Product.Where(x => x.CategoryId == item.CategoryId).Count();
				quantity_list.Add(quantity);
			}
			ViewBag.quantity_list = quantity_list;

			return View("_Cate", _category);
        }

    }
}
