using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using MySocialGolf.DataModel;

namespace MySocialGolf.Web.Models.ViewModels
{
    public class TestApiListViewModel
    {
        public IEnumerable<TestApiDataModel> TestApiDTOList { get; set; }
    }
}