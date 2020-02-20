using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Find_A_Restaurant.Models
{
    public class Restaurant
    {
        [Key]
        public int RestaurantID { get; set; }

        public string RestaurantName { get; set; }

        public string RestaurantStreetAddress { get; set; }

        public string RestaurantPostalCode { get; set; }

        public string RestaurantAbout { get; set; }

        public int RegionID { get; set; }
        [ForeignKey("RegionID")]

        public virtual Region Region { get; set; }

        public virtual ICollection<Cuisine> Cuisines { get; set; }

    }
}