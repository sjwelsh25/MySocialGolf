using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySocialGolf.DtoModel
{
    public class UserDataModel: _UserDataModel
    {
        public int? NewUserID { get; set; }
        public DateTime LastGolfRoundDate { get; set; }
        public string LastHandicapAndDate { get; set; }
    }
}
