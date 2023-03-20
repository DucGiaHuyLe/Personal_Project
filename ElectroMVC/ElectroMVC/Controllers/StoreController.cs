using ElectroMVC.Data;
using ElectroMVC.Migrations;
using ElectroMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

namespace ElectroMVC.Controllers
{
	public class StoreController : Controller
	{
		private readonly ElectroMVCContext _context;
		public StoreController(ElectroMVCContext context)
		{
			_context = context;
		}
		public IActionResult Index()
		{
			var product = _context.Product.Include(p => p.Brand).Include(p => p.Category).ToList();
			ViewBag.length = _context.Category.Count();
			return View(product);
		}

		//[HttpPost]
		//public IActionResult Index(int? id)
		//{
		//	var cat = _context.Category.Find(id);

		//	if (cat.AreChecked == false) cat.AreChecked = true;
		//	else cat.AreChecked = false;
		//	_context.Category.Update(cat);
		//	_context.SaveChanges();

		//	//var product_lst = _context.Product.AsNoTracking().Where
		//	//(x => x.Category.AreChecked == category.ToList()[0].AreChecked).Include(p => p.Brand).Include(p => p.Category).ToList();
		//	var product_lst = _context.Product.AsNoTracking().Where
		//	(x => x.Category.AreChecked == true).Include(p => p.Brand).Include(p => p.Category).ToList();
		//	return View(product_lst);
		//}
    }
}
