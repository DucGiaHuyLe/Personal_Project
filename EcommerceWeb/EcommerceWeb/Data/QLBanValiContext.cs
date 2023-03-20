using System;
using System.Collections.Generic;
using EcommerceWeb.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace EcommerceWeb.Data
{
    public partial class QLBanValiContext : DbContext
    {
        public QLBanValiContext()
        {
        }

        public QLBanValiContext(DbContextOptions<QLBanValiContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TAnhChiTietSp> TAnhChiTietSps { get; set; } = null!;
        public virtual DbSet<TAnhSp> TAnhSps { get; set; } = null!;
        public virtual DbSet<TChatLieu> TChatLieus { get; set; } = null!;
        public virtual DbSet<TChiTietHdb> TChiTietHdbs { get; set; } = null!;
        public virtual DbSet<TChiTietSanPham> TChiTietSanPhams { get; set; } = null!;
        public virtual DbSet<TDanhMucSp> TDanhMucSps { get; set; } = null!;
        public virtual DbSet<THangSx> THangSxes { get; set; } = null!;
        public virtual DbSet<THoaDonBan> THoaDonBans { get; set; } = null!;
        public virtual DbSet<TKhachHang> TKhachHangs { get; set; } = null!;
        public virtual DbSet<TKichThuoc> TKichThuocs { get; set; } = null!;
        public virtual DbSet<TLoaiDt> TLoaiDts { get; set; } = null!;
        public virtual DbSet<TLoaiSp> TLoaiSps { get; set; } = null!;
        public virtual DbSet<TMauSac> TMauSacs { get; set; } = null!;
        public virtual DbSet<TNhanVien> TNhanViens { get; set; } = null!;
        public virtual DbSet<TQuocGium> TQuocGia { get; set; } = null!;
        public virtual DbSet<TUser> TUsers { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=QLBanVali;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TAnhChiTietSp>(entity =>
            {
                entity.HasKey(e => new { e.MaChiTietSp, e.TenFileAnh });

                entity.ToTable("tAnhChiTietSP");

                entity.Property(e => e.MaChiTietSp).HasColumnName("MaChiTietSP");

                entity.Property(e => e.TenFileAnh)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Vitri)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.MaChiTietSpNavigation)
                    .WithMany(p => p.TAnhChiTietSps)
                    .HasForeignKey(d => d.MaChiTietSp)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tAnhChiTietSP_tChiTietSanPham");
            });

            modelBuilder.Entity<TAnhSp>(entity =>
            {
                entity.HasKey(e => new { e.MaSp, e.TenFileAnh });

                entity.ToTable("tAnhSP");

                entity.Property(e => e.MaSp).HasColumnName("MaSP");

                entity.Property(e => e.TenFileAnh)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Vitri)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.MaSpNavigation)
                    .WithMany(p => p.TAnhSps)
                    .HasForeignKey(d => d.MaSp)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tAnhSP_tDanhMucSP");
            });

            modelBuilder.Entity<TChatLieu>(entity =>
            {
                entity.HasKey(e => e.MaChatLieu);

                entity.ToTable("tChatLieu");

                entity.Property(e => e.MaChatLieu).ValueGeneratedNever();

                entity.Property(e => e.ChatLieu)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TChiTietHdb>(entity =>
            {
                entity.HasKey(e => new { e.MaHoaDon, e.MaChiTietSp });

                entity.ToTable("tChiTietHDB");

                entity.Property(e => e.MaChiTietSp).HasColumnName("MaChiTietSP");

                entity.Property(e => e.DonGiaBan)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.GhiChu)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.GiamGia)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.MaChiTietSpNavigation)
                    .WithMany(p => p.TChiTietHdbs)
                    .HasForeignKey(d => d.MaChiTietSp)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tChiTietHDB_tChiTietSanPham");

                entity.HasOne(d => d.MaHoaDonNavigation)
                    .WithMany(p => p.TChiTietHdbs)
                    .HasForeignKey(d => d.MaHoaDon)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tChiTietHDB_tHoaDonBan");
            });

            modelBuilder.Entity<TChiTietSanPham>(entity =>
            {
                entity.HasKey(e => e.MaChiTietSp);

                entity.ToTable("tChiTietSanPham");

                entity.Property(e => e.MaChiTietSp)
                    .ValueGeneratedNever()
                    .HasColumnName("MaChiTietSP");

                entity.Property(e => e.AnhDaiDien)
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.DonGiaBan)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.GiamGia)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MaSp).HasColumnName("MaSP");

                entity.Property(e => e.Slton)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("SLTon");

                entity.Property(e => e.Video)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.MaMauSacNavigation)
                    .WithMany(p => p.TChiTietSanPhams)
                    .HasForeignKey(d => d.MaMauSac)
                    .HasConstraintName("FK_tChiTietSanPham_tKichThuoc");

                entity.HasOne(d => d.MaMauSac1)
                    .WithMany(p => p.TChiTietSanPhams)
                    .HasForeignKey(d => d.MaMauSac)
                    .HasConstraintName("FK_tChiTietSanPham_tMauSac");

                entity.HasOne(d => d.MaSpNavigation)
                    .WithMany(p => p.TChiTietSanPhams)
                    .HasForeignKey(d => d.MaSp)
                    .HasConstraintName("FK_tChiTietSanPham_tDanhMucSP");
            });

            modelBuilder.Entity<TDanhMucSp>(entity =>
            {
                entity.HasKey(e => e.MaSp);

                entity.ToTable("tDanhMucSP");

                entity.Property(e => e.MaSp)
                    .ValueGeneratedNever()
                    .HasColumnName("MaSP");

                entity.Property(e => e.AnhDaiDien)
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.CanNang)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ChietKhau)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DoNoi)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.GiaNhoNhat)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.GiaiLonNhat)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.GioiThieuSp)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("GioiThieuSP");

                entity.Property(e => e.MaDacTinh)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MaDt).HasColumnName("MaDT");

                entity.Property(e => e.MaHangSx).HasColumnName("MaHangSX");

                entity.Property(e => e.MaNuocSx).HasColumnName("MaNuocSX");

                entity.Property(e => e.Model)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NganLaptop)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TenSp)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("TenSP");

                entity.Property(e => e.ThoiGianBaoHanh)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Website)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.MaChatLieuNavigation)
                    .WithMany(p => p.TDanhMucSps)
                    .HasForeignKey(d => d.MaChatLieu)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tDanhMucSP_tChatLieu");

                entity.HasOne(d => d.MaDtNavigation)
                    .WithMany(p => p.TDanhMucSps)
                    .HasForeignKey(d => d.MaDt)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tDanhMucSP_tLoaiDT");

                entity.HasOne(d => d.MaHangSxNavigation)
                    .WithMany(p => p.TDanhMucSps)
                    .HasForeignKey(d => d.MaHangSx)
                    .HasConstraintName("FK_tDanhMucSP_tHangSX");

                entity.HasOne(d => d.MaLoaiNavigation)
                    .WithMany(p => p.TDanhMucSps)
                    .HasForeignKey(d => d.MaLoai)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tDanhMucSP_tLoaiSP");

                entity.HasOne(d => d.MaNuocSxNavigation)
                    .WithMany(p => p.TDanhMucSps)
                    .HasForeignKey(d => d.MaNuocSx)
                    .HasConstraintName("FK_tDanhMucSP_tQuocGia");
            });

            modelBuilder.Entity<THangSx>(entity =>
            {
                entity.HasKey(e => e.MaHangSx);

                entity.ToTable("tHangSX");

                entity.Property(e => e.MaHangSx)
                    .ValueGeneratedNever()
                    .HasColumnName("MaHangSX");

                entity.Property(e => e.HangSx)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("HangSX");

                entity.Property(e => e.MaNuocThuongHieu)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<THoaDonBan>(entity =>
            {
                entity.HasKey(e => e.MaHoaDon);

                entity.ToTable("tHoaDonBan");

                entity.Property(e => e.MaHoaDon).ValueGeneratedNever();

                entity.Property(e => e.GhiChu)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.GiamGiaHd).HasColumnName("GiamGiaHD");

                entity.Property(e => e.MaSoThue)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NgayHoaDon)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PhuongThucThanhToan)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ThongTinThue)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TongTienHd).HasColumnName("TongTienHD");

                entity.HasOne(d => d.MaKhachHangNavigation)
                    .WithMany(p => p.THoaDonBans)
                    .HasForeignKey(d => d.MaKhachHang)
                    .HasConstraintName("FK_tHoaDonBan_tKhachHang");

                entity.HasOne(d => d.MaNhanVienNavigation)
                    .WithMany(p => p.THoaDonBans)
                    .HasForeignKey(d => d.MaNhanVien)
                    .HasConstraintName("FK_tHoaDonBan_tNhanVien");
            });

            modelBuilder.Entity<TKhachHang>(entity =>
            {
                entity.HasKey(e => e.MaKhachHang);

                entity.ToTable("tKhachHang");

                entity.Property(e => e.MaKhachHang).ValueGeneratedNever();

                entity.Property(e => e.AnhDaiDien)
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.DiaChi)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.GhiChu)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LoaiKhachHang)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NgaySinh)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SoDienThoai)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TenKhachHang)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("username");

                entity.HasOne(d => d.UsernameNavigation)
                    .WithMany(p => p.TKhachHangs)
                    .HasForeignKey(d => d.Username)
                    .HasConstraintName("FK_tKhachHang_tUser");
            });

            modelBuilder.Entity<TKichThuoc>(entity =>
            {
                entity.HasKey(e => e.MaKichThuoc);

                entity.ToTable("tKichThuoc");

                entity.Property(e => e.MaKichThuoc).ValueGeneratedNever();

                entity.Property(e => e.KichThuoc)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TLoaiDt>(entity =>
            {
                entity.HasKey(e => e.MaDt);

                entity.ToTable("tLoaiDT");

                entity.Property(e => e.MaDt)
                    .ValueGeneratedNever()
                    .HasColumnName("MaDT");

                entity.Property(e => e.TenLoai)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TLoaiSp>(entity =>
            {
                entity.HasKey(e => e.MaLoai);

                entity.ToTable("tLoaiSP");

                entity.Property(e => e.MaLoai).ValueGeneratedNever();

                entity.Property(e => e.Loai)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TMauSac>(entity =>
            {
                entity.HasKey(e => e.MaMauSac);

                entity.ToTable("tMauSac");

                entity.Property(e => e.MaMauSac).ValueGeneratedNever();

                entity.Property(e => e.TenMauSac)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TNhanVien>(entity =>
            {
                entity.HasKey(e => e.MaNhanVien);

                entity.ToTable("tNhanVien");

                entity.Property(e => e.MaNhanVien).ValueGeneratedNever();

                entity.Property(e => e.AnhDaiDien)
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.ChucVu)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DiaChi)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.GhiChu)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NgaySinh)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SoDienThoai1)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SoDienThoai2)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TenNhanVien)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("username");

                entity.HasOne(d => d.UsernameNavigation)
                    .WithMany(p => p.TNhanViens)
                    .HasForeignKey(d => d.Username)
                    .HasConstraintName("FK_tNhanVien_tUser");
            });

            modelBuilder.Entity<TQuocGium>(entity =>
            {
                entity.HasKey(e => e.MaNuoc);

                entity.ToTable("tQuocGia");

                entity.Property(e => e.MaNuoc).ValueGeneratedNever();

                entity.Property(e => e.TenNuoc)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TUser>(entity =>
            {
                entity.HasKey(e => e.Username);

                entity.ToTable("tUser");

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("username");

                entity.Property(e => e.LoaiUser)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("password");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
