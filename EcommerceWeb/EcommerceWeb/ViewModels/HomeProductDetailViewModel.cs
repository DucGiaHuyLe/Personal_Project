﻿using EcommerceWeb.Models;

namespace EcommerceWeb.ViewModels
{
    public class HomeProductDetailViewModel
    {
        public TDanhMucSp danhMucSp { get; set; }   
        public List<TAnhSp> anhSps { get; set; }
    }
}
