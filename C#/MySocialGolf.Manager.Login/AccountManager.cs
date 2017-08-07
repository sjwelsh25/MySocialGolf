using Microsoft.Owin.Security;
using MySocialGolf.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MySocialGolf.Manager.Login
{
    public class AccountManager
    {
        public const string DefaultAuthType = "MySocialGolfCookie";

        public bool LogOnUser(LogOnModel model)
        {
            
            //user = the user that is loggin on, retrieved from database 
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, model.UserName),
                //new Claim(ClaimTypes.Email, model.),
                //some other claims
            };

            ClaimsIdentity identity = new ClaimsIdentity(claims, DefaultAuthType);
            IAuthenticationManager authManager = HttpContext.Current.Request.GetOwinContext().Authentication;
            authManager.SignIn(new AuthenticationProperties() { IsPersistent = true }, identity);

            return true;
        }
    }
}
