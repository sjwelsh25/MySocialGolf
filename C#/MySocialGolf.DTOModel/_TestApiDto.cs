using System;
 
namespace MySocialGolf.DtoModel
{
    /// <summary>
    /// Represents a TestApi.
    /// NOTE: This class is generated from a T4 template - you should not modify it manually.
    /// </summary>
    public class _TestApiDto: BaseDto 
    {
        public int TestApiId { get; set; }
        public Guid TestApiGuid { get; set; }
        public string TestName { get; set; }
        public string TestUrl { get; set; }
        public string TestInputJson { get; set; }
        public string HttpVerb { get; set; }
        public string ReturnType { get; set; }
        public int? CreatedById { get; set; }
        public DateTime? CreatedDateTime { get; set; }
        public int? ModifiedById { get; set; }
        public DateTime? ModifiedDateTime { get; set; }
    }
}      
