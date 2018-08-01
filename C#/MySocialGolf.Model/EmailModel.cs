using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySocialGolf.Model
{
    public class EmailModel
    {
        public string ApiKey { get; set; }
        public string FromEmailAddress { get; set; }
        public string FromEmailName { get; set; }
        public string ToEmailAddress { get; set; }
        public string ToEmailName { get; set; }
        public string BodyPlainText { get; set; }
        public string BodyHtml { get; set; }
        public string Subject { get; set; }
    }
}
