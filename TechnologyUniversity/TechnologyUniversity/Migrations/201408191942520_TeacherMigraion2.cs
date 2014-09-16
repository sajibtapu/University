namespace TechnologyUniversity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TeacherMigraion2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Teachers", "ContactNo", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Teachers", "ContactNo", c => c.Int(nullable: false));
        }
    }
}
