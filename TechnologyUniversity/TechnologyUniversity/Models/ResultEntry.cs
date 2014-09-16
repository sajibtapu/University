using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TechnologyUniversity.Models
{
    public class ResultEntry
    {
        public int ResultEntryId { set; get; }

        [Required(ErrorMessage = "Student can't be empty")]
        public int StudentId { set; get; }
        public virtual Student Student { set; get; }
        [Required(ErrorMessage = "Course can't be empty")]
        public int CourseId { set; get; }
        public virtual Course Course { set; get; }

        [Required(ErrorMessage = "Grade can't be empty")]
        public int GradeId { set; get; }
        public virtual Grade Grade { set; get; }
    }
}