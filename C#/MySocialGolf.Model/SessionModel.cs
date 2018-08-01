using MySocialGolf.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySocialGolf.Model
{
    public class SessionModel
    {

        public int UserId { get; set; }
        public string UserName { get; set; }
        public UserDataModel UserDataModel { get; set; }

    }
}
