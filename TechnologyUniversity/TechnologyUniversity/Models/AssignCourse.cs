using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TechnologyUniversity.Models
{
    public class AssignCourse
    {
        public int AssignCourseId { set; get; }
        [Required(ErrorMessage = "Department can't be empty")]
        public int DepartmentId { set; get; }
        public virtual Department Department { set; get; }
        [Required(ErrorMessage = "Teacher can't empty")]
        public int TeacherId { set; get; }
        public virtual Teacher Teacher { set; get; }
        public virtual double CreditTaken { set; get; }
        public virtual double RemaingCredit { set; get; }
        [Required(ErrorMessage = "Course can't empty")]
        public int CourseId { set; get; }
        public virtual Course Course { set; get; }



    }
}