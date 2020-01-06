namespace MyComicStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DropList : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DropLists",
                c => new
                    {
                        ListId = c.Int(nullable: false, identity: true),
                        ListName = c.String(),
                    })
                .PrimaryKey(t => t.ListId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.DropLists");
        }
    }
}
