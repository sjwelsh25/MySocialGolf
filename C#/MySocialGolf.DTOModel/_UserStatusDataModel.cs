using System;
 
namespace MySocialGolf.DtoModel
{
    /// <summary>
    /// Represents a UserStatus.
    /// NOTE: This class is generated from a T4 template - you should not modify it manually.
    /// </summary>
    public class _UserStatusDataModel: BaseDataModel 
    {
        public int UserStatusId { get; set; }
        public string Description { get; set; }
        public int? CreatedById { get; set; }
        public DateTime? CreatedDateTime { get; set; }
        public int? ModifiedById { get; set; }
        public DateTime? ModifiedDateTime { get; set; }
    }
}      
