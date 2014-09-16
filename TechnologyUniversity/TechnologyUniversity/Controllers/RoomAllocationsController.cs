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
    public class RoomAllocationsController : Controller
    {
        private UniversityContext db = new UniversityContext();

        // GET: RoomAllocations
        public ActionResult Index()
        {
            var roomAllocations = db.RoomAllocations.Include(r => r.Course).Include(r => r.Day).Include(r => r.Department).Include(r => r.Room);
            return View(roomAllocations.ToList());
        }

        // GET: RoomAllocations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RoomAllocation roomAllocation = db.RoomAllocations.Find(id);
            if (roomAllocation == null)
            {
                return HttpNotFound();
            }
            return View(roomAllocation);
        }

        // GET: RoomAllocations/Create
        public ActionResult Create()
        {
            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "Code");
            ViewBag.DayId = new SelectList(db.Days, "DayId", "Name");
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "Code");
            ViewBag.RoomId = new SelectList(db.Rooms, "RoomId", "Name");
            return View();
        }

        // POST: RoomAllocations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RoomAllocation roomallocation)
        {

            Room room = db.Rooms.Find(roomallocation.RoomId);
            Course course = db.Courses.Find(roomallocation.CourseId);
            Day day = db.Days.Find(roomallocation.DayId);


            if (ModelState.IsValid)
            {

                int givenfrom, givenEnd;

                try
                {
                    givenfrom = ConvertTimetoInt(roomallocation.StartTime);
                }
                catch (Exception anException)
                {
                    if (TempData["ErrorMessage3"] == null)
                    {
                        TempData["ErrorMessage1"] = anException.Message;
                    }
                    TempData["ErrorMessage4"] = TempData["ErrorMessage3"];
                    ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "Code", course.DepartmentId);
                    ViewBag.CourseId = new SelectList(db.Courses.Where(c => c.DepartmentId == course.DepartmentId), "CourseId", "Code", roomallocation.CourseId);
                    ViewBag.RoomId = new SelectList(db.Rooms, "RoomId", "Name", roomallocation.RoomId);
                    ViewBag.DayId = new SelectList(db.Days, "DayId", "Name", roomallocation.DayId);
                    return View(roomallocation);
                    //return RedirectToAction("Create");
                }

                try
                {
                    givenEnd = ConvertTimetoInt(roomallocation.EndTime);
                }
                catch (Exception anException)
                {
                    if (TempData["ErrorMessage3"] == null)
                    {
                        TempData["ErrorMessage2"] = anException.Message;
                    }
                    TempData["ErrorMessage5"] = TempData["ErrorMessage3"];
                    ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "Code", course.DepartmentId);
                    ViewBag.CourseId = new SelectList(db.Courses.Where(c => c.DepartmentId == course.DepartmentId), "CourseId", "Code", roomallocation.CourseId);
                    ViewBag.RoomId = new SelectList(db.Rooms, "RoomId", "Name", roomallocation.RoomId);
                    ViewBag.DayId = new SelectList(db.Days, "DayId", "Name", roomallocation.DayId);
                    return View(roomallocation);
                }

                if (givenEnd < givenfrom)
                {
                    TempData["Message"] = "Start Time must be before End Time (within 24 hours)";
                    ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "Code", course.DepartmentId);
                    ViewBag.CourseId = new SelectList(db.Courses.Where(c => c.DepartmentId == course.DepartmentId), "CourseId", "Code", roomallocation.CourseId);
                    ViewBag.RoomId = new SelectList(db.Rooms, "RoomId", "Name", roomallocation.RoomId);
                    ViewBag.DayId = new SelectList(db.Days, "DayId", "Name", roomallocation.DayId);
                    return View(roomallocation);
                }
                if (givenEnd == givenfrom)
                {
                    TempData["Message"] = "Start Time not be equal to End Time (within 24 hours)";
                    ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "Code", course.DepartmentId);
                    ViewBag.CourseId = new SelectList(db.Courses.Where(c => c.DepartmentId == course.DepartmentId), "CourseId", "Code", roomallocation.CourseId);
                    ViewBag.RoomId = new SelectList(db.Rooms, "RoomId", "Name", roomallocation.RoomId);
                    ViewBag.DayId = new SelectList(db.Days, "DayId", "Name", roomallocation.DayId);
                    return View(roomallocation);
                }

                if ((givenfrom < 0) || (givenEnd >= (24 * 60)))
                {
                    TempData["Message"] = "Please input time within 24 hours With format HH:MM";
                    ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "Code", course.DepartmentId);
                    ViewBag.CourseId = new SelectList(db.Courses.Where(c => c.DepartmentId == course.DepartmentId), "CourseId", "Code", roomallocation.CourseId);
                    ViewBag.RoomId = new SelectList(db.Rooms, "RoomId", "Name", roomallocation.RoomId);
                    ViewBag.DayId = new SelectList(db.Days, "DayId", "Name", roomallocation.DayId);
                    return View(roomallocation);
                }

                List<RoomAllocation> overLapList = new List<RoomAllocation>();

                var DayRoomAllocationList =
                db.RoomAllocations.Include(c => c.Course).Include(d => d.Day).Include(r => r.Room).Where(r => r.RoomId == roomallocation.RoomId && r.DayId == roomallocation.DayId)
                    .ToList();

                foreach (var aDayroomAllocation in DayRoomAllocationList)
                {
                    //int OverlapFrom = 0;
                    //int OverlapEnd = 0;
                    //RoomAllocation overlap =new RoomAllocation();

                    int DbFrom = ConvertTimetoInt(aDayroomAllocation.StartTime);
                    int DbEnd = ConvertTimetoInt(aDayroomAllocation.EndTime);
                    //givenfrom = Convert.ToInt32(roomallocation.StartTime);
                    //givenEnd =  Convert.ToInt32(roomallocation.EndTime);
                    if (
                            ((DbFrom <= givenfrom) && (givenfrom < DbEnd))
                            || ((DbFrom < givenEnd) && (givenEnd <= DbEnd))
                            || ((givenfrom <= DbFrom) && (DbEnd <= givenEnd))
                        )
                    {
                        overLapList.Add(aDayroomAllocation);
                    }

                }
                if (overLapList.Count == 0)
                {
                    //TempData["msg"] = "Time Successfull";
                    db.RoomAllocations.Add(roomallocation);
                    db.SaveChanges();
                    TempData["Message"] = "Room : " + room.Name + " has been successfully allocated "
                    + " for course : " + course.Code + " From " + roomallocation.StartTime
                    + " to " + roomallocation.EndTime + " on " + day.Name;
                }
                else
                {
                    string message = "Room : " + room.Name + " is Overlapped with : ";
                    foreach (var anOverlappedRoom in overLapList)
                    {
                        int dbFrom = ConvertTimetoInt(anOverlappedRoom.StartTime);
                        int dbEnd = ConvertTimetoInt(anOverlappedRoom.EndTime);

                        string overlapStart, overlapEnd;

                        if ((dbFrom <= givenfrom) && (givenfrom < dbEnd))
                            overlapStart = roomallocation.StartTime;
                        else
                            overlapStart = anOverlappedRoom.StartTime;

                        if ((dbFrom < givenEnd) && (givenEnd <= dbEnd))
                            overlapEnd = roomallocation.EndTime;
                        else
                            overlapEnd = anOverlappedRoom.EndTime;
                        message += " Course : " + anOverlappedRoom.Course.Code + " Start Time : "
                            + anOverlappedRoom.StartTime + " and End Time : "
                            + anOverlappedRoom.EndTime + " overlapped from : ";
                        message += overlapStart + " to " + overlapEnd;
                    }

                    TempData["Message"] = message + " on " + day.Name;

                    ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "Code");
                    ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "Code", course.DepartmentId);
                    ViewBag.CourseId = new SelectList(db.Courses.Where(c => c.DepartmentId == course.DepartmentId), "CourseId", "Code", roomallocation.CourseId);
                    ViewBag.RoomId = new SelectList(db.Rooms, "RoomId", "Name", roomallocation.RoomId);
                    ViewBag.DayId = new SelectList(db.Days, "DayId", "Name", roomallocation.DayId);
                    return View(roomallocation);
                }
                //}

                return RedirectToAction("Create");
            }

            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "Code", roomallocation.DepartmentId);
            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "Code", roomallocation.CourseId);
            ViewBag.RoomId = new SelectList(db.Rooms, "RoomId", "Name", roomallocation.RoomId);
            ViewBag.DayId = new SelectList(db.Days, "DayId", "Name", roomallocation.DayId);
            return View(roomallocation);
        }


        private int ConvertTimetoInt(string timeStr)
        {
            try
            {
                if (TimeFormateCheck(timeStr))
                {
                    if (timeStr.Length == 4)
                    {
                        timeStr = "0" + timeStr;
                    }
                    string hour = timeStr[0].ToString() + timeStr[1];
                    string minute = timeStr[3].ToString() + timeStr[4];

                    if (Convert.ToInt32(minute) > 59 || Convert.ToInt32(minute) < 0)
                    {
                        TempData["ErrorMessage3"] = "Minites must be equal or less then 59";
                        throw new Exception();
                    }

                    int time = (Convert.ToInt32(hour) * 60);
                    time += Convert.ToInt32(minute);
                    return time;
                }
                else
                {
                    throw new Exception("Input time must be like HH:MM format");
                }
            }

            catch (FormatException aFormatException)
            {
                throw new FormatException(
                    "Input time must be like HH:MM format", aFormatException);
            }

            catch (IndexOutOfRangeException aRangException)
            {
                throw new IndexOutOfRangeException(
                    "Input time must be like HH:MM format", aRangException);
            }

            catch (Exception anException)
            {
                throw new Exception("Input time must be like HH:MM format", anException);
            }
        }

        private bool TimeFormateCheck(string timeStr)
        {
            char[] list = timeStr.ToCharArray();
            foreach (var aChar in list)
            {
                if (aChar == ':')
                {
                    return true;
                }
            }
            return false;
        }

        public ActionResult RoomAllocationView()
        {
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "Code");
             var CourseList = db.Courses.ToList();

            foreach (var aCourse in CourseList)
            {
                aCourse.RoomAllocationList
                    = db.RoomAllocations.Include(a => a.Room).Include(a => a.Day)
                    .Where(a => a.CourseId == aCourse.CourseId).ToList();
            }

            return View(CourseList);
        }

        // GET: RoomAllocations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RoomAllocation roomAllocation = db.RoomAllocations.Find(id);
            if (roomAllocation == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "Code", roomAllocation.CourseId);
            ViewBag.DayId = new SelectList(db.Days, "DayId", "Name", roomAllocation.DayId);
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "Code", roomAllocation.DepartmentId);
            ViewBag.RoomId = new SelectList(db.Rooms, "RoomId", "Name", roomAllocation.RoomId);
            return View(roomAllocation);
        }

        // POST: RoomAllocations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RoomAllocationId,DepartmentId,CourseId,RoomId,DayId,StartTime,EndTime")] RoomAllocation roomAllocation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(roomAllocation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "Code", roomAllocation.CourseId);
            ViewBag.DayId = new SelectList(db.Days, "DayId", "Name", roomAllocation.DayId);
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "Code", roomAllocation.DepartmentId);
            ViewBag.RoomId = new SelectList(db.Rooms, "RoomId", "Name", roomAllocation.RoomId);
            return View(roomAllocation);
        }

        // GET: RoomAllocations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RoomAllocation roomAllocation = db.RoomAllocations.Find(id);
            if (roomAllocation == null)
            {
                return HttpNotFound();
            }
            return View(roomAllocation);
        }

        // POST: RoomAllocations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RoomAllocation roomAllocation = db.RoomAllocations.Find(id);
            db.RoomAllocations.Remove(roomAllocation);
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


        public ActionResult LoadCourse(int? departmentId)
        {
            var courseList = db.Courses.Where(u => u.DepartmentId == departmentId).ToList();
            ViewBag.CourseId = new SelectList(courseList, "CourseId", "Name");
            return PartialView("~/Views/shared/_FilteredCourse.cshtml");

        }

        public PartialViewResult AllocatedRoomLoad(int? departmentId)
        {
            List<Course> courseList = null;
            if (departmentId != null)
            {
                courseList = db.Courses.Where(c => c.DepartmentId == departmentId).ToList();
                foreach (var aCourse in courseList)
                {
                    aCourse.RoomAllocationList
                        = db.RoomAllocations.Include(a => a.Room).Include(a => a.Day)
                        .Where(a => a.CourseId == aCourse.CourseId).ToList();
                }

                if (courseList.Count == 0)
                {
                    ViewBag.NoCourse = "This department has no course";
                }

                return PartialView("~/Views/shared/_RoomAllocationView.cshtml", courseList);
                
            }

           
           
            return PartialView("~/Views/shared/_RoomAllocationView.cshtml", courseList);
        }
    }
}
