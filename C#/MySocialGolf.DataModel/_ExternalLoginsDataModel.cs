using System;
 
namespace MySocialGolf.DataModel
{
    /// <summary>
    /// Represents a ExternalLogins.
    /// NOTE: This class is generated from a T4 template - you should not modify it manually.
    /// </summary>
    public class _ExternalLoginsDataModel: BaseDataModel 
    {
        public Guid ExternalLoginId { get; set; }
        public Guid UserId { get; set; }
        public string LoginProvider { get; set; }
        public string ProviderKey { get; set; }
    }
}      
