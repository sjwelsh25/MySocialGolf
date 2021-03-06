using System;
 
namespace MySocialGolf.DtoModel
{
    /// <summary>
    /// Represents a Run_LastExecution.
    /// NOTE: This class is generated from a T4 template - you should not modify it manually.
    /// </summary>
    public class _Run_LastExecutionDto: BaseDto 
    {
        public string TestName { get; set; }
        public int? SessionId { get; set; }
        public DateTime? LoginTime { get; set; }
    }
}      
