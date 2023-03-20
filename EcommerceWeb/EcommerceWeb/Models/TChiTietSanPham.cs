using System;
using System.Collections.Generic;

namespace EcommerceWeb.Models
{
    public partial class TChiTietSanPham
    {
        public TChiTietSanPham()
        {
            TAnhChiTietSps = new HashSet<TAnhChiTietSp>();
            TChiTietHdbs = new HashSet<TChiTietHdb>();
        }

        public int MaChiTietSp { get; set; }
        public int? MaSp { get; set; }
        public int? MaKichThuoc { get; set; }
        public int? MaMauSac { get; set; }
        public string? AnhDaiDien { get; set; }
        public string? Video { get; set; }
        public string? DonGiaBan { get; set; }
        public string? GiamGia { get; set; }
        public string? Slton { get; set; }

        public virtual TMauSac? MaMauSac1 { get; set; }
        public virtual TKichThuoc? MaMauSacNavigation { get; set; }
        public virtual TDanhMucSp? MaSpNavigation { get; set; }
        public virtual ICollection<TAnhChiTietSp> TAnhChiTietSps { get; set; }
        public virtual ICollection<TChiTietHdb> TChiTietHdbs { get; set; }
    }
}
