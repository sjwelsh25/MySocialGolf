using System.Collections.Generic;

using MySocialGolf.DtoModel;

namespace MySocialGolf.Web.Models.ViewModels
{
    public class UserGolfRoundListViewModel
    {
        public IEnumerable<GolfRoundDto> UserGolfRoundDTOList { get; set; }
    }
}