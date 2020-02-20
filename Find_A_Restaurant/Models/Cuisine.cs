using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Find_A_Restaurant.Models
{
    public class Cuisine
    {
        [Key]
        public int CuisineID { get; set; }

        public string CuisineName { get; set; }

        public ICollection<Restaurant> RestaurantCuisines { get; set; }

    }
}