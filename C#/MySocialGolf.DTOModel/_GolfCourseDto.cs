using System;
 
namespace MySocialGolf.DtoModel
{
    /// <summary>
    /// Represents a GolfCourse.
    /// NOTE: This class is generated from a T4 template - you should not modify it manually.
    /// </summary>
    public class _GolfCourseDto: BaseDto 
    {
        public int GolfCourseId { get; set; }
        public string CourseName { get; set; }
        public string Address { get; set; }
        public int? CoursePar { get; set; }
        public int? CreatedById { get; set; }
        public DateTime? CreatedDateTime { get; set; }
        public int? ModifiedById { get; set; }
        public DateTime? ModifiedDateTime { get; set; }
    }
}      
