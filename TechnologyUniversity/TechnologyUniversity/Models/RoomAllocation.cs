using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TechnologyUniversity.Models
{
    public class RoomAllocation
    {
        public int RoomAllocationId { set; get; }


        [Required(ErrorMessage = "Department can't be empty")]
        public int DepartmentId { set; get; }
        public virtual Department Department { set; get; }

        [Required(ErrorMessage = "Course can't be empty")]
        public int CourseId { set; get; }
        public virtual Course Course { set; get; }

        [Required(ErrorMessage = "Room can't be empty")]
        public int RoomId { set; get; }
        public virtual Room Room { set; get; }

        [Required(ErrorMessage = "Day can't be empty")]
        public int DayId { set; get; }
        public virtual Day Day { set; get; }
        [Required(ErrorMessage = "Start time is required")]
        [Display(Name = "Start time (Formate HH:MM) (24 Hours)")]
        public string StartTime { set; get; }
        [Required(ErrorMessage = "End time is required")]
        [Display(Name = "End time (Formate HH:MM) (24 Hours)")]
        public string EndTime { set; get; }


    }
}