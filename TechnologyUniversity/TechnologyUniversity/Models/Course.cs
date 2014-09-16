using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TechnologyUniversity.Models
{
    public class Course
    {
        public int CourseId { set; get; }
        [Required(ErrorMessage = "Code can't be empty")]
        [Remote("CheckCode", "Courses", ErrorMessage = "Code Already exist")]
        public string Code { set; get; }
        [Required(ErrorMessage = "Name can't be empty")]
        [Remote("CheckName", "Courses", ErrorMessage = "Name Already exist")]
        public string Name { set; get; }
        [Required(ErrorMessage = "Credit can't be empty")]
        public double Credit { set; get; }
        public string Description { set; get; }

        [Required(ErrorMessage = "Department can't be empty")]
        public int DepartmentId { set; get; }
        public virtual Department CourseDepartment { set; get; }

        [Required(ErrorMessage = "Semester can't be empty")]
        public int SemesterId { set; get; }
        public virtual Semester CourseSemester { set; get; }
        public virtual string AssignTo { set; get; }
        public virtual List<RoomAllocation> RoomAllocationList { set; get; }
    }
}