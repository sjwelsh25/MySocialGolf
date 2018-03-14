using System;
 
namespace MySocialGolf.DtoModel
{
    /// <summary>
    /// Represents a TestResult.
    /// NOTE: This class is generated from a T4 template - you should not modify it manually.
    /// </summary>
    public class _TestResultDataModel: BaseDto 
    {
        public int Id { get; set; }
        public string Class { get; set; }
        public string TestCase { get; set; }
        public string Name { get; set; }
        public string TranName { get; set; }
        public string Result { get; set; }
        public string Msg { get; set; }
        public DateTime TestStartTime { get; set; }
        public DateTime? TestEndTime { get; set; }
    }
}      
