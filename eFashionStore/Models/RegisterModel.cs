﻿using System.ComponentModel.DataAnnotations;

namespace eFashionStore.Models
{
    public class RegisterModel
    {
        public string HoTen { get; set; }
        public string Email { get; set; }
        public string DiaChi { get; set; }
        public string Sdt { get; set; }
        public string TenTaiKhoan { get; set; }
        public string MatKhau { get; set; }
        public bool IsAdmin { get; set; }
    }
}
