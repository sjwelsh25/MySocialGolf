using MySocialGolf.Manager.Email;
using MySocialGolf.Web.Models;
using MySocialGolf.Web.Models.ViewModels;
using System.Linq;
using System.Web.Mvc;

namespace MySocialGolf.Web.Controllers
{
    public class AdminController : Controller
    {
        public ViewResult Index()
        {
            return View();
        }

        public ViewResult Email()
        {
            return View();
        }

        //
        // POST: /Admin/Email
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Email(AdminTestEmailViewModel model)
        {
            EmailManager emailMngr = new EmailManager();
            emailMngr.SendEmail(new Model.EmailModel()
            {
                ToEmailAddress = model.ToEmail,
                BodyHtml = model.EmailBody,
                Subject = model.EmailSubject
            });

            // If we got this far, something failed, redisplay form
            return View(model);
        }

    }
}