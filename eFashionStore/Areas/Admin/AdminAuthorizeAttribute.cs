using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eFashionStore.Models;

namespace eFashionStore.Areas.Admin
{
    public class AdminAuthorizeAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var username = httpContext.Session["UserName"] as string;

            if (username == null)
            {
                return false;
            }
            eFashionStoreDataContext da = new eFashionStoreDataContext();
            NguoiDung Admin = da.NguoiDungs.FirstOrDefault(s => s.TenTaiKhoan.Equals(username) && s.IsAdmin == true);
            if (Admin != null)
            {
                return true;
            }
            else
                return false;
        }
    }
}