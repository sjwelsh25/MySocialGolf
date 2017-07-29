using MySocialGolf.Web.Models;
using System.Linq;
using System.Web.Mvc;

namespace MySocialGolf.Web.Controllers
{
    public class HomeController : Controller
    {
        public ViewResult Index()
        {
            ViewBag.NumSessions = Sessions.All.Count;
            return View();
        }

        public ViewResult Friends()
        {
            var allSpeakers = Sessions.All.SelectMany(x => x.Speakers).Distinct().OrderBy(x => x);
            return View(allSpeakers);
        }

        public ViewResult Rounds()
        {
            var allTags = Sessions.All.SelectMany(x => x.Tags).Distinct().OrderBy(x => x);
            return View(allTags);
        }

        public ViewResult AddFriend()
        {
            return View();
        }

        public ViewResult AddRound()
        {
            return View();
        }

    }
}