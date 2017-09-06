using System;
using Microsoft.AspNet.Identity;

namespace MySocialGolf.Model.Identity
{
    /// <summary>
    /// Simple Wrapper around the Microsoft.Asp.Identity.IUser for Owin support. SW 5/9/17
    /// </summary>
    public class IdentityUser : IUser
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }

        public string Id
        {
            get { return UserId; }
        }
    }
}
