using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MySocialGolf.Extensions;

namespace MySocialGolf.DtoModel
{
    public class TestApiDto : _TestApiDto
    {
        public string LastLogResponse { get; set; } = "";
        public string LastStatusCode { get; set; }
        public DateTime LastLogDateTime { get; set; }
        public string ResponseToLog { get; set; } = "";
        public string TestAPIMessage { get; set; } = "";

        public string LastResponseCalculated
        {
            get
            {
                return (LastLogDateTime > DateTime.MinValue ? LastLogDateTime.IToDateTimeStamp() : ""); //  + (LastLogResponse.Length == 0 ? "" : LastLogResponse);
            }
        }
    }

}
