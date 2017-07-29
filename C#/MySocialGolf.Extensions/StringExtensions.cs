using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySocialGolf.Extensions
{
    public static class StringExtensions
    {
        public static bool IEqualsNullSafe(this string source, string param)
        {
            if (source == null)
            {
                return false;
            }
            return source.ToLower().Equals(param.ToLower());
        }
    }
}
