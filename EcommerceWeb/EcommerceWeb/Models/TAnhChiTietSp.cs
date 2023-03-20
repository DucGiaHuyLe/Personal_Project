using System;
using System.Collections.Generic;

namespace EcommerceWeb.Models
{
    public partial class TAnhChiTietSp
    {
        public int MaChiTietSp { get; set; }
        public string TenFileAnh { get; set; } = null!;
        public string Vitri { get; set; } = null!;

        public virtual TChiTietSanPham MaChiTietSpNavigation { get; set; } = null!;
    }
}
