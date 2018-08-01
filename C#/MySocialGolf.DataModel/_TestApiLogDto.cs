using System;
 
namespace MySocialGolf.DtoModel
{
    /// <summary>
    /// Represents a TestApiLog.
    /// NOTE: This class is generated from a T4 template - you should not modify it manually.
    /// </summary>
    public class _TestApiLogDto: BaseDataModel 
    {
        public int TestApiLogId { get; set; }
        public Guid TestApiLogGuid { get; set; }
        public int TestApiId { get; set; }
        public int? CreatedById { get; set; }
        public DateTime? CreatedDateTime { get; set; }
        public string StatusCode { get; set; }
        public string Response { get; set; }
    }
}      
