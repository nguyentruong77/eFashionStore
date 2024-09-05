using eFashionStore.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace eFashionStore.Controllers
{
    public class CartController : Controller
    {
        private readonly eFashionStoreDataContext _context = new eFashionStoreDataContext();
        public ActionResult Index()
        {
            return View();
        }

        private int? GetUserId()
        {
            if (Session != null && Session["UserId"] is int userId)
            {
                return userId;
            }

            return null;
        }

        public List<CartModel> GetListCarts()
        {
            var sessionCart = Session["CartModel"] as string;
            List<CartModel> carts;
            if (!string.IsNullOrEmpty(sessionCart))
            {
                carts = JsonConvert.DeserializeObject<List<CartModel>>(sessionCart);
            }
            else
            {
                carts = new List<CartModel>();
                Session["CartModel"] = JsonConvert.SerializeObject(carts);
            }
            return carts;
        }

        public ActionResult ListCarts()
        {
            var userId = GetUserId();
            var nguoiDung = _context.NguoiDungs.FirstOrDefault(u => u.UserID == userId);
            ViewBag.HoTen = nguoiDung != null ? nguoiDung.HoTen : "";
            ViewBag.DiaChi = nguoiDung != null ? nguoiDung.DiaChi : "";
            ViewBag.SDT = nguoiDung != null ? nguoiDung.SDT : "";
            ViewBag.Email = nguoiDung != null ? nguoiDung.Email : "";
            ViewBag.DiaChi = nguoiDung != null ? nguoiDung.DiaChi : "";

            if (userId != null)
            {
                var cartItems = _context.GioHangs
                    .Where(g => g.MaKH == userId)
                    .SelectMany(g => _context.ChiTietGioHangs
                        .Where(c => c.MaGH == g.MaGH)
                        .Join(_context.SanPhams, c => c.MaSP, s => s.MaSP, (c, s) => new CartModel
                        {
                            MaSp = s.MaSP,
                            TenSp = s.TenSP,
                            Gia = s.Gia,
                            SoLuong = c.SoLuong ?? 0,
                            Hinh = s.Hinh,
                            GiamGia = s.GiamGia
                        }))
                    .ToList();

                ViewBag.CountProduct = cartItems.Sum(c => c.SoLuong);
                ViewBag.SubTotal = cartItems.Sum(c => c.Gia * c.SoLuong);
                decimal discount = 0;
                foreach (var cart in cartItems)
                {
                    discount += (cart.GiamGia / 100.0m) * (cart.Gia * cart.SoLuong);
                }

                ViewBag.Discount = discount;
                ViewBag.Total = ViewBag.SubTotal - ViewBag.Discount;

                ViewBag.Coupon = TempData["Coupon"];
                ViewBag.GiamGiaCoupon = TempData["GiamGiaCoupon"];
                ViewBag.Message = TempData["Message"];

                return View(cartItems);
            }

            return View(new List<CartModel>());
        }

        
        [HttpPost]
        public ActionResult ListCarts(string couponCode)
        {
            decimal discountCoupon = 0;
            if (!string.IsNullOrEmpty(couponCode))
            {
                var coupon = _context.Coupons.FirstOrDefault(u => u.MaCoupon == couponCode);
                if (coupon != null)
                {
                    ViewBag.Coupon = coupon.MaCoupon;
                    ViewBag.GiamGiaCoupon = coupon.GiamGia;
                    ViewBag.Message = "";
                    discountCoupon = coupon.GiamGia;
                    //save session
                    Session["discountCoupon"] = coupon.GiamGia;
                }
                else
                {
                    ViewBag.Coupon = "";
                    ViewBag.GiamGiaCoupon = 0;
                    ViewBag.Message = "Invalid coupon code.";
                }
            }
            else
            {
                ViewBag.Coupon = "";
                ViewBag.GiamGiaCoupon = 0;
                ViewBag.Message = "Please enter a coupon code.";
            }

            var userId = GetUserId();
            var nguoiDung = _context.NguoiDungs.FirstOrDefault(u => u.UserID == userId);
            ViewBag.HoTen = nguoiDung != null ? nguoiDung.HoTen : "";
            ViewBag.DiaChi = nguoiDung != null ? nguoiDung.DiaChi : "";
            ViewBag.SDT = nguoiDung != null ? nguoiDung.SDT : "";
            ViewBag.Email = nguoiDung != null ? nguoiDung.Email : "";
            ViewBag.DiaChi = nguoiDung != null ? nguoiDung.DiaChi : "";

            if (userId != null)
            {
                var cartItems = _context.GioHangs
                    .Where(g => g.MaKH == userId)
                    .SelectMany(g => _context.ChiTietGioHangs
                        .Where(c => c.MaGH == g.MaGH)
                        .Join(_context.SanPhams, c => c.MaSP, s => s.MaSP, (c, s) => new CartModel
                        {
                            MaSp = s.MaSP,
                            TenSp = s.TenSP,
                            Gia = s.Gia,
                            SoLuong = c.SoLuong ?? 0,
                            Hinh = s.Hinh,
                            GiamGia = s.GiamGia
                        }))
                    .ToList();

                ViewBag.CountProduct = cartItems.Sum(c => c.SoLuong);
                ViewBag.SubTotal = cartItems.Sum(c => c.Gia * c.SoLuong);
                decimal discount = 0;
                foreach (var cart in cartItems)
                {
                    discount += (cart.GiamGia / 100.0m) * (cart.Gia * cart.SoLuong);
                }

                ViewBag.Discount = discount / (1 - (discountCoupon / 100));
                ViewBag.Total = (ViewBag.SubTotal - ViewBag.Discount);

                return View("ListCarts", cartItems);
            }

            return View("ListCarts", new List<CartModel>());
        }

        public ActionResult AddToCart(string id, int quantity)
        {
            if (quantity <= 0)
            {
                return RedirectToAction("Index");
            }

            var product = _context.SanPhams.FirstOrDefault(s => s.MaSP == id);

            if (product != null)
            {
                List<CartModel> carts = GetListCarts();
                CartModel existingProduct = carts.FirstOrDefault(s => s.MaSp == id);

                if (existingProduct != null)
                {
                    existingProduct.SoLuong += quantity;
                }
                else
                {
                    CartModel newProduct = new CartModel
                    {
                        MaSp = product.MaSP,
                        TenSp = product.TenSP,
                        Gia = product.Gia,
                        SoLuong = quantity,
                        Hinh = product.Hinh,
                        GiamGia = product.GiamGia
                    };
                    carts.Add(newProduct);
                }

                var jsonCart = JsonConvert.SerializeObject(carts);
                HttpContext.Session["CartModel"] = jsonCart;

                var maKh = GetUserId();
                if (maKh != null)
                {
                    var gioHang = _context.GioHangs.FirstOrDefault(g => g.MaKH == maKh);

                    if (gioHang == null)
                    {
                        gioHang = new GioHang { MaKH = maKh };
                        _context.GioHangs.InsertOnSubmit(gioHang);
                        _context.SubmitChanges();
                    }

                    var chiTietGioHang = _context.ChiTietGioHangs.FirstOrDefault(c => c.MaGH == gioHang.MaGH && c.MaSP == id);
                    if (chiTietGioHang != null)
                    {
                        chiTietGioHang.SoLuong += quantity;
                    }
                    else
                    {
                        chiTietGioHang = new ChiTietGioHang
                        {
                            MaGH = gioHang.MaGH,
                            MaSP = id,
                            SoLuong = quantity
                        };
                        _context.ChiTietGioHangs.InsertOnSubmit(chiTietGioHang);
                    }

                    _context.SubmitChanges();
                }
            }

            return RedirectToAction("ListCarts");
        }


        private void LoadCartFromDatabase()
        {
            var userId = GetUserId();

            if (userId != null)
            {
                // Load database
                var cartItemsFromDb = (from gioHang in _context.GioHangs
                                       join chiTietGioHang in _context.ChiTietGioHangs on gioHang.MaGH equals chiTietGioHang.MaGH
                                       join sanPham in _context.SanPhams on chiTietGioHang.MaSP equals sanPham.MaSP
                                       where gioHang.MaKH == userId
                                       select new CartModel
                                       {
                                           MaSp = sanPham.MaSP,
                                           TenSp = sanPham.TenSP,
                                           Gia = sanPham.Gia,
                                           SoLuong = chiTietGioHang.SoLuong ?? 0,
                                           Hinh = sanPham.Hinh,
                                           GiamGia = sanPham.GiamGia
                                       }).ToList();

                // Load session
                var cartItemsFromSession = GetListCarts();

                // Shuffer session, db
                foreach (var sessionItem in cartItemsFromSession)
                {
                    var dbItem = cartItemsFromDb.FirstOrDefault(c => c.MaSp == sessionItem.MaSp);
                    if (dbItem != null)
                    {
                        dbItem.SoLuong += sessionItem.SoLuong;
                    }
                    else
                    {
                        cartItemsFromDb.Add(sessionItem);
                    }
                }
                HttpContext.Session["CartModel"] = JsonConvert.SerializeObject(cartItemsFromDb);
            }
        }


        [HttpGet]
        public ActionResult Delete(string id)
        {
            var userId = GetUserId();

            if (userId != null)
            {
                var cart = _context.GioHangs
                    .FirstOrDefault(g => g.MaKH == userId);

                if (cart != null)
                {
                    var cartItem = _context.ChiTietGioHangs
                        .FirstOrDefault(c => c.MaGH == cart.MaGH && c.MaSP == id);

                    if (cartItem != null)
                    {
                        _context.ChiTietGioHangs.DeleteOnSubmit(cartItem);
                        _context.SubmitChanges();
                    }
                }
            }
            return RedirectToAction("ListCarts", "Cart");
        }

        public string GenerateMaHd()
        {

            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            var result = new string(
                Enumerable.Repeat(chars, 6)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());
            return result;
        }

        
        public ActionResult OrderConfirmation()
        {
            return View();
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult OrderConfirmation(string paymentMethod)
        {
            if (!User.Identity.IsAuthenticated)
            {
                string returnUrl = Url.Action("Order", "Cart");
                return RedirectToAction("DangNhap", "Account", new { returnUrl });
            }

            List<CartModel> carts = GetListCarts();
            int? userId = GetUserId();

            if (carts == null || carts.Count == 0 || !userId.HasValue)
            {
                return RedirectToAction("ListCarts");
            }

            decimal discountCoupon = 0;
            if (Session != null && Session["discountCoupon"] is int discountCP)
            {
                discountCoupon = discountCP;
            }

            decimal subTotal = carts.Sum(s => s.SoLuong * s.Gia);
            decimal discount = carts.Sum(s => (s.GiamGia / 100.0m) * (s.Gia * s.SoLuong));
            decimal total = (subTotal - discount) * (1 - (discountCoupon / 100));

            var khachHang = _context.NguoiDungs.FirstOrDefault(u => u.UserID == userId);
            if (khachHang == null)
            {
                return RedirectToAction("ListCarts");
            }

            bool tinhTrangTt = paymentMethod == "Online";
            var order = new HoaDon
            {
                MaHD = GenerateMaHd(),
                MaKH = userId.Value,
                NgayDatHang = DateTime.Now,
                TongGiaTri = total,
                DiaChiGiaoHang = khachHang.DiaChi,
                TrangThaiTT = tinhTrangTt,
                TrangThaiDH = 0
            };

            _context.HoaDons.InsertOnSubmit(order);
            _context.SubmitChanges();

            foreach (var item in carts)
            {
                var orderDetail = new ChiTietHoaDon
                {
                    MaHD = order.MaHD,
                    MaSP = item.MaSp,
                    SoLuongDatHang = item.SoLuong,
                    DonGia = ((item.Gia * (1 - (item.GiamGia / 100))) * (1 - (discountCoupon / 100)))*item.SoLuong
                };
                _context.ChiTietHoaDons.InsertOnSubmit(orderDetail);

                var product = _context.SanPhams.FirstOrDefault(p => p.MaSP == item.MaSp);
                if (product != null)
                {
                    product.SoLuong -= (short)item.SoLuong;
                    if (product.SoLuong < 0)
                    {
                        product.SoLuong = 0;
                    }
                }
            }

            _context.SubmitChanges();

            var gioHang = _context.GioHangs.FirstOrDefault(g => g.MaKH == userId);
            if (gioHang != null)
            {
                var chiTietGioHangs = _context.ChiTietGioHangs.Where(c => c.MaGH == gioHang.MaGH).ToList();
                _context.ChiTietGioHangs.DeleteAllOnSubmit(chiTietGioHangs);
                _context.GioHangs.DeleteOnSubmit(gioHang);
                _context.SubmitChanges();
            }

            HttpContext.Session.Remove("CartModel");

            return RedirectToAction("ListCarts");
        }
  
        
    }
}