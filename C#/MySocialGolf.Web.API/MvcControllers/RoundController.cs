using System.Web.Mvc;
using MySocialGolf.DtoManager;
using MySocialGolf.Web.Models.ViewModels;
using MySocialGolf.DtoModel;

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
            GolfRoundsDtoManager userMngr = new GolfRoundsDtoManager();
            UserGolfRoundListViewModel ugrlvm = new UserGolfRoundListViewModel();
            ugrlvm.UserGolfRoundDTOList = userMngr.ListGolfRoundForUser(0);
            return View(ugrlvm);
        }

        // POST: Rounds/Create
        [HttpPost]
        public ActionResult Add(GolfRoundDto roundDTO)
        {
            GolfRoundsDtoManager usrMngr = new GolfRoundsDtoManager();
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
