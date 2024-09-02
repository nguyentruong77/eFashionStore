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
            List<HangHoa> ds = da.HangHoas.OrderByDescending(p => p.MaHH).ToList();
            return View(ds);
        }
    }
}