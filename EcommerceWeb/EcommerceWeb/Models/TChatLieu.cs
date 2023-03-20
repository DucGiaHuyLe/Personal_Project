using System;
using System.Collections.Generic;

namespace EcommerceWeb.Models
{
    public partial class TChatLieu
    {
        public TChatLieu()
        {
            TDanhMucSps = new HashSet<TDanhMucSp>();
        }

        public int MaChatLieu { get; set; }
        public string? ChatLieu { get; set; }

        public virtual ICollection<TDanhMucSp> TDanhMucSps { get; set; }
    }
}
