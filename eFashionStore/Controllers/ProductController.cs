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

        public ActionResult Details(string id)
        {
            var product = da.SanPhams.FirstOrDefault(p => p.MaSP == id);
            if (product == null)
            {
                return null;
            }

            var reviews = da.DanhGias
                .Where(r => r.MaSP == id)
                .Join(da.NguoiDungs,
                      r => r.MaKH,
                      u => u.UserID,
                      (r, u) => new RateModel
                      {
                          HoTen = u.HoTen,
                          SoSao = r.SoSao,
                          BinhLuan = r.BinhLuan
                      })
                .ToList();

            var viewModel = new ProductModel
            {
                ItemDetails = product,
                Reviews = reviews
            };

            return View(viewModel);
        }

        private int? GetUserId()
        {
            if (Session != null && Session["UserId"] is int userId)
            {
                return userId;
            }

            return null;
        }

        [HttpPost]
        [Authorize]
        public ActionResult SubmitReview(string MaSp, int SoSao, string BinhLuan)
        {
            int? userId = GetUserId();
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var existingReview = da.DanhGias.FirstOrDefault(r => r.MaSP == MaSp && r.MaKH == userId.Value);

            if (existingReview != null)
            {
                existingReview.SoSao = SoSao;
                existingReview.BinhLuan = BinhLuan;
            }
            else
            {
                var review = new DanhGia
                {
                    MaSP = MaSp,
                    MaKH = userId.Value,
                    SoSao = SoSao,
                    BinhLuan = BinhLuan
                };
                da.DanhGias.InsertOnSubmit(review);
            }

            da.SubmitChanges();
            return RedirectToAction("Details", new { id = MaSp.Trim() });
        }
    }
}