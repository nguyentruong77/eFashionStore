using eFashionStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using System.Web.UI;

namespace eFashionStore.Areas.Admin.Controllers
{
    public class HomeAdminController : Controller
    {
        eFashionStoreDataContext da = new eFashionStoreDataContext();
        // GET: Admin/Home
        public ActionResult Index(int ?page)
        {
            var username = Session["UserName"] as string;
            NguoiDung nd = da.NguoiDungs.FirstOrDefault(s => s.TenTaiKhoan.Equals(username) && s.IsAdmin == true);
            if (nd != null)
            {
                int pageSize = 10; // Số sản phẩm trên mỗi trang
                List<SanPham> sp = da.SanPhams.ToList();
                int pageNumber = (page ?? 1); // Trang mặc định là 1 nếu không có trang được chỉ định
                IPagedList<SanPham> pagedProducts =  sp.ToPagedList(pageNumber, pageSize);
                return View(pagedProducts);
            }
            else
                return RedirectToAction("DenyAccess", "HomeAdmin");
        }
        public ActionResult Error404()
        {
            return View();
        }
        public ActionResult DenyAccess()
        {
            return View();
        }
    }
}