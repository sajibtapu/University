namespace TechnologyUniversity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Allocation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Days",
                c => new
                    {
                        DayId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.DayId);
            
            CreateTable(
                "dbo.RoomAllocations",
                c => new
                    {
                        RoomAllocationId = c.Int(nullable: false, identity: true),
                        DepartmentId = c.Int(nullable: false),
                        CourseId = c.Int(nullable: false),
                        RoomId = c.Int(nullable: false),
                        DayId = c.Int(nullable: false),
                        StartTime = c.String(nullable: false),
                        EndTime = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.RoomAllocationId)
                .ForeignKey("dbo.Courses", t => t.CourseId)
                .ForeignKey("dbo.Days", t => t.DayId)
                .ForeignKey("dbo.Departments", t => t.DepartmentId)
                .ForeignKey("dbo.Rooms", t => t.RoomId)
                .Index(t => t.DepartmentId)
                .Index(t => t.CourseId)
                .Index(t => t.RoomId)
                .Index(t => t.DayId);
            
            CreateTable(
                "dbo.Rooms",
                c => new
                    {
                        RoomId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.RoomId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RoomAllocations", "RoomId", "dbo.Rooms");
            DropForeignKey("dbo.RoomAllocations", "DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.RoomAllocations", "DayId", "dbo.Days");
            DropForeignKey("dbo.RoomAllocations", "CourseId", "dbo.Courses");
            DropIndex("dbo.RoomAllocations", new[] { "DayId" });
            DropIndex("dbo.RoomAllocations", new[] { "RoomId" });
            DropIndex("dbo.RoomAllocations", new[] { "CourseId" });
            DropIndex("dbo.RoomAllocations", new[] { "DepartmentId" });
            DropTable("dbo.Rooms");
            DropTable("dbo.RoomAllocations");
            DropTable("dbo.Days");
        }
    }
}
