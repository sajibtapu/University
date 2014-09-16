namespace TechnologyUniversity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class enrollment : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EnrollCourses",
                c => new
                    {
                        EnrollCourseId = c.Int(nullable: false, identity: true),
                        StudentId = c.Int(nullable: false),
                        CourseId = c.Int(nullable: false),
                        EnrollDate = c.DateTime(nullable: false),
                        GradeName = c.String(),
                    })
                .PrimaryKey(t => t.EnrollCourseId)
                .ForeignKey("dbo.Courses", t => t.CourseId)
                .ForeignKey("dbo.Students", t => t.StudentId)
                .Index(t => t.StudentId)
                .Index(t => t.CourseId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EnrollCourses", "StudentId", "dbo.Students");
            DropForeignKey("dbo.EnrollCourses", "CourseId", "dbo.Courses");
            DropIndex("dbo.EnrollCourses", new[] { "CourseId" });
            DropIndex("dbo.EnrollCourses", new[] { "StudentId" });
            DropTable("dbo.EnrollCourses");
        }
    }
}
