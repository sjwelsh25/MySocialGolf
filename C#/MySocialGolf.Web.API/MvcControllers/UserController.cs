using System.Web.Mvc;
using MySocialGolf.DtoManager;
using MySocialGolf.Web.Models.ViewModels;
using MySocialGolf.DtoModel;
using MySocialGolf.Model;

namespace MySocialGolf.Web.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        // GET: User/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: User/List
        public ViewResult List()
        {
            UserDtoManager uDtoMngr = new UserDtoManager();
            UserListViewModel ulvm = new UserListViewModel();
            ulvm.UserDTOList = uDtoMngr.ListUser(new UserSearchModel());
            return View(ulvm);
        }

        // GET: User/Add
        public ActionResult Add()
        {
            return View();
        }

        // POST: User/Add
        [HttpPost]
        public ActionResult Add(UserDto user)
        {
            // Add insert logic here
            var usrMngr = new UserDtoManager();
            usrMngr.AddUser(user);

            return View(user);
            // return RedirectToAction("Index");
        }

        // GET: User/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: User/Edit/5
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

        // GET: User/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: User/Delete/5
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
