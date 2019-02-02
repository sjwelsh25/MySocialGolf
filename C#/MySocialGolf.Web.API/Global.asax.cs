using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.WebPages;
using TwitterBootstrapMVC;
using System.Web.Http;
using System.Web.Helpers;
using System.Security.Claims;
using MySocialGolf.Manager;

namespace MySocialGolf.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            DisplayModeProvider.Instance.Modes.Insert(0, new DefaultDisplayMode("iPhone")
            {
                ContextCondition = (context => context.GetOverriddenUserAgent().IndexOf
                    ("iPhone", StringComparison.OrdinalIgnoreCase) >= 0)
            });

            Bootstrap.Configure();
        }

        void Session_Start(object sender, EventArgs e)
        {
            // your code here, it will be executed upon session start
            // if the user has a cookie and has previously signed in then log the Login Event and set the Session values
            if (Request.IsAuthenticated)
            {
                new MSGUserManager().LogInUser(User.Identity.Name);
            }

        }
    }
}
