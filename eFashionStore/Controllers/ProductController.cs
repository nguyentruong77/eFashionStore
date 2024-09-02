using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using eFashionStore.Models;
using PagedList;

namespace eFashionStore.Controllers
{
    public class ProductController : Controller
    {
        eFashionStoreDataContext da = new eFashionStoreDataContext();
        // GET: HangHoa
        public ActionResult Index(string id, int? page)
        {
            var pageSize = 3;
            var pageNumber = page ?? 1; // default page
            var loaiSPs = da.LoaiSPs.ToList();
            var sanPhams = string.IsNullOrEmpty(id)
                ? da.SanPhams.ToList()
                : da.SanPhams.Where(p => p.MaLoai == id).ToList();

            var model = new ProductModel
            {
                LoaiSPs = loaiSPs,
                SanPhams = sanPhams.ToPagedList(pageNumber, pageSize)
            };

            return View(model);
        }

        public ActionResult Search(string key, int? page)
        {
            var loaiSPs = da.LoaiSPs.ToList();
            var sanPhams = string.IsNullOrEmpty(key)
            ? da.SanPhams.ToList()
                : da.SanPhams.Where(p => p.TenSP.Contains(key)).ToList();

            var model = new ProductModel
            {
                LoaiSPs = loaiSPs,
                SanPhams = sanPhams.ToPagedList(1, 999)
            };

            return View("Index", model);
        }
    }
}