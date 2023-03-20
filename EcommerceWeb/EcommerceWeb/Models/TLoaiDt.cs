using System;
using System.Collections.Generic;

namespace EcommerceWeb.Models
{
    public partial class TLoaiDt
    {
        public TLoaiDt()
        {
            TDanhMucSps = new HashSet<TDanhMucSp>();
        }

        public int MaDt { get; set; }
        public string? TenLoai { get; set; }

        public virtual ICollection<TDanhMucSp> TDanhMucSps { get; set; }
    }
}
