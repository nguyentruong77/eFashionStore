using eFashionStore.MailService;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace eFashionStore
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            //mail
            var mailSettings = new MailSettings
            {
                Mail = ConfigurationManager.AppSettings["MailSettings:Mail"],
                DisplayName = ConfigurationManager.AppSettings["MailSettings:DisplayName"],
                Password = ConfigurationManager.AppSettings["MailSettings:Password"],
                Host = ConfigurationManager.AppSettings["MailSettings:Host"],
                Port = int.TryParse(ConfigurationManager.AppSettings["MailSettings:Port"], out int port) ? port : 0,
                EnableSsl = bool.TryParse(ConfigurationManager.AppSettings["MailSettings:EnableSsl"], out bool enableSsl) ? enableSsl : false
            };

            // check value
            if (string.IsNullOrEmpty(mailSettings.Mail) ||
                string.IsNullOrEmpty(mailSettings.DisplayName) ||
                string.IsNullOrEmpty(mailSettings.Password) ||
                string.IsNullOrEmpty(mailSettings.Host) ||
                mailSettings.Port <= 0)
            {
                throw new ArgumentException("Mail settings cannot be null or empty.");
            }

            DependencyResolver.SetResolver(new UnityDependencyResolver(mailSettings));
        }

        public class UnityDependencyResolver : IDependencyResolver
        {
            private readonly MailSettings _mailSettings;

            public UnityDependencyResolver(MailSettings mailSettings)
            {
                _mailSettings = mailSettings;
            }

            public object GetService(Type serviceType)
            {
                if (serviceType == typeof(SendMailService))
                {
                    return new SendMailService(_mailSettings);
                }

                return null;
            }

            public IEnumerable<object> GetServices(Type serviceType)
            {
                return new List<object>();
            }
        }
    }
}
