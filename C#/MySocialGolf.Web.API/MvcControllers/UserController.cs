﻿using System.Web.Mvc;
using MySocialGolf.DataManager;
using MySocialGolf.Web.Models.ViewModels;
using MySocialGolf.DataModel;
using MySocialGolf.Model;

namespace MySocialGolf.Web.Controllers
{
    [Authorize]
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
            UserDataManager uDtoMngr = new UserDataManager();
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
        public ActionResult Add(UserDataModel user)
        {
            // Add insert logic here
            var usrMngr = new UserDataManager();
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
