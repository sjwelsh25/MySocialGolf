using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MySocialGolf.Extensions
{
    public static class ObjectExtensions
    {
        public static TConvert ICopyObject<TConvert>(this object input)
        {
            return JsonConvert.DeserializeObject<TConvert>(JsonConvert.SerializeObject(input));
        }
    }
}
