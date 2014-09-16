using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TechnologyUniversity.Models
{
    public class Teacher
    {
       
 
        public int TeacherId { set; get; }
        [Required(ErrorMessage = "Name can't be empty")]
        public string Name { set; get; }
        public string Address { set; get; }
        [Required(ErrorMessage = "Email can't be empty")]
        [Remote("CheckEmail","Teachers",ErrorMessage = "Email already exist")]
        [EmailAddress(ErrorMessage = "Please Enter a valid Email Address")]
        public string Email { set; get; }
        [Display (Name = "Contact No")]
        [Required(ErrorMessage = "Contact can't be empty")]
        public string ContactNo { set; get; }
        [Required(ErrorMessage = "Designation can't be empty")]
        public int DesignationId { set; get; }
        public virtual Designation TeacherDesignation { set; get; }
        [Required(ErrorMessage = "Department can't be empty")]
        public int DepartmentId { set; get; }
        public virtual Department TeacherDepartment { set; get; }
        [Required(ErrorMessage = "Must take some credit")]
        public double CreditTaken { set; get; }
    }
}