using MySocialGolf.DataManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySocialGolf.Manager
{
    public class UserManager
    {

        public void LogInUser(string userName)
        {
            // log to DB and add to Session
            UserDataManager userDMngr = new UserDataManager();
            var userDMdl = userDMngr.GetUserByUserName(userName);

            SessionManager.Model.UserId = userDMdl.UserId;
            SessionManager.Model.UserName = userName;
            SessionManager.Model.UserDataModel = userDMdl;
        }

    }
}
