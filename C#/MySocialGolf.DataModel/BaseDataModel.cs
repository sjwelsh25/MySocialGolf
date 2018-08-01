using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace MySocialGolf.DataModel
{
    public class BaseDataModel
    {
        public BaseDataModel()
        {
            SubmitMessage = "";
        }

        public string SubmitMessage { get; set; }

    }
}
