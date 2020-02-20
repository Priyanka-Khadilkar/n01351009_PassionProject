using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Find_A_Restaurant.Models
{
    public class Region
    {
        [Key]
        public int RegionID { get; set; }

        public string RegionName { get; set; }

        public ICollection<Restaurant> RestaurantRegions { get; set; }
    }
}