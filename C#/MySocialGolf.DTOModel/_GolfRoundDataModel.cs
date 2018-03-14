using System;
 
namespace MySocialGolf.DtoModel
{
    /// <summary>
    /// Represents a GolfRound.
    /// NOTE: This class is generated from a T4 template - you should not modify it manually.
    /// </summary>
    public class _GolfRoundDataModel: BaseDataModel 
    {
        public int GolfRoundId { get; set; }
        public int? UserId { get; set; }
        public string GolfCourseName { get; set; }
        public DateTime? GolfRoundDate { get; set; }
        public int? StableFordScore { get; set; }
        public int? StrokeScore { get; set; }
        public int? HandicapThatDay { get; set; }
        public int? CourseParThatDay { get; set; }
        public int? CreatedById { get; set; }
        public DateTime? CreatedDateTime { get; set; }
        public int? ModifiedById { get; set; }
        public DateTime? ModifiedDateTime { get; set; }
    }
}      
