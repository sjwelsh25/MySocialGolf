using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySocialGolf.DataManager
{
    public static class DBConnectionManager
    {
        public static string GetConnectionString()
        {
            string s = ConfigurationManager.ConnectionStrings["MySocialGolfConnectionString"].ToString();
            return String.Format(s, "23");
        }
    }
}
