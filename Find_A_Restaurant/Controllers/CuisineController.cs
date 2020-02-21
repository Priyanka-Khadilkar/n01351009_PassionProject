using Find_A_Restaurant.Data;
using Find_A_Restaurant.Models;
using Find_A_Restaurant.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Find_A_Restaurant.Controllers
{
    public class CuisineController : Controller
    {
        //Db context
        private FindRestaurantsContext db = new FindRestaurantsContext();
        public ActionResult List()
        {
            //List all cuisines 
            List<Cuisine> Cuisines = db.Cuisines.ToList();

            //I was learning how to use view model.I know we can do with List<Cuisine> in the view
            ListCuisine ViewModel = new ListCuisine();
            ViewModel.cuisines = Cuisines;

            //Assign viewmodel to the view
            return View(ViewModel);
        }

        [HttpPost]
        public ActionResult List(string SearchText)
        {
            //Create ViewModel of cuisines
            ListCuisine ViewModel = new ListCuisine();

            //Select List of all cuisines
            List<Cuisine> SearchedRestaurants = db.Cuisines.ToList();

            Debug.WriteLine("Search keyword to search: " + SearchText);

            //Search results according to search keyword
            if (SearchText != "")
            {
                SearchedRestaurants = SearchedRestaurants.Where(x => x.CuisineName.ToLower() == SearchText.Trim().ToLower()).ToList();
            }

            //Assign Values to ViewModel.
            ViewModel.cuisines = SearchedRestaurants;

            //Assign viewmodel to the view
            return View(ViewModel);
        }

        public ActionResult New()
        {
            //Return new view.   
            return View();
        }

        [HttpPost]
        public ActionResult New(string CuisineName)
        {
            //Create Object of the cuisine
            Cuisine Cuisine = new Cuisine();

            Debug.WriteLine("Cuisine Name to add: " + CuisineName);

            //Assign value to the Cuisine object
            Cuisine.CuisineName = CuisineName;
            db.Cuisines.Add(Cuisine);

            //Save Added Cuisine into database
            db.SaveChanges();

            //Redirect to list of cuisine 
            return RedirectToAction("List");
        }

        public ActionResult Update(int id)
        {
            Debug.WriteLine("Update the Cuisine Id: " + id);

            //Select Cuisine to Update.
            Cuisine cuisine = db.Cuisines.SingleOrDefault(x => x.CuisineID == id);

            //Pass selected Cuisine to View.
            return View(cuisine);
        }

        [HttpPost]
        public ActionResult Update(int id, string CuisineName)
        {
            Debug.WriteLine("Update the Cuisine Id: " + id + "With name : " + CuisineName);

            //Select Cuisine to update
            Cuisine Cuisine = db.Cuisines.Single(u => u.CuisineID == id);

            //Update the Cuisine Name 
            Cuisine.CuisineName = CuisineName;

            //Save Updated Cuisine into database
            db.SaveChanges();
            return RedirectToAction("List");
        }

        public ActionResult Delete(int id)
        {
            //I was trying to do with EF6 LINQ query, i was facing some errors to delete so I did it with following apporach 
            //TODO - With EF6 query
            //Delete attached all restaurants to the cuisines
            string RestaurantsDeleteQuery = "delete from RestaurantCuisines where Cuisine_CuisineID = @id";
            SqlParameter RestaurantsDeleteparam = new SqlParameter("@id", id);
            db.Database.ExecuteSqlCommand(RestaurantsDeleteQuery, RestaurantsDeleteparam);

            // Delete Cuisine
            string DeleteCuisineQuery = "delete from Cuisines where CuisineID = @id";
            SqlParameter DeleteCuisineParam = new SqlParameter("@id", id);
            db.Database.ExecuteSqlCommand(DeleteCuisineQuery, DeleteCuisineParam);

            //return to List of  cuisines.
            return RedirectToAction("List");
        }

    }
}