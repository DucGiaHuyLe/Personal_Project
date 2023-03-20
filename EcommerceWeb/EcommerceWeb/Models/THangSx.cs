using System;
using System.Collections.Generic;

namespace EcommerceWeb.Models
{
    public partial class THangSx
    {
        public THangSx()
        {
            TDanhMucSps = new HashSet<TDanhMucSp>();
        }

        public int MaHangSx { get; set; }
        public string? HangSx { get; set; }
        public string? MaNuocThuongHieu { get; set; }

        public virtual ICollection<TDanhMucSp> TDanhMucSps { get; set; }
    }
}
