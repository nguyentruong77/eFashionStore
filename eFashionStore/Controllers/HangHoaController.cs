using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eFashionStore.Data;

namespace eFashionStore.Controllers
{
    public class HangHoaController : Controller
    {
        eFashionStoreDataContext da = new eFashionStoreDataContext();
        // GET: HangHoa
        public ActionResult Index()
        {
            List<SanPham> ds = da.SanPhams.OrderByDescending(p => p.MaSP).ToList();
            return View(ds);
        }
    }
}