using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySocialGolf.Extensions
{
    public static class DateExtensions
    {

        public static string IToDateTimeStamp(this DateTime d)
        {
            return d.ToString("dd/MM/yy h:mm:sstt").ToLower();
        }

    }
}
