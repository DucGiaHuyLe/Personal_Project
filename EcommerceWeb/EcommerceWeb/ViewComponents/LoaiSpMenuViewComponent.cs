using EcommerceWeb.Repository;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceWeb.ViewComponents
{
    public class LoaiSpMenuViewComponent: ViewComponent
    {
        private readonly ILoaiSPRepository _loaiSp;
        public LoaiSpMenuViewComponent(ILoaiSPRepository loaiSPRepository)
        {
            _loaiSp= loaiSPRepository;
        }
        public IViewComponentResult Invoke()
        {
            var loaisp = _loaiSp.GetAllLoaiSp().OrderBy(x => x.Loai);
            return View(loaisp);
        }
    }
}
