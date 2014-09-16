using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TechnologyUniversity.Models;

namespace TechnologyUniversity.Controllers
{
    public class ResultEntriesController : Controller
    {
        private UniversityContext db = new UniversityContext();

        // GET: ResultEntries
        public ActionResult Index()
        {
            var resultEntries = db.ResultEntries.Include(r => r.Course).Include(r => r.Grade).Include(r => r.Student);
            return View(resultEntries.ToList());
        }

        // GET: ResultEntries/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ResultEntry resultEntry = db.ResultEntries.Find(id);
            if (resultEntry == null)
            {
                return HttpNotFound();
            }
            return View(resultEntry);
        }

        // GET: ResultEntries/Create
        public ActionResult Create()
        {
            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "Code");
            ViewBag.GradeId = new SelectList(db.Grades, "GradeId", "Name");
            ViewBag.StudentId = new SelectList(db.Students, "StudentId", "RegistrationId");
            return View();
        }

        // POST: ResultEntries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( ResultEntry resultEntry)
        {
            if (ModelState.IsValid)
            {
                var result =
                    db.ResultEntries.Count(
                        r => r.StudentId == resultEntry.StudentId && r.CourseId == resultEntry.CourseId) == 0;
                if (result)
                {
                    Grade aGrade = db.Grades.FirstOrDefault(g => g.GradeId == resultEntry.GradeId);
                    EnrollCourse resultEnrollCourse =
                        db.EnrollCourses.FirstOrDefault(r => r.CourseId == resultEntry.CourseId && r.StudentId == resultEntry.StudentId);

                    if (resultEnrollCourse != null) resultEnrollCourse.GradeName = aGrade.Name;

                    db.ResultEntries.Add(resultEntry);
                    db.SaveChanges();
                    TempData["success"] = "Result Entry is Successful";
                    return RedirectToAction("Create");
                }
                else
                {
                    TempData["Already"] = "Result of this course has already been assigned";
                    return RedirectToAction("Create");
                }
            }

            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "Code", resultEntry.CourseId);
            ViewBag.GradeId = new SelectList(db.Grades, "GradeId", "Name", resultEntry.GradeId);
            ViewBag.StudentId = new SelectList(db.Students, "StudentId", "RegistrationId", resultEntry.StudentId);
            return View(resultEntry);
        }


        public PartialViewResult StudentInfoLoad(int? studentId)
        {
            if (studentId != null)
            {
                Student aStudent = db.Students.FirstOrDefault(s => s.StudentId == studentId);
                ViewBag.Name = aStudent.Name;
                ViewBag.Email = aStudent.Email;
                ViewBag.Dept = aStudent.Department.Name;
                return PartialView("~/Views/Shared/_StudentInformation.cshtml");
            }
            else
            {
                return PartialView("~/Views/Shared/_StudentInformation.cshtml");
            }

        }
        // GET: ResultEntries/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ResultEntry resultEntry = db.ResultEntries.Find(id);
            if (resultEntry == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "Code", resultEntry.CourseId);
            ViewBag.GradeId = new SelectList(db.Grades, "GradeId", "Name", resultEntry.GradeId);
            ViewBag.StudentId = new SelectList(db.Students, "StudentId", "RegistrationId", resultEntry.StudentId);
            return View(resultEntry);
        }

        // POST: ResultEntries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ResultEntryId,StudentId,CourseId,GradeId")] ResultEntry resultEntry)
        {
            if (ModelState.IsValid)
            {
                db.Entry(resultEntry).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "Code", resultEntry.CourseId);
            ViewBag.GradeId = new SelectList(db.Grades, "GradeId", "Name", resultEntry.GradeId);
            ViewBag.StudentId = new SelectList(db.Students, "StudentId", "RegistrationId", resultEntry.StudentId);
            return View(resultEntry);
        }

        // GET: ResultEntries/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ResultEntry resultEntry = db.ResultEntries.Find(id);
            if (resultEntry == null)
            {
                return HttpNotFound();
            }
            return View(resultEntry);
        }

        // POST: ResultEntries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ResultEntry resultEntry = db.ResultEntries.Find(id);
            db.ResultEntries.Remove(resultEntry);
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

        public PartialViewResult CourseLoad(int? studentId)
        {
            List<EnrollCourse> enrollmentList = new List<EnrollCourse>();
            List<Course> courseList = new List<Course>();
            if (studentId != null)
            {
                enrollmentList = db.EnrollCourses.Where(e => e.StudentId == studentId).ToList();
                foreach (EnrollCourse anEnrollment in enrollmentList)
                {
                    Course aCourse = db.Courses.FirstOrDefault(c => c.CourseId == anEnrollment.CourseId);
                    courseList.Add(aCourse);
                }
                ViewBag.CourseId = new SelectList(courseList, "CourseId", "Code");
            }
            return PartialView("~/Views/shared/_FilteredCourse.cshtml");
        }
        public ActionResult ViewResult()
        {
            ViewBag.StudentId = new SelectList(db.Students, "StudentId", "RegistrationId");
            return View();
        }

        public PartialViewResult ResultInfoLoad(int? studentId)
        {

            List<EnrollCourse> enrollCourseList = new List<EnrollCourse>();

            if (studentId != null)
            {
                enrollCourseList = db.EnrollCourses.Where(r => r.StudentId == studentId).ToList();

                if (enrollCourseList.Count == 0)
                {
                    Student aStudent = db.Students.Find(studentId);
                    ViewBag.NotEnrolled = aStudent.RegistrationId + "  : has not taken any course";
                }
            }

            return PartialView("~/Views/shared/_resultInformation.cshtml", enrollCourseList);
        }
    }

  
}
