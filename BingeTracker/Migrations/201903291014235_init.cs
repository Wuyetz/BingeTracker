namespace BingeTracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Movies",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        IdImdb = c.String(),
                        Title = c.String(),
                        ReleaseYear = c.String(),
                        Genres = c.String(),
                        ImdbRating = c.String(),
                        Votes = c.Int(nullable: false),
                        AddedToMyMovies = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.MyMovies",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        IdImdb = c.String(),
                        Title = c.String(),
                        ReleaseYear = c.String(),
                        Genres = c.String(),
                        ImdbRating = c.String(),
                        Votes = c.Int(nullable: false),
                        MyRating = c.String(),
                        Note = c.String(maxLength: 60),
                        Watched = c.String(),
                        UserID = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.MyMovies");
            DropTable("dbo.Movies");
        }
    }
}
