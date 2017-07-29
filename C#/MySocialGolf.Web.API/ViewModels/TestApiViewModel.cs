using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using MySocialGolf.DtoModel;

namespace MySocialGolf.Web.Models.ViewModels
{
    public class TestApiListViewModel
    {
        public IEnumerable<TestApiDto> TestApiDTOList { get; set; }
    }
}