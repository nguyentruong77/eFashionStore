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
    }
}