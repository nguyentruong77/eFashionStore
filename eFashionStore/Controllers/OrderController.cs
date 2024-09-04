using eFashionStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eFashionStore.Controllers
{
    public class OrderController : Controller
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
        [Authorize]
        public ActionResult Order()
        {
            int? userId = GetUserId();
            if (userId == null)
            {
                return RedirectToAction("DangNhap", "Account");
            }

            var order = (from hd in _context.HoaDons
                          where hd.MaKH == userId.Value
                          orderby hd.NgayDatHang descending
                          select new OrderModel
                          {
                              MaHd = hd.MaHD,
                              NgayDatHang = hd.NgayDatHang,
                              TongGiaTri = hd.TongGiaTri,
                              DiaChiGiaoHang = hd.DiaChiGiaoHang,
                              TrangThaiThanhToan = hd.TrangThaiTT,
                              TrangThaiDonHang = hd.TrangThaiDH,
                              NgayNhanHang = hd.NgayNhanHang
                          }).ToList();

            return View(order);
        }

        [Authorize]
        public ActionResult OrderDetails(string maHd)
        {
            int? userId = GetUserId();
            if (userId == null)
            {
                return RedirectToAction("DangNhap", "Account");
            }

            var orderDetails = (from ct in _context.ChiTietHoaDons
                                 join sp in _context.SanPhams on ct.MaSP equals sp.MaSP
                                 where ct.MaHD == maHd
                                 select new OrderModel
                                 {
                                     MaHd = ct.MaHD,
                                     MaSp = sp.MaSP,
                                     TenSp = sp.TenSP,
                                     SoLuongDatHang = ct.SoLuongDatHang,
                                     DonGia = ct.DonGia
                                 }).ToList();

            return View(orderDetails);
        }
    }
}