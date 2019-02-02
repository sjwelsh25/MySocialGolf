﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySocialGolf.DataModel
{
    public class UserDataModel : _UserDataModel
    {
        public int? NewUserId { get; set; }
        public DateTime? LastGolfRoundDate { get; set; }
        public string LastHandicapAndDate { get; set; }
    }

}
