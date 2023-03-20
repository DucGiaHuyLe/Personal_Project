using System;
using System.Collections.Generic;

namespace EcommerceWeb.Models
{
    public partial class TKhachHang
    {
        public TKhachHang()
        {
            THoaDonBans = new HashSet<THoaDonBan>();
        }

        public int MaKhachHang { get; set; }
        public string? Username { get; set; }
        public string? TenKhachHang { get; set; }
        public string? NgaySinh { get; set; }
        public string? SoDienThoai { get; set; }
        public string? DiaChi { get; set; }
        public string? LoaiKhachHang { get; set; }
        public string? AnhDaiDien { get; set; }
        public string? GhiChu { get; set; }

        public virtual TUser? UsernameNavigation { get; set; }
        public virtual ICollection<THoaDonBan> THoaDonBans { get; set; }
    }
}
