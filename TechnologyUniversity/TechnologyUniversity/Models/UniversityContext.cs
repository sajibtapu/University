using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace TechnologyUniversity.Models
{
    public class UniversityContext:DbContext
    {
        public UniversityContext() : base("UniversityDbContext")
        {
        }

        public DbSet<Department> Departments { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Semester> Semesters { get; set; }
        public DbSet<Student> Students { set; get; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Designation> Designations { get; set; }
        public DbSet<AssignCourse> AssignCourses { get; set; }



        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
        }

        public System.Data.Entity.DbSet<TechnologyUniversity.Models.RoomAllocation> RoomAllocations { get; set; }

        public System.Data.Entity.DbSet<TechnologyUniversity.Models.Day> Days { get; set; }

        public System.Data.Entity.DbSet<TechnologyUniversity.Models.Room> Rooms { get; set; }

        public System.Data.Entity.DbSet<TechnologyUniversity.Models.EnrollCourse> EnrollCourses { get; set; }

        public System.Data.Entity.DbSet<TechnologyUniversity.Models.ResultEntry> ResultEntries { get; set; }

        public System.Data.Entity.DbSet<TechnologyUniversity.Models.Grade> Grades { get; set; }

       
       

    }
}