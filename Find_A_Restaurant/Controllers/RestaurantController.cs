using Find_A_Restaurant.Data;
using Find_A_Restaurant.Models;
using Find_A_Restaurant.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Find_A_Restaurant.Controllers
{
    public class RestaurantController : Controller
    {
        //Db context
        private FindRestaurantsContext db = new FindRestaurantsContext();
        public ActionResult New()
        {
            //Query for selecting all cuisines for checkbox list on add restaurant page
            string AllCuisineQuery = "select * from Cuisines";
            List<Cuisine> AllCuisines = db.Cuisines.SqlQuery(AllCuisineQuery).ToList();

            //Query for selecting all regions for dropdown list on add restaurant page
            string AllRegionQuery = "select * from Regions";
            List<Region> AllRegions = db.Regions.SqlQuery(AllRegionQuery).ToList();

            //Assign cuisines and regions to the NewRestaurant view model
            NewRestaurant ViewModel = new NewRestaurant();
            ViewModel.cuisines = AllCuisines;
            ViewModel.regions = AllRegions;

            //Assign Viewmodel to the view
            return View(ViewModel);
        }

        [HttpPost]
        public ActionResult New(string Name, string StreetAddress, string PostalCode, string[] SelectedCuisines, int Regions, string About)
        {
            //create object of restaurant to add into database
            Restaurant Restaurant = new Restaurant();

            //Assign the values to the object
            Restaurant.RestaurantName = Name;
            Restaurant.RestaurantStreetAddress = StreetAddress;
            Restaurant.RestaurantPostalCode = PostalCode;

            //Selected dropdown region id will be set here
            Region selectedRegion = db.Regions.Where(x => x.RegionID == Regions).First();
            selectedRegion.RestaurantRegions = new List<Restaurant>();
            Restaurant.Region = selectedRegion;

            Restaurant.RestaurantAbout = About;

            //Get all Selected Cuisines and insert into database
            //string[] SelectedCuisineIds = SelectedCuisines.Split(',');
            List<Cuisine> ListCuisines = new List<Cuisine>();
            foreach (string CuisineId in SelectedCuisines)
            {
                int id = int.Parse(CuisineId);
                Cuisine Cuisine = db.Cuisines.SingleOrDefault(x => x.CuisineID == id);
                Cuisine.RestaurantCuisines = new List<Restaurant>();
                ListCuisines.Add(Cuisine);
            }

            //Assign all the Cuisines to the Restaurant Object
            Restaurant.Cuisines = ListCuisines;

            //Add the object into the database using enityframework method
            db.Restaurants.Add(Restaurant);

            //Save Added Restaurant into database
            db.SaveChanges();


            //Redirect to List Page
            return RedirectToAction("List");
        }

        public ActionResult Update(int id)
        {
            //Query for selecting all cuisines for checkbox list on add restaurant page
            string AllCuisineQuery = "select * from Cuisines";
            List<Cuisine> AllCuisines = db.Cuisines.SqlQuery(AllCuisineQuery).ToList();

            //Query for selecting all regions for dropdown list on add restaurant page
            string AllRegionQuery = "select * from Regions";
            List<Region> AllRegions = db.Regions.SqlQuery(AllRegionQuery).ToList();

            //Select All selected Cuisines
            List<Cuisine> cuisines = db.Restaurants.Where(X => X.RestaurantID == id).SelectMany(c => c.Cuisines).ToList();
            string[] SelectedCuisines = cuisines.Select(x => x.CuisineID.ToString()).ToArray();

            //Select the restaurant 
            Restaurant Restaurant = db.Restaurants.SingleOrDefault(x => x.RestaurantID == id);

            //Assign cuisines and regions to the UpdateRestaurant view model
            UpdateRestaurant ViewModel = new UpdateRestaurant();
            ViewModel.cuisines = AllCuisines;
            ViewModel.regions = AllRegions;
            ViewModel.restaurant = Restaurant;
            ViewModel.selectedCuisinesIds = SelectedCuisines;

            //Assign Viewmodel to the view
            return View(ViewModel);
        }

        [HttpPost]
        public ActionResult Update(int id, string Name, string StreetAddress, string PostalCode, string[] SelectedCuisines, int Regions, string About)
        {
            //create object of restaurant to update into database
            Restaurant Restaurant = new Restaurant();

            //Select the existing data of restaurant
            Restaurant = db.Restaurants.Include("Cuisines").Include("Region").Single(u => u.RestaurantID == id);

            //Assign the values to the object
            Restaurant.RestaurantName = Name;
            Restaurant.RestaurantStreetAddress = StreetAddress;
            Restaurant.RestaurantPostalCode = PostalCode;

            //Selected dropdown region id will be set here
            Region selectedRegion = db.Regions.Where(x => x.RegionID == Regions).First();
            //selectedRegion.RestaurantRegions = new List<Restaurant>();
            Restaurant.Region = selectedRegion;
            Restaurant.RegionID = Regions;

            Restaurant.RestaurantAbout = About;

            //Clear the old list of cuisines from the object
            Restaurant.Cuisines.Clear();

            //Get all Selected Cuisines and update into database
            List<Cuisine> ListCuisines = new List<Cuisine>();
            foreach (string CuisineId in SelectedCuisines)
            {
                int CuisineIdtoUpdate = int.Parse(CuisineId);
                Cuisine Cuisine = db.Cuisines.SingleOrDefault(x => x.CuisineID == CuisineIdtoUpdate);
                Cuisine.RestaurantCuisines = new List<Restaurant>();
                ListCuisines.Add(Cuisine);
            }

            //Assign all the Cuisines to the Restaurant Object
            Restaurant.Cuisines = ListCuisines;

            //Save Updated Restaurant into database
            db.SaveChanges();

            //Redirect to List Page
            return RedirectToAction("List");
        }

        public ActionResult Find()
        {
            //Query for selecting all cuisines for checkbox list on add restaurant page
            string AllCuisineQuery = "select * from Cuisines";
            List<Cuisine> AllCuisines = db.Cuisines.SqlQuery(AllCuisineQuery).ToList();

            //Query for selecting all regions for dropdown list on add restaurant page
            string AllRegionQuery = "select * from Regions";
            List<Region> AllRegions = db.Regions.SqlQuery(AllRegionQuery).ToList();

            //Assign cuisines and regions to the FindRestaurant view model
            FindRestaurant ViewModel = new FindRestaurant();
            ViewModel.restaurants = new List<Restaurant>();
            ViewModel.cuisines = AllCuisines;
            ViewModel.regions = AllRegions;

            //Assign Viewmodel to the view
            return View(ViewModel);
        }

        [HttpPost]
        public ActionResult Find(string SearchText, string[] SelectedCuisines, int? Regions)
        {
            //Query for selecting all cuisines for checkbox list on add restaurant page
            string AllCuisineQuery = "select * from Cuisines";
            List<Cuisine> AllCuisines = db.Cuisines.SqlQuery(AllCuisineQuery).ToList();

            //Query for selecting all regions for dropdown list on add restaurant page
            string AllRegionQuery = "select * from Regions";
            List<Region> AllRegions = db.Regions.SqlQuery(AllRegionQuery).ToList();

            //Assign cuisines and regions to the FindRestaurant view model
            FindRestaurant ViewModel = new FindRestaurant();
            ViewModel.cuisines = AllCuisines;
            ViewModel.regions = AllRegions;

            //Fetch all Restaurant
            List<Restaurant> SearchedRestaurants = db.Restaurants.Include("Cuisines").Include("Region").ToList();

            if (SearchText != "")
            {
                //Query for Search by keyword
                SearchedRestaurants = SearchedRestaurants.Where(x => x.Region.RegionName.ToLower().Contains(SearchText.ToLower()) || x.RestaurantName.ToLower().Contains(SearchText.ToLower()) || x.RestaurantPostalCode.ToLower().Contains(SearchText.ToLower())
                || x.RestaurantStreetAddress.ToLower().Contains(SearchText.ToLower()) || x.RestaurantAbout.ToLower().Contains(SearchText.ToLower()) || x.Cuisines.Any(r => r.CuisineName.ToLower().Contains(SearchText.ToLower()))).ToList();
            }

            if (SelectedCuisines != null)
            {
                //Query for Search by Cuisines
                SearchedRestaurants = SearchedRestaurants.Where(u => u.Cuisines.Any(r => SelectedCuisines.Contains(r.CuisineID.ToString()))).ToList();
            }

            if (Regions > 0)
            {
                //Query for Search by region
                SearchedRestaurants = SearchedRestaurants.Where(x => x.RegionID == Regions).ToList();
            }

            ViewModel.restaurants = SearchedRestaurants;

            //Assign Viewmodel to the view
            return View(ViewModel);
        }

        public ActionResult List()
        {
            //Query for selecting all cuisines for checkbox list on add restaurant page
            string AllCuisineQuery = "select * from Cuisines";
            List<Cuisine> AllCuisines = db.Cuisines.SqlQuery(AllCuisineQuery).ToList();

            //Query for selecting all regions for dropdown list on add restaurant page
            string AllRegionQuery = "select * from Regions";
            List<Region> AllRegions = db.Regions.SqlQuery(AllRegionQuery).ToList();

            //Assign cuisines and regions to the FindRestaurant view model
            FindRestaurant ViewModel = new FindRestaurant();

            List<Restaurant> Restaurants = db.Restaurants.Include("Cuisines").Include("Region").ToList();
            ViewModel.restaurants = Restaurants;

            ViewModel.cuisines = AllCuisines;
            ViewModel.regions = AllRegions;

            //Assign Viewmodel to the view
            return View(ViewModel);
        }

        [HttpPost]
        public ActionResult List(string SearchText, string[] SelectedCuisines, int? Regions)
        {
            //Query for selecting all cuisines for checkbox list on add restaurant page
            string AllCuisineQuery = "select * from Cuisines";
            List<Cuisine> AllCuisines = db.Cuisines.SqlQuery(AllCuisineQuery).ToList();

            //Query for selecting all regions for dropdown list on add restaurant page
            string AllRegionQuery = "select * from Regions";
            List<Region> AllRegions = db.Regions.SqlQuery(AllRegionQuery).ToList();

            //Assign cuisines and regions to the FindRestaurant view model
            FindRestaurant ViewModel = new FindRestaurant();
            ViewModel.cuisines = AllCuisines;
            ViewModel.regions = AllRegions;

            //Fetch all Restaurant
            List<Restaurant> SearchedRestaurants = db.Restaurants.Include("Cuisines").Include("Region").ToList();

            if (SearchText != "")
            {
                //Query for Search by keyword
                SearchedRestaurants = SearchedRestaurants.Where(x => x.Region.RegionName.ToLower().Contains(SearchText.ToLower()) || x.RestaurantName.ToLower().Contains(SearchText.ToLower()) || x.RestaurantPostalCode.ToLower().Contains(SearchText.ToLower())
                || x.RestaurantStreetAddress.ToLower().Contains(SearchText.ToLower()) || x.RestaurantAbout.ToLower().Contains(SearchText.ToLower()) || x.Cuisines.Any(r => r.CuisineName.ToLower().Contains(SearchText.ToLower()))).ToList();
            }

            if (SelectedCuisines != null)
            {
                //Query for Search by Cuisines
                SearchedRestaurants = SearchedRestaurants.Where(u => u.Cuisines.Any(r => SelectedCuisines.Contains(r.CuisineID.ToString()))).ToList();
            }

            if (Regions > 0)
            {
                //Query for Search by region
                SearchedRestaurants = SearchedRestaurants.Where(x => x.RegionID == Regions).ToList();
            }

            ViewModel.restaurants = SearchedRestaurants;

            //Assign Viewmodel to the view
            return View(ViewModel);
        }

        public ActionResult Show(int id)
        {

            //Select Restaurant according to id
            Restaurant Restaurant = db.Restaurants.Include("Cuisines").Include("Region").Where(x => x.RestaurantID == id).SingleOrDefault();

            //Assign Viewmodel to the view
            return View(Restaurant);
        }
        public ActionResult Delete(int id)
        {
            //I was trying to do with EF6 LINQ query, i was facing some errors to delete so I did it with following apporach 
            //TODO - With EF6 query
            //Delete attached all cuisines to the restaurant
            string CuisineDeleteQuery = "delete from RestaurantCuisines where Restaurant_RestaurantID = @id";
            SqlParameter CuisineDeleteParam = new SqlParameter("@id", id);
            db.Database.ExecuteSqlCommand(CuisineDeleteQuery, CuisineDeleteParam);

            // Delete Restaurant
            string DeleteRestaurant = "delete from Restaurants where RestaurantID = @id";
            SqlParameter DeleteRestaurantParam = new SqlParameter("@id", id);
            db.Database.ExecuteSqlCommand(DeleteRestaurant, DeleteRestaurantParam);

            return RedirectToAction("List");
        }

    }
}