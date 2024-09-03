using eFashionStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;

namespace eFashionStore.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        eFashionStoreDataContext da = new eFashionStoreDataContext();
        // GET: Admin/Home
        public ActionResult Index()
        {
            List<SanPham> ds = da.SanPhams.OrderByDescending(p => p.MaSP).ToList();
            return View(ds);
        }
    }
}