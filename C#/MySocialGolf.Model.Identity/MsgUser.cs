using System;
using Microsoft.AspNet.Identity;

namespace MySocialGolf.Model.Identity
{
    public class MsgUser : IUser
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }

        public string Id
        {
            get { return UserId.ToString(); }
        }
    }
}
