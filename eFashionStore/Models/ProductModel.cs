using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eFashionStore.Models
{
    public class ProductModel
    {
        public IEnumerable<LoaiSP> LoaiSPs { get; set; }
        public IPagedList<SanPham> SanPhams { get; set; }

        public SanPham ItemDetails { get; set; }
        public List<RateModel> Reviews { get; set; }
    }
}