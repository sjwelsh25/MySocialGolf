using System;
 
namespace MySocialGolf.DataModel
{
    /// <summary>
    /// Represents a UserGroup.
    /// NOTE: This class is generated from a T4 template - you should not modify it manually.
    /// </summary>
    public class _UserGroupDataModel: BaseDataModel 
    {
        public int UserGroupID { get; set; }
        public int? UserID { get; set; }
        public string GroupName { get; set; }
        public int? CreatedByID { get; set; }
        public DateTime? CreatedDateTime { get; set; }
        public int? ModifiedByID { get; set; }
        public DateTime? ModifiedDateTime { get; set; }
    }
}      
