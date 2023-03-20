using System;
using System.Collections.Generic;

namespace EcommerceWeb.Models
{
    public partial class THoaDonBan
    {
        public THoaDonBan()
        {
            TChiTietHdbs = new HashSet<TChiTietHdb>();
        }

        public int MaHoaDon { get; set; }
        public string? NgayHoaDon { get; set; }
        public int? MaKhachHang { get; set; }
        public int? MaNhanVien { get; set; }
        public int? TongTienHd { get; set; }
        public int? GiamGiaHd { get; set; }
        public string? PhuongThucThanhToan { get; set; }
        public string? MaSoThue { get; set; }
        public string? ThongTinThue { get; set; }
        public string? GhiChu { get; set; }

        public virtual TKhachHang? MaKhachHangNavigation { get; set; }
        public virtual TNhanVien? MaNhanVienNavigation { get; set; }
        public virtual ICollection<TChiTietHdb> TChiTietHdbs { get; set; }
    }
}
