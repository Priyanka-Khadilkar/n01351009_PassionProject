using Find_A_Restaurant.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Find_A_Restaurant.Data
{
    public class FindRestaurantsContext : DbContext
    {

        public FindRestaurantsContext() : base("name=FindRestaurantsContext")
        {
        }

        public System.Data.Entity.DbSet<Cuisine> Cuisines { get; set; }
        public System.Data.Entity.DbSet<Region> Regions { get; set; }
        public System.Data.Entity.DbSet<Restaurant> Restaurants { get; set; }
        public System.Data.Entity.DbSet<User> Users { get; set; }

    }
}