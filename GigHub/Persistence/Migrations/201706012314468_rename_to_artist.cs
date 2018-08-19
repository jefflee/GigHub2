namespace GigHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class rename_to_artist : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Gigs", name: "ApplicationUser_Id", newName: "Artist_Id");
            RenameIndex(table: "dbo.Gigs", name: "IX_ApplicationUser_Id", newName: "IX_Artist_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Gigs", name: "IX_Artist_Id", newName: "IX_ApplicationUser_Id");
            RenameColumn(table: "dbo.Gigs", name: "Artist_Id", newName: "ApplicationUser_Id");
        }
    }
}
