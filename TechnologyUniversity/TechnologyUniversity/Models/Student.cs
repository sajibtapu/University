using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TechnologyUniversity.Models
{
    public class Student
    {
        public int StudentId { get; set; }
        public virtual string RegistrationId { set; get; }
        [Display(Name = "Student Name")]
        [Required(ErrorMessage = "Name can't be empty")]
        public string Name { get; set; }

        [Remote("CheckEmail", "Students", ErrorMessage = "Email already exist")]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Email can't be empty")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Contact No can't be empty")]
        public string ContactNo { get; set; }

        [Required(ErrorMessage = "Date can't be empty")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        [Required(ErrorMessage = "Address can't be empty")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Department can't be empty")]
        public int DepartmentId { get; set; }
        public virtual Department Department { get; set; }
    }
}