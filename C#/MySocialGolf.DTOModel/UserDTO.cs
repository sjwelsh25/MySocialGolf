using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySocialGolf.DtoModel
{
    public class UserDto: _UserDto
    {
        public int? NewUserID { get; set; }
        public DateTime LastGolfRoundDate { get; set; }
        public string LastHandicapAndDate { get; set; }
    }
}
