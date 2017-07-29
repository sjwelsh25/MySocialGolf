using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace MySocialGolf.DtoModel
{
    public class BaseDto
    {
        public BaseDto()
        {
            SubmitMessage = "";
        }

        public string SubmitMessage { get; set; }

    }
}
