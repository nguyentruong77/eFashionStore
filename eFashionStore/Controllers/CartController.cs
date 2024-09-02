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
            var userIdString = Session["UserId"] as string;

            if (int.TryParse(userIdString, out int userId))
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
            LoadCartFromDatabase();

            var sessionCart = Session["CartModel"] as string;
            List<CartModel> carts = string.IsNullOrEmpty(sessionCart)
                ? new List<CartModel>()
                : JsonConvert.DeserializeObject<List<CartModel>>(sessionCart);

            ViewBag.CountProduct = carts.Sum(s => s.SoLuong);
            decimal subTotal = carts.Sum(s => s.SoLuong * s.Gia);
            ViewBag.SubTotal = subTotal;

            decimal discount = 0;
            foreach (var cart in carts)
            {
                discount += (cart.GiamGia / 100.0m) * (cart.Gia * cart.SoLuong);
            }

            ViewBag.Discount = discount;
            ViewBag.Total = subTotal - discount;

            return View(carts);
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

                var cartItemsFromSession = HttpContext.Session["CartModel"] as List<CartModel> ?? new List<CartModel>();
                // Nối session và giỏ
                foreach (var sessionItem in cartItemsFromSession)
                {
                    var dbItem = cartItemsFromDb.FirstOrDefault(c => c.MaSp == sessionItem.MaSp);
                    if (dbItem != null)
                    {
                        dbItem.SoLuong += (sessionItem.SoLuong - 1);
                    }
                    else
                    {
                        cartItemsFromDb.Add(sessionItem);
                    }
                }
                HttpContext.Session["CartModel"] = cartItemsFromDb;
            }
        }

        public ActionResult Delete(string id)
        {
            var carts = GetListCarts();
            var productToRemove = carts.FirstOrDefault(p => p.MaSp == id);
            if (productToRemove != null)
            {
                carts.Remove(productToRemove);
                HttpContext.Session["CartModel"] = carts;

                var userId = GetUserId();
                if (userId != null)
                {
                    var gioHang = _context.GioHangs.FirstOrDefault(g => g.MaKH == userId);
                    if (gioHang != null)
                    {
                        var chiTietGioHang = _context.ChiTietGioHangs.FirstOrDefault(c => c.MaGH == gioHang.MaGH && c.MaSP == id);
                        if (chiTietGioHang != null)
                        {
                            _context.ChiTietGioHangs.DeleteOnSubmit(chiTietGioHang);
                            _context.SubmitChanges();
                        }
                    }
                }
            }

            return RedirectToAction("ListCarts");
        } 
    }
}