using System;

namespace MySocialGolf.Model
{
    public class UserNotUsedEF : IUser
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }

        string IUser.Id
        {
            get { return UserId.ToString(); }
        }
    }
}
