using System;
using System.Collections.Generic;

namespace EcommerceWeb.Models
{
    public partial class TKichThuoc
    {
        public TKichThuoc()
        {
            TChiTietSanPhams = new HashSet<TChiTietSanPham>();
        }

        public int MaKichThuoc { get; set; }
        public string? KichThuoc { get; set; }

        public virtual ICollection<TChiTietSanPham> TChiTietSanPhams { get; set; }
    }
}
