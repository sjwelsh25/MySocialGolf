using Microsoft.Owin;
using MySocialGolf.Web.API.App_Start;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

[assembly: OwinStartup(typeof(MySocialGolf.Web.API.Startup))]
namespace MySocialGolf.Web.API
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            AuthConfig.ConfigureAuth(app);
        }
    }
}