using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using TechnologyUniversity.Models;

namespace TechnologyUniversity.Controllers
{
    public class AssignCoursesController : Controller
    {
        private UniversityContext db = new UniversityContext();

        // GET: AssignCourses
        public ActionResult Index()
        {
            var assignCourses = db.AssignCourses.Include(a => a.Course).Include(a => a.Department).Include(a => a.Teacher);
            return View(assignCourses.ToList());
        }

        // GET: AssignCourses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AssignCourse assignCourse = db.AssignCourses.Find(id);
            if (assignCourse == null)
            {
                return HttpNotFound();
            }
            return View(assignCourse);
        }

        // GET: AssignCourses/Create
        public ActionResult Create()
        {
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "Code");
            ViewBag.TeacherId = new SelectList(db.Teachers, "TeacherId", "Name");
            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "Code");
            return View();
        }

        // POST: AssignCourses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AssignCourse assignCourse)
        {
            if (ModelState.IsValid)
            {
                var result =
                    db.AssignCourses.Count(
                        r => r.CourseId == assignCourse.CourseId) == 0;

                if (result)
                {
                    Teacher aTeacher = db.Teachers.FirstOrDefault(t => t.TeacherId == assignCourse.TeacherId);
                    Course aCourse = db.Courses.FirstOrDefault(c => c.CourseId == assignCourse.CourseId);
                    List<AssignCourse> assignTeachers =
                        db.AssignCourses.Where(t => t.TeacherId == assignCourse.TeacherId).ToList();
                    AssignCourse assign = null;
                    if (assignTeachers.Count != 0)
                    {

                        assign = assignTeachers.Last();
                        assignCourse.RemaingCredit = assign.RemaingCredit;
                    }
                    else
                    {
                        assignCourse.RemaingCredit = aTeacher.CreditTaken;
                    }

                    //if (assign == null)
                    //    assigncourse.RemaingCredit = aTeacher.CreditTaken;

                    if (assignCourse.RemaingCredit < aCourse.Credit)
                    {
                        Session["Teacher"] = aTeacher;
                        Session["Course"] = aCourse;
                        Session["AssignedCourse"] = assignCourse;
                        Session["AssigneddCourseCheck"] = assign;
                        return RedirectToAction("AskToAssign");
                    }

                    assignCourse.CreditTaken = aTeacher.CreditTaken;

                    if (assign == null)
                    {
                        assignCourse.RemaingCredit = aTeacher.CreditTaken - aCourse.Credit;
                    }
                    else
                    {
                        assignCourse.RemaingCredit = assign.RemaingCredit - aCourse.Credit;
                    }

                    aCourse.AssignTo = aTeacher.Name;

                    db.AssignCourses.Add(assignCourse);
                    db.SaveChanges();
                    TempData["success"] = "Course is assigned successfully";
                    return RedirectToAction("Create");
                }
                else
                {
                    TempData["Already"] = "Course has already been assigned";
                    return RedirectToAction("Create");
                }
            }

            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "Code", assignCourse.CourseId);
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "Code", assignCourse.DepartmentId);
            ViewBag.TeacherId = new SelectList(db.Teachers, "TeacherId", "Name", assignCourse.TeacherId);
            return View(assignCourse);
        }



        // GET: AssignCourses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AssignCourse assignCourse = db.AssignCourses.Find(id);
            if (assignCourse == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "Code", assignCourse.CourseId);
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "Code", assignCourse.DepartmentId);
            ViewBag.TeacherId = new SelectList(db.Teachers, "TeacherId", "Name", assignCourse.TeacherId);
            return View(assignCourse);
        }

        // POST: AssignCourses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AssignCourseId,DepartmentId,TeacherId,CreditTaken,RemaingCredit,CourseId")] AssignCourse assignCourse)
        {
            if (ModelState.IsValid)
            {
                db.Entry(assignCourse).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "Code", assignCourse.CourseId);
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "Code", assignCourse.DepartmentId);
            ViewBag.TeacherId = new SelectList(db.Teachers, "TeacherId", "Name", assignCourse.TeacherId);
            return View(assignCourse);
        }

        // GET: AssignCourses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AssignCourse assignCourse = db.AssignCourses.Find(id);
            if (assignCourse == null)
            {
                return HttpNotFound();
            }
            return View(assignCourse);
        }

        // POST: AssignCourses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AssignCourse assignCourse = db.AssignCourses.Find(id);
            db.AssignCourses.Remove(assignCourse);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult LoadTeacher(int? departmentId)
        {
            var teacherList = db.Teachers.Where(u => u.DepartmentId == departmentId).ToList();
            ViewBag.TeacherId = new SelectList(teacherList, "TeacherId", "Name");
            return PartialView("~/Views/Shared/_FillteredTeacher.cshtml");
        }

        public ActionResult LoadCourse(int? departmentId)
        {
            var courseList = db.Courses.Where(u => u.DepartmentId == departmentId).ToList();
            ViewBag.CourseId = new SelectList(courseList, "CourseId", "Name");
            return PartialView("~/Views/shared/_FilteredCourse.cshtml");

        }

        public PartialViewResult CourseInfoLoad(int? courseId)
        {
            if (courseId != null)
            {
                Course aCourse = db.Courses.FirstOrDefault(s => s.CourseId == courseId);
                ViewBag.Code = aCourse.Code;
                ViewBag.Credit = aCourse.Credit;
                return PartialView("~/Views/Shared/_CourseInfo.cshtml");
            }
            else
            {
                return PartialView("~/Views/Shared/_CourseInfo.cshtml");
            }

        }

        public PartialViewResult TeacherInfoLoad(int? teacherId)
        {
            if (teacherId != null)
            {
                Teacher aTeacher = db.Teachers.FirstOrDefault(s => s.TeacherId == teacherId);
                ViewBag.Credit = aTeacher.CreditTaken;
                List<AssignCourse> assignTeachers =
                        db.AssignCourses.Where(t => t.TeacherId == teacherId).ToList();
                AssignCourse assign = null;
                if (assignTeachers.Count != 0)
                {
                    assign = assignTeachers.Last();
                }
                if (assign == null)
                {
                    ViewBag.RemainingCredit = aTeacher.CreditTaken;
                }
                else
                {
                    ViewBag.RemainingCredit = assign.RemaingCredit;
                }

                return PartialView("~/Views/Shared/_TeachersCreditInfo.cshtml");
            }
            else
            {
                return PartialView("~/Views/Shared/_TeachersCreditInfo.cshtml");
            }

        }
        public ActionResult AskToAssign()
        {
            Teacher aTeacher = (Teacher)Session["Teacher"];
            Course aCourse = (Course)Session["Course"];
            AssignCourse assign = (AssignCourse)Session["AssigneddCourseCheck"];
            double remainingCredite = 0.0;
            if (assign == null)
                remainingCredite = aTeacher.CreditTaken;
            else
            {
                remainingCredite = assign.RemaingCredit;
            }
            if (remainingCredite < 0)
            {
                ViewBag.Message = aTeacher.Name
                + " can take no more course of credit : " + aCourse.Credit
                + " \n ! Do you still want to assign this course to this teacher ?";
            }
            else
            {
                ViewBag.Message = aTeacher.Name
                + " has only " + remainingCredite
                + " credits remaining ." + "\n" + "your Selected Course : " + aCourse.Code
                + " has " + aCourse.Credit + " Credits"
                + "\n  ! Do you still want to assign this course to this teacher ?";
            }

            return View();
        }

        public ActionResult AssignConfirmed()
        {
            Teacher aTeacher = (Teacher)Session["Teacher"];

            AssignCourse assigncourse = (AssignCourse)Session["AssignedCourse"];
            AssignCourse assign = (AssignCourse)Session["AssigneddCourseCheck"];
            Course aCourse = db.Courses.FirstOrDefault(c => c.CourseId == assigncourse.CourseId);


            assigncourse.CreditTaken = aTeacher.CreditTaken;
            if (assign == null)
            {
                assigncourse.RemaingCredit = aTeacher.CreditTaken - aCourse.Credit;
            }
            else
            {
                assigncourse.RemaingCredit = assign.RemaingCredit - aCourse.Credit;
            }

            aCourse.AssignTo = aTeacher.Name;

            db.AssignCourses.Add(assigncourse);
            db.SaveChanges();
            TempData["success"] = "Course is assigned successfully";
            return View();
        }

        public ActionResult ViewCourseStatus()
        {
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "Code");
            return View();
        }

        public PartialViewResult CourseStatusLoad(int? departmentId)
        {
            List<Course> courseList = new List<Course>();
            if (departmentId != null)
            {
                //var model = db.Courses.Where(d => d.DepartmentId == departmentId);
                courseList = db.Courses.Where(r => r.DepartmentId == departmentId).ToList();
                if (courseList.Count == 0)
                {
                    ViewBag.NotAssigned = "This department has no course";
                }
             return PartialView("~/Views/shared/_coursestatus.cshtml", courseList);
            }


            return PartialView("~/Views/shared/_coursestatus.cshtml", courseList);
        }
        public PartialViewResult AllCourseStatusLoad(int? departmentId)
        {
            

            var courselist = from n in db.Courses
                select n;


            return PartialView("~/Views/shared/_coursestatus.cshtml", courselist);
           


            
        }
    }
}
