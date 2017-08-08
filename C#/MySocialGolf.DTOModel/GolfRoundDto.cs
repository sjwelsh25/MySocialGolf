using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MySocialGolf.DtoModel
{
    public class GolfRoundDto: _GolfRoundDto
    {
        //public int? NewUserGolfRoundID { get; set; }
        //[Display(Name = "Golf Course Name")]
        //new public string GolfCourseName { get; set; }
        //[Display(Name = "StableFord Score")]
        //new public int? StableFordScore { get; set; }
        //[Display(Name = "Stroke Score")]
        //new public int? StrokeScore { get; set; }
        //[Display(Name = "Handicap That Day")]
        //new public int? HandicapThatDay { get; set; }
        //[Display(Name = "Course Par That Day")]
        //new public int? CourseParThatDay { get; set; }

        public int UserId { get; set; }
        public string HandicapAndDate { get; set; }
    }
}
