using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySocialGolf.Model
{
    public class RestCallModel
    {
        public string BaseAddressUrl { get; set; }
        public string Url { get; set; }
        public string HttpVerb { get; set; }
        public string ResponseMessage { get; set; }
        public string ResponseMessageOverride { get; set; }
        public string InputJson { get; set; }
        // public System.Net.HttpStatusCode StatusCode { get; set; }
        public string StatusCode { get; set; }

        public RestCallModel()
        {
            BaseAddressUrl = "http://localhost:37217/";
        }
    }

}
