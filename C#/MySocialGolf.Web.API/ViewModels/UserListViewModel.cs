using System.Collections.Generic;

using MySocialGolf.DataModel;

namespace MySocialGolf.Web.Models.ViewModels
{
    public class UserListViewModel
    {
        public IEnumerable<UserDataModel> UserDTOList { get; set; }
    }
}