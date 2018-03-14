using System.Web.Mvc;
using MySocialGolf.DtoManager;
using MySocialGolf.DtoModel;
using MySocialGolf.Manager;
using MySocialGolf.Web.Models.ViewModels;
using System.Linq;
using System;

namespace MySocialGolf.Web.API.MvcControllers
{
    [Authorize]
    public class TestAPIController : Controller
    {
        // GET: TestAPI
        public ActionResult List()
        {
            TestApiDtoManager taMngr = new TestApiDtoManager();
            TestApiListViewModel talvm = new TestApiListViewModel();
            talvm.TestApiDTOList = taMngr.ListTestApi();
            return View(talvm);
        }

        [HttpPost]
        public JsonResult RunTest(int id)
        {
            TestApiManager taMngr = new TestApiManager();
            TestApiDataModel taDto = taMngr.TestApi(id); // do The Restful call

            return Json(taDto);
        }

        // GET: TestAPI/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TestAPI/Create
        public ActionResult Add()
        {
            return View();
        }

        // POST: TestAPI/Create
        [HttpPost]
        public ActionResult Add(TestApiDataModel dto)
        {
            try
            {
                // TODO: Add insert logic here
                TestApiDtoManager taMngr = new TestApiDtoManager();
                taMngr.AddTestApi(dto);
                return RedirectToAction("List");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // GET: TestAPI/Edit/5
        public ActionResult Edit(int id)
        {
            TestApiDtoManager taDtoMngr = new TestApiDtoManager();
            TestApiDataModel taDto = taDtoMngr.ListTestApi(id).First<TestApiDataModel>();
            return View(taDto);
        }

        // POST: TestAPI/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, TestApiDataModel taDto)
        {
            try
            {
                TestApiDtoManager taDtoMngr = new TestApiDtoManager();
                taDto.TestApiId = id; // doesn't get returned in vm object passed back (only input properties it seems)
                taDtoMngr.UpdateTestApi(taDto);

                return View(taDto); // RedirectToAction("List");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // GET: TestAPI/Delete/5
        public ActionResult Delete(int id)
        {
            // Make a Copy and then return same View
            var taMngr = new TestApiDtoManager();
            taMngr.DeleteTestApi(id);

            return RedirectToAction("List");
        }

        // GET: TestAPI/Clone/5
        public ActionResult Clone(int id)
        {
            // Make a Copy and then return same View
            var taMngr = new TestApiDtoManager();
            taMngr.CloneTestApi(id);

            return RedirectToAction("List");
        }
    }
}
