namespace Find_A_Restaurant.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Restaurant : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cuisines",
                c => new
                    {
                        CuisineID = c.Int(nullable: false, identity: true),
                        CuisineName = c.String(),
                    })
                .PrimaryKey(t => t.CuisineID);
            
            CreateTable(
                "dbo.Restaurants",
                c => new
                    {
                        RestaurantID = c.Int(nullable: false, identity: true),
                        RestaurantName = c.String(),
                        RestaurantStreetAddress = c.String(),
                        RestaurantPostalCode = c.String(),
                        RestaurantAbout = c.String(),
                        RegionID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RestaurantID)
                .ForeignKey("dbo.Regions", t => t.RegionID, cascadeDelete: true)
                .Index(t => t.RegionID);
            
            CreateTable(
                "dbo.Regions",
                c => new
                    {
                        RegionID = c.Int(nullable: false, identity: true),
                        RegionName = c.String(),
                    })
                .PrimaryKey(t => t.RegionID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserID = c.Int(nullable: false, identity: true),
                        UserFirstName = c.Int(nullable: false),
                        UserLastName = c.Int(nullable: false),
                        UserName = c.String(),
                        UserEmail = c.String(),
                        UserPassword = c.String(),
                        UserPhoneNumber = c.String(),
                    })
                .PrimaryKey(t => t.UserID);
            
            CreateTable(
                "dbo.RestaurantCuisines",
                c => new
                    {
                        Restaurant_RestaurantID = c.Int(nullable: false),
                        Cuisine_CuisineID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Restaurant_RestaurantID, t.Cuisine_CuisineID })
                .ForeignKey("dbo.Restaurants", t => t.Restaurant_RestaurantID, cascadeDelete: true)
                .ForeignKey("dbo.Cuisines", t => t.Cuisine_CuisineID, cascadeDelete: true)
                .Index(t => t.Restaurant_RestaurantID)
                .Index(t => t.Cuisine_CuisineID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Restaurants", "RegionID", "dbo.Regions");
            DropForeignKey("dbo.RestaurantCuisines", "Cuisine_CuisineID", "dbo.Cuisines");
            DropForeignKey("dbo.RestaurantCuisines", "Restaurant_RestaurantID", "dbo.Restaurants");
            DropIndex("dbo.RestaurantCuisines", new[] { "Cuisine_CuisineID" });
            DropIndex("dbo.RestaurantCuisines", new[] { "Restaurant_RestaurantID" });
            DropIndex("dbo.Restaurants", new[] { "RegionID" });
            DropTable("dbo.RestaurantCuisines");
            DropTable("dbo.Users");
            DropTable("dbo.Regions");
            DropTable("dbo.Restaurants");
            DropTable("dbo.Cuisines");
        }
    }
}
