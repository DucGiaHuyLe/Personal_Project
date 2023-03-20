using System;
using System.Collections.Generic;

namespace EcommerceWeb.Models
{
    public partial class TDanhMucSp
    {
        public TDanhMucSp()
        {
            TAnhSps = new HashSet<TAnhSp>();
            TChiTietSanPhams = new HashSet<TChiTietSanPham>();
        }

        public int MaSp { get; set; }
        public string? TenSp { get; set; }
        public int? MaChatLieu { get; set; }
        public string? NganLaptop { get; set; }
        public string? Model { get; set; }
        public string? CanNang { get; set; }
        public string? DoNoi { get; set; }
        public int? MaHangSx { get; set; }
        public int? MaNuocSx { get; set; }
        public string? MaDacTinh { get; set; }
        public string? Website { get; set; }
        public string? ThoiGianBaoHanh { get; set; }
        public string? GioiThieuSp { get; set; }
        public string? ChietKhau { get; set; }
        public int? MaLoai { get; set; }
        public int? MaDt { get; set; }
        public string? AnhDaiDien { get; set; }
        public string? GiaNhoNhat { get; set; }
        public string? GiaiLonNhat { get; set; }

        public virtual TChatLieu MaChatLieuNavigation { get; set; } = null!;
        public virtual TLoaiDt MaDtNavigation { get; set; } = null!;
        public virtual THangSx? MaHangSxNavigation { get; set; }
        public virtual TLoaiSp MaLoaiNavigation { get; set; } = null!;
        public virtual TQuocGium? MaNuocSxNavigation { get; set; }
        public virtual ICollection<TAnhSp> TAnhSps { get; set; }
        public virtual ICollection<TChiTietSanPham> TChiTietSanPhams { get; set; }
    }
}
