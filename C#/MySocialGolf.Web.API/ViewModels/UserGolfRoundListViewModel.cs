using System.Collections.Generic;

using MySocialGolf.DataModel;

namespace MySocialGolf.Web.Models.ViewModels
{
    public class UserGolfRoundListViewModel
    {
        public IEnumerable<GolfRoundDataModel> UserGolfRoundDTOList { get; set; }
    }
}