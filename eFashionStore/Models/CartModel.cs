using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eFashionStore.Models
{
    public class CartModel
    {
        public string MaSp { get; set; }
        public string TenSp { get; set; }
        public decimal Gia { get; set; }
        public int SoLuong { get; set; }
        public string Hinh { get; set; }
        public decimal GiamGia { get; set; }

        public string HoTen { get; set; }
        public string SDT { get; set; }
        public string DiaChi { get; set; }
        public string Email { get; set; }
        public string DiaChiGiaoHang { get; set; }
        public string PhuongThucThanhToan { get; set; }
    }
}