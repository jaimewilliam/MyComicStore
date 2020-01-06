namespace MyComicStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.CategoryId);
            
            CreateTable(
                "dbo.Comics",
                c => new
                    {
                        ComicId = c.Int(nullable: false, identity: true),
                        CategoryId = c.Int(nullable: false),
                        Title = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ImgUrl = c.String(),
                    })
                .PrimaryKey(t => t.ComicId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Comics");
            DropTable("dbo.Categories");
        }
    }
}
