using System.Web.Mvc;
using MySocialGolf.DataManager;
using MySocialGolf.Web.Models.ViewModels;
using MySocialGolf.DataModel;
using MySocialGolf.Manager;

namespace MySocialGolf.Web.Controllers
{
    [Authorize]
    public class RoundController : Controller
    {
        // GET: Rounds
        public ActionResult Index()
        {
            return View();
        }

        // GET: Rounds/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Rounds/Create
        public ActionResult Add()
        {
            return View();
        }

        // GET: User/List
        public ViewResult List()
        {
            GolfRoundsDataManager userMngr = new GolfRoundsDataManager();
            UserGolfRoundListViewModel ugrlvm = new UserGolfRoundListViewModel();
            ugrlvm.UserGolfRoundDTOList = userMngr.ListGolfRoundForUser(0);
            return View(ugrlvm);
        }

        // POST: Rounds/Create
        [HttpPost]
        public ActionResult Add(GolfRoundDataModel roundDTO)
        {
            GolfRoundsDataManager usrMngr = new GolfRoundsDataManager();
            // if UserId is Null then get it from Session
            if (roundDTO.UserId == null || roundDTO.UserId == 0)
            {
                roundDTO.UserId = SessionManager.Model.UserId;
            }
            usrMngr.UpdateGolfRound(roundDTO);

            return View(roundDTO);
        }

        // GET: Rounds/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Rounds/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Rounds/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Rounds/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
