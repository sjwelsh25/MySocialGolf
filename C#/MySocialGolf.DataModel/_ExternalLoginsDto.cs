using System;
 
namespace MySocialGolf.DtoModel
{
    /// <summary>
    /// Represents a ExternalLogins.
    /// NOTE: This class is generated from a T4 template - you should not modify it manually.
    /// </summary>
    public class _ExternalLoginsDto: BaseDataModel 
    {
        public Guid ExternalLoginId { get; set; }
        public Guid UserId { get; set; }
        public string LoginProvider { get; set; }
        public string ProviderKey { get; set; }
    }
}      
