using System.Web.Mvc;

namespace eFashionStore.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
        private string XuLyChuoi(string AccountNo)
        {
            if (AccountNo.Length() == 10)
                AccountNo = AccountNo.Trim().ToUpper();
            else
                AccountNo = "001A" + AccountNo.Trim().ToUpper();
            return AccountNo;
        }
    }
}