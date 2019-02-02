using MySocialGolf.DataManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySocialGolf.Manager.Email;

namespace MySocialGolf.Manager
{
    public class MSGUserManager
    {

        public void LogInUser(string userName)
        {
            // log to DB and add to Session
            UserDataManager userDMngr = new UserDataManager();
            var userDMdl = userDMngr.GetUserByUserName(userName);

            if (userDMdl != null)
            {
                SessionManager.Model.UserId = userDMdl.UserId;
                SessionManager.Model.UserName = userName;
                SessionManager.Model.UserDataModel = userDMdl;
            }
        }

        public void RegisterNewUser(string callbackUrl, string emailConfTokenCode)
        {
            // write to database the new register email details

            UserDataManager userDMngr = new UserDataManager();
            userDMngr.RegisterNewUser(SessionManager.Model.UserId);

            // Now email the confirmation
            EmailManager emailMngr = new EmailManager();
            Model.EmailModel model = new Model.EmailModel()
            {
                BodyHtml = "Please confirm your account by clicking <a href=\""
                   + callbackUrl + "\">here</a>",
                Subject = "Confirm your account",
                ToEmailAddress = SessionManager.Model.UserName,
                FromEmailName = "My Social Golf Email Confirmation",
                FromEmailAddress = "confirmation@mysocialgolf.com.au"
            };
            emailMngr.SendEmail(model);

        }

    }
}
