using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySocialGolf.Model;
using System.Web;

namespace MySocialGolf.Manager
{
    public static class SessionManager
    {

        public static SessionModel Model
        {
            get
            {
                if (HttpContext.Current == null 
                    || HttpContext.Current.Session == null
                    || HttpContext.Current.Session["MySocialGolfSession"] == null)
                {
                    HttpContext.Current.Session["MySocialGolfSession"] = new SessionModel();
                }
                return (SessionModel)HttpContext.Current.Session["MySocialGolfSession"];
            }
        }
    }
}
