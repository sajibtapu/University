namespace TechnologyUniversity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateAll : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Courses", "Name", c => c.String(nullable: false));
            AddColumn("dbo.Teachers", "CreditTaken", c => c.Double(nullable: false));
            AlterColumn("dbo.Teachers", "Address", c => c.String());
            DropColumn("dbo.Courses", "Title");
            DropColumn("dbo.Courses", "AssignedToTeachersName");
            DropColumn("dbo.Teachers", "CreditToBeTaken");
            DropColumn("dbo.Teachers", "CreditRemaining");
            DropColumn("dbo.Teachers", "CreditsHaveTaken");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Teachers", "CreditsHaveTaken", c => c.Double(nullable: false));
            AddColumn("dbo.Teachers", "CreditRemaining", c => c.Double(nullable: false));
            AddColumn("dbo.Teachers", "CreditToBeTaken", c => c.Double(nullable: false));
            AddColumn("dbo.Courses", "AssignedToTeachersName", c => c.String());
            AddColumn("dbo.Courses", "Title", c => c.String(nullable: false));
            AlterColumn("dbo.Teachers", "Address", c => c.String(nullable: false));
            DropColumn("dbo.Teachers", "CreditTaken");
            DropColumn("dbo.Courses", "Name");
        }
    }
}
