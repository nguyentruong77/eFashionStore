using eFashionStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eFashionStore.Controllers
{
    public class HomeController : Controller
    {
        eFashionStoreDataContext da = new eFashionStoreDataContext();
        // GET: HangHoa
        public ActionResult Index()
        {
            List<SanPham> ds = da.SanPhams.OrderByDescending(p => p.MaSP).ToList();
            return View(ds);
        }

        public ActionResult Contact()
        {
            return View();
        }
    }
}