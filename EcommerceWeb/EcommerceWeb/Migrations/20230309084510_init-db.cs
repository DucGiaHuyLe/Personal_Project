using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcommerceWeb.Migrations
{
    public partial class initdb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tChatLieu",
                columns: table => new
                {
                    MaChatLieu = table.Column<int>(type: "int", nullable: false),
                    ChatLieu = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tChatLieu", x => x.MaChatLieu);
                });

            migrationBuilder.CreateTable(
                name: "tHangSX",
                columns: table => new
                {
                    MaHangSX = table.Column<int>(type: "int", nullable: false),
                    HangSX = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    MaNuocThuongHieu = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tHangSX", x => x.MaHangSX);
                });

            migrationBuilder.CreateTable(
                name: "tKichThuoc",
                columns: table => new
                {
                    MaKichThuoc = table.Column<int>(type: "int", nullable: false),
                    KichThuoc = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tKichThuoc", x => x.MaKichThuoc);
                });

            migrationBuilder.CreateTable(
                name: "tLoaiDT",
                columns: table => new
                {
                    MaDT = table.Column<int>(type: "int", nullable: false),
                    TenLoai = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tLoaiDT", x => x.MaDT);
                });

            migrationBuilder.CreateTable(
                name: "tLoaiSP",
                columns: table => new
                {
                    MaLoai = table.Column<int>(type: "int", nullable: false),
                    Loai = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tLoaiSP", x => x.MaLoai);
                });

            migrationBuilder.CreateTable(
                name: "tMauSac",
                columns: table => new
                {
                    MaMauSac = table.Column<int>(type: "int", nullable: false),
                    TenMauSac = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tMauSac", x => x.MaMauSac);
                });

            migrationBuilder.CreateTable(
                name: "tQuocGia",
                columns: table => new
                {
                    MaNuoc = table.Column<int>(type: "int", nullable: false),
                    TenNuoc = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tQuocGia", x => x.MaNuoc);
                });

            migrationBuilder.CreateTable(
                name: "tUser",
                columns: table => new
                {
                    username = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    password = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    LoaiUser = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tUser", x => x.username);
                });

            migrationBuilder.CreateTable(
                name: "tDanhMucSP",
                columns: table => new
                {
                    MaSP = table.Column<int>(type: "int", nullable: false),
                    TenSP = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    MaChatLieu = table.Column<int>(type: "int", nullable: true),
                    NganLaptop = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Model = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    CanNang = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    DoNoi = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    MaHangSX = table.Column<int>(type: "int", nullable: true),
                    MaNuocSX = table.Column<int>(type: "int", nullable: true),
                    MaDacTinh = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Website = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    ThoiGianBaoHanh = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    GioiThieuSP = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    ChietKhau = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    MaLoai = table.Column<int>(type: "int", nullable: true),
                    MaDT = table.Column<int>(type: "int", nullable: true),
                    AnhDaiDien = table.Column<string>(type: "varchar(300)", unicode: false, maxLength: 300, nullable: true),
                    GiaNhoNhat = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    GiaiLonNhat = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tDanhMucSP", x => x.MaSP);
                    table.ForeignKey(
                        name: "FK_tDanhMucSP_tChatLieu",
                        column: x => x.MaChatLieu,
                        principalTable: "tChatLieu",
                        principalColumn: "MaChatLieu");
                    table.ForeignKey(
                        name: "FK_tDanhMucSP_tHangSX",
                        column: x => x.MaHangSX,
                        principalTable: "tHangSX",
                        principalColumn: "MaHangSX");
                    table.ForeignKey(
                        name: "FK_tDanhMucSP_tLoaiDT",
                        column: x => x.MaDT,
                        principalTable: "tLoaiDT",
                        principalColumn: "MaDT");
                    table.ForeignKey(
                        name: "FK_tDanhMucSP_tLoaiSP",
                        column: x => x.MaLoai,
                        principalTable: "tLoaiSP",
                        principalColumn: "MaLoai");
                    table.ForeignKey(
                        name: "FK_tDanhMucSP_tQuocGia",
                        column: x => x.MaNuocSX,
                        principalTable: "tQuocGia",
                        principalColumn: "MaNuoc");
                });

            migrationBuilder.CreateTable(
                name: "tKhachHang",
                columns: table => new
                {
                    MaKhachHang = table.Column<int>(type: "int", nullable: false),
                    username = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    TenKhachHang = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    NgaySinh = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    SoDienThoai = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    DiaChi = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    LoaiKhachHang = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    AnhDaiDien = table.Column<string>(type: "varchar(300)", unicode: false, maxLength: 300, nullable: true),
                    GhiChu = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tKhachHang", x => x.MaKhachHang);
                    table.ForeignKey(
                        name: "FK_tKhachHang_tUser",
                        column: x => x.username,
                        principalTable: "tUser",
                        principalColumn: "username");
                });

            migrationBuilder.CreateTable(
                name: "tNhanVien",
                columns: table => new
                {
                    MaNhanVien = table.Column<int>(type: "int", nullable: false),
                    username = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    TenNhanVien = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    NgaySinh = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    SoDienThoai1 = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    SoDienThoai2 = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    DiaChi = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    ChucVu = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    AnhDaiDien = table.Column<string>(type: "varchar(300)", unicode: false, maxLength: 300, nullable: true),
                    GhiChu = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tNhanVien", x => x.MaNhanVien);
                    table.ForeignKey(
                        name: "FK_tNhanVien_tUser",
                        column: x => x.username,
                        principalTable: "tUser",
                        principalColumn: "username");
                });

            migrationBuilder.CreateTable(
                name: "tAnhSP",
                columns: table => new
                {
                    MaSP = table.Column<int>(type: "int", nullable: false),
                    TenFileAnh = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Vitri = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tAnhSP", x => new { x.MaSP, x.TenFileAnh });
                    table.ForeignKey(
                        name: "FK_tAnhSP_tDanhMucSP",
                        column: x => x.MaSP,
                        principalTable: "tDanhMucSP",
                        principalColumn: "MaSP");
                });

            migrationBuilder.CreateTable(
                name: "tChiTietSanPham",
                columns: table => new
                {
                    MaChiTietSP = table.Column<int>(type: "int", nullable: false),
                    MaSP = table.Column<int>(type: "int", nullable: true),
                    MaKichThuoc = table.Column<int>(type: "int", nullable: true),
                    MaMauSac = table.Column<int>(type: "int", nullable: true),
                    AnhDaiDien = table.Column<string>(type: "varchar(300)", unicode: false, maxLength: 300, nullable: true),
                    Video = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    DonGiaBan = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    GiamGia = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    SLTon = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tChiTietSanPham", x => x.MaChiTietSP);
                    table.ForeignKey(
                        name: "FK_tChiTietSanPham_tDanhMucSP",
                        column: x => x.MaSP,
                        principalTable: "tDanhMucSP",
                        principalColumn: "MaSP");
                    table.ForeignKey(
                        name: "FK_tChiTietSanPham_tKichThuoc",
                        column: x => x.MaMauSac,
                        principalTable: "tKichThuoc",
                        principalColumn: "MaKichThuoc");
                    table.ForeignKey(
                        name: "FK_tChiTietSanPham_tMauSac",
                        column: x => x.MaMauSac,
                        principalTable: "tMauSac",
                        principalColumn: "MaMauSac");
                });

            migrationBuilder.CreateTable(
                name: "tHoaDonBan",
                columns: table => new
                {
                    MaHoaDon = table.Column<int>(type: "int", nullable: false),
                    NgayHoaDon = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    MaKhachHang = table.Column<int>(type: "int", nullable: true),
                    MaNhanVien = table.Column<int>(type: "int", nullable: true),
                    TongTienHD = table.Column<int>(type: "int", nullable: true),
                    GiamGiaHD = table.Column<int>(type: "int", nullable: true),
                    PhuongThucThanhToan = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    MaSoThue = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    ThongTinThue = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    GhiChu = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tHoaDonBan", x => x.MaHoaDon);
                    table.ForeignKey(
                        name: "FK_tHoaDonBan_tKhachHang",
                        column: x => x.MaKhachHang,
                        principalTable: "tKhachHang",
                        principalColumn: "MaKhachHang");
                    table.ForeignKey(
                        name: "FK_tHoaDonBan_tNhanVien",
                        column: x => x.MaNhanVien,
                        principalTable: "tNhanVien",
                        principalColumn: "MaNhanVien");
                });

            migrationBuilder.CreateTable(
                name: "tAnhChiTietSP",
                columns: table => new
                {
                    MaChiTietSP = table.Column<int>(type: "int", nullable: false),
                    TenFileAnh = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Vitri = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tAnhChiTietSP", x => new { x.MaChiTietSP, x.TenFileAnh });
                    table.ForeignKey(
                        name: "FK_tAnhChiTietSP_tChiTietSanPham",
                        column: x => x.MaChiTietSP,
                        principalTable: "tChiTietSanPham",
                        principalColumn: "MaChiTietSP");
                });

            migrationBuilder.CreateTable(
                name: "tChiTietHDB",
                columns: table => new
                {
                    MaHoaDon = table.Column<int>(type: "int", nullable: false),
                    MaChiTietSP = table.Column<int>(type: "int", nullable: false),
                    SoLuongBan = table.Column<int>(type: "int", nullable: true),
                    DonGiaBan = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    GiamGia = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    GhiChu = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tChiTietHDB", x => new { x.MaHoaDon, x.MaChiTietSP });
                    table.ForeignKey(
                        name: "FK_tChiTietHDB_tChiTietSanPham",
                        column: x => x.MaChiTietSP,
                        principalTable: "tChiTietSanPham",
                        principalColumn: "MaChiTietSP");
                    table.ForeignKey(
                        name: "FK_tChiTietHDB_tHoaDonBan",
                        column: x => x.MaHoaDon,
                        principalTable: "tHoaDonBan",
                        principalColumn: "MaHoaDon");
                });

            migrationBuilder.CreateIndex(
                name: "IX_tChiTietHDB_MaChiTietSP",
                table: "tChiTietHDB",
                column: "MaChiTietSP");

            migrationBuilder.CreateIndex(
                name: "IX_tChiTietSanPham_MaMauSac",
                table: "tChiTietSanPham",
                column: "MaMauSac");

            migrationBuilder.CreateIndex(
                name: "IX_tChiTietSanPham_MaSP",
                table: "tChiTietSanPham",
                column: "MaSP");

            migrationBuilder.CreateIndex(
                name: "IX_tDanhMucSP_MaChatLieu",
                table: "tDanhMucSP",
                column: "MaChatLieu");

            migrationBuilder.CreateIndex(
                name: "IX_tDanhMucSP_MaDT",
                table: "tDanhMucSP",
                column: "MaDT");

            migrationBuilder.CreateIndex(
                name: "IX_tDanhMucSP_MaHangSX",
                table: "tDanhMucSP",
                column: "MaHangSX");

            migrationBuilder.CreateIndex(
                name: "IX_tDanhMucSP_MaLoai",
                table: "tDanhMucSP",
                column: "MaLoai");

            migrationBuilder.CreateIndex(
                name: "IX_tDanhMucSP_MaNuocSX",
                table: "tDanhMucSP",
                column: "MaNuocSX");

            migrationBuilder.CreateIndex(
                name: "IX_tHoaDonBan_MaKhachHang",
                table: "tHoaDonBan",
                column: "MaKhachHang");

            migrationBuilder.CreateIndex(
                name: "IX_tHoaDonBan_MaNhanVien",
                table: "tHoaDonBan",
                column: "MaNhanVien");

            migrationBuilder.CreateIndex(
                name: "IX_tKhachHang_username",
                table: "tKhachHang",
                column: "username");

            migrationBuilder.CreateIndex(
                name: "IX_tNhanVien_username",
                table: "tNhanVien",
                column: "username");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tAnhChiTietSP");

            migrationBuilder.DropTable(
                name: "tAnhSP");

            migrationBuilder.DropTable(
                name: "tChiTietHDB");

            migrationBuilder.DropTable(
                name: "tChiTietSanPham");

            migrationBuilder.DropTable(
                name: "tHoaDonBan");

            migrationBuilder.DropTable(
                name: "tDanhMucSP");

            migrationBuilder.DropTable(
                name: "tKichThuoc");

            migrationBuilder.DropTable(
                name: "tMauSac");

            migrationBuilder.DropTable(
                name: "tKhachHang");

            migrationBuilder.DropTable(
                name: "tNhanVien");

            migrationBuilder.DropTable(
                name: "tChatLieu");

            migrationBuilder.DropTable(
                name: "tHangSX");

            migrationBuilder.DropTable(
                name: "tLoaiDT");

            migrationBuilder.DropTable(
                name: "tLoaiSP");

            migrationBuilder.DropTable(
                name: "tQuocGia");

            migrationBuilder.DropTable(
                name: "tUser");
        }
    }
}
