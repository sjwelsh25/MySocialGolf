using System.Collections.Generic;

using MySocialGolf.DtoModel;

namespace MySocialGolf.Web.Models.ViewModels
{
    public class UserListViewModel
    {
        public IEnumerable<UserDataModel> UserDTOList { get; set; }
    }
}