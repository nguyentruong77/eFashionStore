using eFashionStore.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.WebPages.Html;

namespace eFashionStore.Areas.Admin.Controllers
{
    [AdminAuthorize]
    public class ProductAdminController : Controller
    {
        eFashionStoreDataContext da = new eFashionStoreDataContext();
        // GET: Admin/ProductAdmin
        public ActionResult CreatePR()
        {
            List<LoaiSP> tenloai = da.LoaiSPs.ToList();
            var tenLoaiList = tenloai.Select(p => new System.Web.Mvc.SelectListItem
            {
                Value = p.MaLoai.ToString(),
                Text = p.TenLoai
            }).ToList();
            ViewBag.tenLoaiList = tenLoaiList;
            return View();
        }
        [HttpPost]
        public ActionResult CreatePR(SanPham newSP)
        {
            try
            {
                SanPham sp = da.SanPhams.FirstOrDefault(s => s.MaSP.Equals(newSP.MaSP));
                if (sp == null)
                {
                    da.SanPhams.InsertOnSubmit(newSP);
                    da.SubmitChanges();
                    return RedirectToAction("Index", "HomeAdmin");
                }
                else
                {
                    ViewBag.CodeExist = "Product code already exists!";
                    return CreatePR();
                }    
            }
            catch
            {
                return RedirectToAction("Error404", "HomeAdmin");
            }
        }
    public ActionResult DetailPR(string id)
        {
            try
            {
                var DetailPR = da.SanPhams.FirstOrDefault(x => x.MaSP.Equals(id));
                return View(DetailPR);
            }
            catch
            {
                return RedirectToAction("Error404", "HomeAdmin");
            }
        }
        public ActionResult UpdatePR(string id)
        {
            try
            {
                var sp = da.SanPhams.FirstOrDefault(x => x.MaSP.Equals(id));
                List<LoaiSP> tenloai = da.LoaiSPs.ToList();
                SelectList tenLoaiList = new SelectList(tenloai, "MaLoai", "TenLoai", sp.MaLoai);
                ViewBag.tenLoaiList = tenLoaiList;
                return View(sp);
            }
            catch
            {
                return RedirectToAction("Error404", "HomeAdmin");
            }
        }
        [HttpPost]
        public ActionResult UpdatePR(SanPham upSP)
        {
            try
            {
                SanPham sp = da.SanPhams.FirstOrDefault(s => s.MaSP.Equals(upSP.MaSP));
                if (sp != null)
                {
                    sp.TenSP = upSP.MaSP;
                    sp.Hinh = upSP.Hinh;
                    sp.MoTa = upSP.MoTa;
                    sp.TonKho = upSP.TonKho;
                    sp.SoLuong = upSP.SoLuong;
                    sp.Gia = upSP.Gia;
                    sp.GiamGia = upSP.GiamGia;
                    sp.MaLoai = upSP.MaLoai;
                    da.SubmitChanges();
                }
                else
                    return RedirectToAction("Error404", "HomeAdmin");
                return RedirectToAction("Index", "HomeAdmin");
            }
            catch
            {
                return RedirectToAction("Error404", "HomeAdmin");
            }
        }
        public ActionResult DelPR(string id)
        {
            try
            {
                SanPham sp = da.SanPhams.FirstOrDefault(s => s.MaSP.Equals(id));
                return View(sp);
            }
            catch
            {
                return RedirectToAction("Error404", "HomeAdmin");
            }
        }
        [HttpPost]
        public ActionResult DelPR(string id, FormCollection collection)
        {
            if (id == null)
            {
                return RedirectToAction("Error404", "HomeAdmin");
            }
            SanPham sp = da.SanPhams.FirstOrDefault(s => s.MaSP.Equals(id));
            if (sp == null)
            {
                return RedirectToAction("Error404", "HomeAdmin");
            }
            try
            {
                List<ChiTietHoaDon> cthd = da.ChiTietHoaDons.Where(s => s.MaSP.Equals(sp.MaSP)).ToList();
                foreach (ChiTietHoaDon i in cthd)
                {
                    da.ChiTietHoaDons.DeleteOnSubmit(i);
                    da.SubmitChanges();
                }
                List<DanhGia> dg = da.DanhGias.Where(s => s.MaSP.Equals(sp.MaSP)).ToList();
                foreach (DanhGia i in dg)
                {
                    da.DanhGias.DeleteOnSubmit(i);
                    da.SubmitChanges();
                }
                da.SanPhams.DeleteOnSubmit(sp);
                da.SubmitChanges();
                return RedirectToAction("Index", "HomeAdmin");
            }
            catch
            {
                return View(sp);
            }
        }
        public ActionResult ListPT(int? page)
        {
            try
            {
                int pageSize = 10; // Số sản phẩm trên mỗi trang
                var ListPT = da.LoaiSPs.ToList();
                int pageNumber = (page ?? 1); // Trang mặc định là 1 nếu không có trang được chỉ định
                IPagedList<LoaiSP> pagedPC = ListPT.ToPagedList(pageNumber, pageSize);
                return View(pagedPC);
            }
            catch
            {
                return RedirectToAction("Error404", "HomeAdmin");
            }
        }
        public ActionResult CreatePT()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreatePT(LoaiSP newPT)
        {
            try
            {

                LoaiSP lsp = da.LoaiSPs.FirstOrDefault(s => s.MaLoai.Equals(newPT.MaLoai));
                if (lsp == null)
                {
                    da.LoaiSPs.InsertOnSubmit(newPT);
                    da.SubmitChanges();
                    return RedirectToAction("ListPT", "ProductAdmin");
                }
                else
                {
                    ViewBag.CodeExist = "Product type code already exists!";
                    return CreatePT();
                }
            }
            catch
            {
                return RedirectToAction("Error404", "HomeAdmin");
            }
        }
        public ActionResult UpdatePT(string id)
        {
            LoaiSP lsp = da.LoaiSPs.FirstOrDefault(s => s.MaLoai.Equals(id));
            return View(lsp);
        }
        [HttpPost]
        public ActionResult UpdatePT(LoaiSP newPT)
        {
            try
            {
                LoaiSP lsp = da.LoaiSPs.FirstOrDefault(s => s.MaLoai.Equals(newPT.MaLoai));
                if (lsp != null)
                {
                    lsp.TenLoai = newPT.TenLoai;
                    lsp.Hinh = newPT.Hinh;
                    da.SubmitChanges();
                }
                else
                    return RedirectToAction("Error404", "HomeAdmin");
                return RedirectToAction("ListPT", "ProductAdmin");
            }
            catch
            {
                return RedirectToAction("Error404", "HomeAdmin");
            }
        }
        public ActionResult DelPT(string id)
        {
            try
            {
                LoaiSP lsp = da.LoaiSPs.FirstOrDefault(s => s.MaLoai.Equals(id));
                return View(lsp);
            }
            catch
            {
                return RedirectToAction("Error404", "HomeAdmin");
            }
        }
        [HttpPost]
        public ActionResult DelPT(string id, FormCollection collection)
        {
            if (id == null)
            {
                return RedirectToAction("Error404", "HomeAdmin");
            }
            LoaiSP lsp = da.LoaiSPs.FirstOrDefault(s => s.MaLoai.Equals(id));
            if (lsp == null)
            {
                return RedirectToAction("Error404", "HomeAdmin");
            }
            try
            {
                List<SanPham> sp = da.SanPhams.Where(s => s.MaLoai.Equals(lsp.MaLoai)).ToList();
                foreach (SanPham i in sp)
                {
                    i.MaLoai = null;
                    da.SubmitChanges();
                }
                da.LoaiSPs.DeleteOnSubmit(lsp);
                da.SubmitChanges();
                return RedirectToAction("ListPT", "CustomerAdmin");
            }
            catch
            {
                return View(lsp);
            }
        }
        public ActionResult ListCP(int? page)
        {
            try
            {
                int pageSize = 10; // Số sản phẩm trên mỗi trang
                var ListCP = da.Coupons.ToList();
                int pageNumber = (page ?? 1); // Trang mặc định là 1 nếu không có trang được chỉ định
                IPagedList<Coupon> pagedCP = ListCP.ToPagedList(pageNumber, pageSize);
                return View(pagedCP);
            }
            catch
            {
                return RedirectToAction("Error404", "HomeAdmin");
            }
        }
        public ActionResult CreateCP()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateCP(Coupon newCT)
        {
            try
            {

                Coupon cp = da.Coupons.FirstOrDefault(s => s.MaCoupon.Equals(newCT.MaCoupon));
                if (cp == null)
                {
                    da.Coupons.InsertOnSubmit(newCT);
                    da.SubmitChanges();
                    return RedirectToAction("ListCP", "ProductAdmin");
                }
                else
                {
                    ViewBag.CodeExist = "Coupon code already exists!";
                    return CreateCP();
                }
            }
            catch
            {
                return RedirectToAction("Error404", "HomeAdmin");
            }
        }
        public ActionResult UpdateCP(string id)
        {
            Coupon cp = da.Coupons.FirstOrDefault(s => s.MaCoupon.Equals(id));
            return View(cp);
        }
        [HttpPost]
        public ActionResult UpdateCP(Coupon newCP)
        {
            try
            {
                Coupon cp = da.Coupons.FirstOrDefault(s => s.MaCoupon.Equals(newCP.MaCoupon));
                if (cp != null)
                {
                    cp.GiamGia = newCP.GiamGia;
                    da.SubmitChanges();
                }
                else
                    return RedirectToAction("Error404", "HomeAdmin");
                return RedirectToAction("ListCP", "ProductAdmin");
            }
            catch
            {
                return RedirectToAction("Error404", "HomeAdmin");
            }
        }
        public ActionResult DelCP(string id)
        {
            try
            {
                Coupon cp = da.Coupons.FirstOrDefault(s => s.MaCoupon.Equals(id));
                return View(cp);
            }
            catch
            {
                return RedirectToAction("Error404", "HomeAdmin");
            }
        }
        [HttpPost]
        public ActionResult DelCP(string id, FormCollection collection)
        {
            if (id == null)
            {
                return RedirectToAction("Error404", "HomeAdmin");
            }
            Coupon cp = da.Coupons.FirstOrDefault(s => s.MaCoupon.Equals(id));
            if (cp == null)
            {
                return RedirectToAction("Error404", "HomeAdmin");
            }
            try
            {
                List<HoaDon> hd = da.HoaDons.Where(s => s.MaCoupon.Equals(cp.MaCoupon)).ToList();
                foreach (HoaDon i in hd)
                {
                    i.MaCoupon = null;
                    da.SubmitChanges();
                }
                da.Coupons.DeleteOnSubmit(cp);
                da.SubmitChanges();
                return RedirectToAction("ListCP", "ProductAdmin");
            }
            catch
            {
                return View(cp);
            }
        }
    }
}