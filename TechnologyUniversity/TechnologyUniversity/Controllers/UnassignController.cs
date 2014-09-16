using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TechnologyUniversity.Models;

namespace TechnologyUniversity.Controllers
{
    public class UnassignController : Controller
    {
        
        UniversityContext db = new UniversityContext();
        public ActionResult UnassignCourse()
        {
            return View();
        }

        public string UnAssign()
        {
            var model = db.AssignCourses.ToList();
            foreach (var assignCourse in model)
            {
                db.AssignCourses.Remove(assignCourse);
                db.SaveChanges();
            }
            return "success";
        }

        public ActionResult UnassignRoom()
        {
            return View();
        }



        public string UnRoom()
        {
            var model = db.RoomAllocations.ToList();
            foreach (var assignRoom in model)
            {
                db.RoomAllocations.Remove(assignRoom);
                db.SaveChanges();
            }
            return "success";
        }
    }
}