using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Find_A_Restaurant.Models.ViewModel
{
    public class UpdateRestaurant
    {
        public List<Cuisine> cuisines { get; set; }

        public List<Region> regions { get; set; }

        public Restaurant restaurant { get; set; }

        public string[] selectedCuisinesIds { get; set; }
    }
}