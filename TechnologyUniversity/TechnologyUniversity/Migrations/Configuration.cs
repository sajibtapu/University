using System.Collections.Generic;
using TechnologyUniversity.Models;

namespace TechnologyUniversity.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<TechnologyUniversity.Models.UniversityContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(TechnologyUniversity.Models.UniversityContext context)
        {
            var departments = new List<Department>

            {
                new Department {Code = "ICT", Name = "Information and Communication Technology"},
                new Department {Code = "CSE", Name = "Computer Science and Engineering"},
                new Department {Code = "EEE", Name = "Electrical and Electronics Engineering"},
                new Department {Code = "BGE", Name = "Biotechnology and Genetic Engineering"},
                new Department {Code = "ECE", Name = "Electronics and Communication Engineering"},
                new Department {Code = "CE", Name = "Civil Engineering"}


            };

            departments.ForEach(d => context.Departments.AddOrUpdate(p => p.Code, d));
            context.SaveChanges();

            var semesters = new List<Semester>

            {

                new Semester {Name = "1st Semester"},
                new Semester {Name = "2nd Semester"},
                new Semester {Name = "3rd Semester"},
                new Semester {Name = "4th Semester"},
                new Semester {Name = "5th Semester"},
                new Semester {Name = "6th Semester"},
                new Semester {Name = "7th Semester"},
                new Semester {Name = "8th Semester"}
            };

            semesters.ForEach(d => context.Semesters.AddOrUpdate(p => p.Name, d));
            context.SaveChanges();

            var courses = new List<Course>

            {
                new Course
                {
                    Code = "ICT-101",
                    Name = "C programming",
                    Credit = 3.0,
                    Description = "First Programming Introduction",
                    DepartmentId = 1,
                    SemesterId = 1
                },
                new Course
                {
                    Code = "ICT-201",
                    Name = "Java programming",
                    Credit = 4.0,
                    Description = "Object Oriented  Programming Introduction",
                    DepartmentId = 1,
                    SemesterId = 3
                },
                new Course
                {
                    Code = "CSE-101",
                    Name = "C programming",
                    Credit = 3.5,
                    Description = "First Programming Introduction",
                    DepartmentId = 2,
                    SemesterId = 1
                },
                new Course
                {
                    Code = "CSE-201",
                    Name = "Java programming",
                    Credit = 4.5,
                    Description = "Object Oriented  Programming Introduction",
                    DepartmentId = 2,
                    SemesterId = 3
                },
                new Course
                {
                    Code = "EEE-101",
                    Name = "Elcetronics-1",
                    Credit = 4,
                    Description = "Basics of Electronics",
                    DepartmentId = 3,
                    SemesterId = 1
                },
                new Course
                {
                    Code = "EEE-201",
                    Name = "Elcetronics-2",
                    Credit = 4.5,
                    Description = "Advance Electronics",
                    DepartmentId = 3,
                    SemesterId = 2
                }

            };

            courses.ForEach(d => context.Courses.AddOrUpdate(p => p.Code, d));

            context.SaveChanges();
            var designations = new List<Designation>
            {
                new Designation {Name = "Chairman"},
                new Designation {Name = "Professor"},
                new Designation {Name = "Associate Professor"},
                new Designation {Name = "Assistant Professor"},
                new Designation {Name = "Lecturer"}
            };

            designations.ForEach(designation => context.Designations.AddOrUpdate(p => p.Name, designation));
            context.SaveChanges();

            var teachers = new List<Teacher>

            {
                new Teacher
                {
                    Name = "Mijan Mia",
                    Address = "Dhaka",
                    ContactNo = "01930823391",
                    Email = "m@gmail.com",
                    CreditTaken = 15,
                    DesignationId = 1,
                    DepartmentId = 1
                },
                new Teacher
                {
                    Name = "Korim Mia",
                    Address = "Dhaka",
                    ContactNo = "01930823391",
                    Email = "Korim@gmail.com",
                    CreditTaken = 15,
                    DesignationId = 1,
                    DepartmentId = 1
                },
                new Teacher
                {
                    Name = "Rohim Mia",
                    Address = "Dhaka",
                    ContactNo = "01930823391",
                    Email = "Rohim@gmail.com",
                    CreditTaken = 15,
                    DesignationId = 1,
                    DepartmentId = 2
                },
                new Teacher
                {
                    Name = "jamal Mia",
                    Address = "Dhaka",
                    ContactNo = "01930823391",
                    Email = "jamal@gmail.com",
                    CreditTaken = 15,
                    DesignationId = 1,
                    DepartmentId = 2
                },
                new Teacher
                {
                    Name = "Kamal Mia",
                    Address = "Dhaka",
                    ContactNo = "01930823391",
                    Email = "Kamal@gmail.com",
                    CreditTaken = 15,
                    DesignationId = 1,
                    DepartmentId = 3
                },
                new Teacher
                {
                    Name = "Sojol Mia",
                    Address = "Dhaka",
                    ContactNo = "01930823391",
                    Email = "Sojol@gmail.com",
                    CreditTaken = 15,
                    DesignationId = 1,
                    DepartmentId = 3
                }

            };

            teachers.ForEach(d => context.Teachers.AddOrUpdate(p => p.Email, d));
            context.SaveChanges();


            var rooms = new List<Room>
            {
                new Room {Name = "A-201"},
                new Room {Name = "A-202"},
                new Room {Name = "A-203"},
                new Room {Name = "A-301"},
                new Room {Name = "A-302"},
                new Room {Name = "A-303"},
                new Room {Name = "A-304"},


            };

            rooms.ForEach(d => context.Rooms.AddOrUpdate(p => p.Name, d));
            context.SaveChanges();

            var days = new List<Day>
            {
                new Day {Name = "SaturDay"},
                new Day {Name = "SunDay"},
                new Day {Name = "MonDay"},
                new Day {Name = "TuesDay"},
                new Day {Name = "WednesDay"},
                new Day {Name = "ThrusDay"},
                new Day {Name = "FriDay"},
            };

            days.ForEach(d => context.Days.AddOrUpdate(p => p.Name, d));
            context.SaveChanges();


            var grades = new List<Grade>
            {
                new Grade {Name = "A+"},
                new Grade {Name = "A"},
                new Grade {Name = "A-"},
                new Grade {Name = "B+"},
                new Grade {Name = "B"},
                new Grade {Name = "C"},
                new Grade {Name = "D"},
                new Grade {Name = "F"},

            };

            grades.ForEach(d => context.Grades.AddOrUpdate(p => p.Name, d));
            context.SaveChanges();

        }
    }
}
