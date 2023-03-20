using System;
using System.Collections.Generic;

namespace EcommerceWeb.Models
{
    public partial class TAnhSp
    {
        public int MaSp { get; set; }
        public string TenFileAnh { get; set; } = null!;
        public string? Vitri { get; set; }

        public virtual TDanhMucSp MaSpNavigation { get; set; } = null!;
    }
}
