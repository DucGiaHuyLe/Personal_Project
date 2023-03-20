using ElectroMVC.Data;
using ElectroMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System.ComponentModel;

namespace ElectroMVC.ViewComponents
{
    [ViewComponent(Name ="_Category")]
    public class CategoryMenuViewComponent: ViewComponent
    {
        private readonly ElectroMVCContext _context;
        public CategoryMenuViewComponent(ElectroMVCContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {

            var _category = _context.Category.ToList();
            return View("_Category", _category);
        }

    }
}
