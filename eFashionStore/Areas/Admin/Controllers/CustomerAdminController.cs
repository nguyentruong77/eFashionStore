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
    public class CustomerAdminController : Controller
    {
        eFashionStoreDataContext da = new eFashionStoreDataContext();
        // GET: Admin/CustommerAdmin
        public ActionResult ListCus(int? page)
        {
            //string NameAdmin = Session["USERACCOUNT"].ToString();
            int pageSize = 10;
            List<NguoiDung> nd = da.NguoiDungs.Where(x => x.IsAdmin == false).ToList();
            int pageNumber = (page ?? 1);
            IPagedList<NguoiDung> pagedCus = nd.ToPagedList(pageNumber, pageSize);
            return View(pagedCus);
        }
        public ActionResult UpdateCus(int id)
        {
            try
            {
                NguoiDung nd = da.NguoiDungs.FirstOrDefault(x => x.UserID.Equals(id));
                return View(nd);
            }
            catch
            {
                return RedirectToAction("Error404", "HomeAdmin");
            }
        }
        [HttpPost]
        public ActionResult UpdateCus(NguoiDung nd)
        {
            try
            {
                List<NguoiDung> lnd = da.NguoiDungs.ToList();
                var ndg = da.NguoiDungs.FirstOrDefault(kh => kh.UserID == nd.UserID);
                lnd.Remove(ndg);
                var checkEmail = lnd.FirstOrDefault(x => x.Email == nd.Email);
                var checkSDT = lnd.FirstOrDefault(x => x.SDT == nd.SDT);
                if (checkEmail != null && checkSDT == null)
                {
                    ViewBag.EmailError = "Email đã tồn tại!";
                    return View();
                }
                else if (checkEmail == null && checkSDT != null)
                {
                    ViewBag.SDTError = "Số điện thoại đã tồn tại!";
                    return View();
                }
                else if (checkEmail != null && checkSDT != null)
                {
                    ViewBag.EmailError = "Email đã tồn tại!";
                    ViewBag.SDTError = "Số điện thoại đã tồn tại!";
                    return View();
                }
                else
                {
                    ndg.HoTen = nd.HoTen;
                    ndg.Email = nd.Email;
                    ndg.DiaChi = nd.DiaChi;
                    ndg.SDT = nd.SDT;
                    da.SubmitChanges();
                    return RedirectToAction("ListCus", "CustomerAdmin");
                }
            }
            catch
            {
                return RedirectToAction("Error404", "HomeAdmin");
            }
        }
        public ActionResult ListOrder(int? page)
        {
            int pageSize = 10;
            List<HoaDon> od = da.HoaDons.ToList();
            int pageNumber = (page ?? 1);
            IPagedList<HoaDon> pagedOD = od.ToPagedList(pageNumber, pageSize);
            return View(pagedOD);
        }
        public ActionResult DetailOrder(string id)
        {
            List<ChiTietHoaDon> cthd = da.ChiTietHoaDons.Where(x => x.MaHD.Equals(id)).ToList();
            return View(cthd);
        }
        public ActionResult UpdateOrder(string id)
        {
            try
            {
                HoaDon hd = da.HoaDons.FirstOrDefault(x => x.MaHD.Equals(id));
                return View(hd);
            }
            catch
            {
                return RedirectToAction("Error404", "HomeAdmin");
            }
        }
        //[HttpPost]
        //public ActionResult UpdateCus(NguoiDung nd)
        //{
        //    try
        //    {
        //        List<NguoiDung> lnd = da.NguoiDungs.ToList();
        //        var ndg = da.NguoiDungs.FirstOrDefault(kh => kh.UserID == nd.UserID);
        //        lnd.Remove(ndg);
        //        var checkEmail = lnd.FirstOrDefault(x => x.Email == nd.Email);
        //        var checkSDT = lnd.FirstOrDefault(x => x.SDT == nd.SDT);
        //        if (checkEmail != null && checkSDT == null)
        //        {
        //            ViewBag.EmailError = "Email đã tồn tại!";
        //            return View();
        //        }
        //        else if (checkEmail == null && checkSDT != null)
        //        {
        //            ViewBag.SDTError = "Số điện thoại đã tồn tại!";
        //            return View();
        //        }
        //        else if (checkEmail != null && checkSDT != null)
        //        {
        //            ViewBag.EmailError = "Email đã tồn tại!";
        //            ViewBag.SDTError = "Số điện thoại đã tồn tại!";
        //            return View();
        //        }
        //        else
        //        {
        //            ndg.HoTen = nd.HoTen;
        //            ndg.Email = nd.Email;
        //            ndg.DiaChi = nd.DiaChi;
        //            ndg.SDT = nd.SDT;
        //            da.SubmitChanges();
        //            return RedirectToAction("ListCus", "CustomerAdmin");
        //        }
        //    }
        //    catch
        //    {
        //        return RedirectToAction("Error404", "HomeAdmin");
        //    }
        //}
        public ActionResult DelOrder(String id)
        {
            if (id == null)
            {
                return RedirectToAction("Error404", "HomeAdmin");
            }
            HoaDon hd  = da.HoaDons.FirstOrDefault(s => s.MaHD.Equals(id));
            
            if (hd == null)
            {
                return RedirectToAction("Error404", "HomeAdmin");
            }

            return View(hd);
        }
        [HttpPost]
        public ActionResult DelOrder(String id, FormCollection collection)
        {
            if (id == null)
            {
                return RedirectToAction("Error404", "HomeAdmin");
            }
            HoaDon hd = da.HoaDons.FirstOrDefault(s => s.MaHD.Equals(id));
            if (hd == null)
            {
                return RedirectToAction("Error404", "HomeAdmin");
            }    
            try
            {
                List<ChiTietHoaDon> cthd = da.ChiTietHoaDons.Where(s => s.MaHD.Equals(hd.MaHD)).ToList();
                foreach (ChiTietHoaDon i in cthd)
                {
                    da.ChiTietHoaDons.DeleteOnSubmit(i);
                    da.SubmitChanges();
                }
                da.HoaDons.DeleteOnSubmit(hd);
                da.SubmitChanges();
                return RedirectToAction("ListOrder", "CustomerAdmin");
            }
            catch
            {
                return View(hd);
            }
        }

    }
}