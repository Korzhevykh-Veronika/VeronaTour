namespace VeronaTour.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Implimitation_Logging : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ExceptionDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CallSite = c.String(),
                        Date = c.String(),
                        Exception = c.String(),
                        Level = c.String(),
                        Logger = c.String(),
                        MachineName = c.String(),
                        Message = c.String(),
                        StackTrace = c.String(),
                        Thread = c.String(),
                        Username = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ExceptionDetails");
        }
    }
}
