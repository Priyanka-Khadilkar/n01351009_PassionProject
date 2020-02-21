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
    public class RegionController : Controller
    {
        //Db context
        private FindRestaurantsContext db = new FindRestaurantsContext();
        public ActionResult List()
        {
            //Select all list
            List<Region> Regions = db.Regions.ToList();
            Debug.WriteLine("Region List data Loaded...");

            //Assign model to the view
            return View(Regions);
        }

        [HttpPost]
        public ActionResult List(string SearchText)
        {
            //List all regions
            List<Region> SearchedRegions = db.Regions.ToList();

            //Search results according to search keyword
            if (SearchText != "")
            {
                Debug.WriteLine("SearchText for region List " + SearchText);
                SearchedRegions = SearchedRegions.Where(x => x.RegionName.ToLower() == SearchText.Trim().ToLower()).ToList();
            }

            //Assign viewmodel to the view
            return View(SearchedRegions);
        }

        public ActionResult New()
        {
            return View();
        }

        [HttpPost]
        public ActionResult New(string RegionName)
        {
            Region Region = new Region();

            //Add Region into the database
            Debug.WriteLine("Adding Region :" + RegionName);
            Region.RegionName = RegionName.Trim();
            db.Regions.Add(Region);

            //Save Added Region into database
            db.SaveChanges();

            //Redirect to the list of Regions
            return RedirectToAction("List");
        }

        public ActionResult Update(int id)
        {
            //Select Region to update 
            Debug.WriteLine("Region Id is :" + id);
            Region region = db.Regions.SingleOrDefault(x => x.RegionID == id);
            //Pass Region to view
            return View(region);
        }

        [HttpPost]
        public ActionResult Update(int id, string RegionName)
        {
            //Select Region to Update
            Region Region = db.Regions.Single(u => u.RegionID == id);

            Debug.WriteLine("latest region name :" + RegionName);

            //Assign updated region name
            Region.RegionName = RegionName;

            //Save Updated Region into database
            db.SaveChanges();

            //Redirect to list of region name
            return RedirectToAction("List");
        }

        public ActionResult Delete(int id)
        {
            //I was trying to do with EF6 LINQ query, i was facing some errors to delete so I did it with following apporach 
            //TODO - With EF6 query
            // Delete Region
            string DeleteRegionQuery = "delete from Regions where RegionID = @id";
            SqlParameter DeleteRegionParam = new SqlParameter("@id", id);

            Debug.WriteLine("SQL query to delete Region: " + DeleteRegionQuery);
            db.Database.ExecuteSqlCommand(DeleteRegionQuery, DeleteRegionParam);

            //Redirect to list of Region
            return RedirectToAction("List");
        }

        public ActionResult Show(int id)
        {
            //Select region to show all details
            Region region = db.Regions.Include("RestaurantRegions").SingleOrDefault(x => x.RegionID == id);

            Debug.WriteLine("Region id to show : " + id);
            return View(region);
        }

    }
}