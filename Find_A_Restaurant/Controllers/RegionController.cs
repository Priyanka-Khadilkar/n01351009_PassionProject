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
    public class RegionController : Controller
    {
        //Db context
        private FindRestaurantsContext db = new FindRestaurantsContext();
        public ActionResult List()
        {
            List<Region> Regions = db.Regions.ToList();

            //Assign model to the view
            return View(Regions);
        }

        [HttpPost]
        public ActionResult List(string SearchText)
        {
            List<Region> SearchedRegions = db.Regions.ToList();

            //Search results according to search keyword
            if (SearchText != "")
            {
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

            Region.RegionName = RegionName.Trim();
            db.Regions.Add(Region);

            //Save Added Region into database
            db.SaveChanges();

            return RedirectToAction("List");
        }

        public ActionResult Update(int id)
        {
            Region region = db.Regions.SingleOrDefault(x => x.RegionID == id);
            return View(region);
        }

        [HttpPost]
        public ActionResult Update(int id, string RegionName)
        {
            Region Region = db.Regions.Single(u => u.RegionID == id);
            Region.RegionName = RegionName;

            //Save Updated Region into database
            db.SaveChanges();
            return RedirectToAction("List");
        }

        public ActionResult Delete(int id)
        {
            //I was trying to do with EF6 LINQ query, i was facing some errors to delete so I did it with following apporach 
            //TODO - With EF6 query
            // Delete Region
            string DeleteRegionQuery = "delete from Regions where RegionID = @id";
            SqlParameter DeleteRegionParam = new SqlParameter("@id", id);
            db.Database.ExecuteSqlCommand(DeleteRegionQuery, DeleteRegionParam);

            return RedirectToAction("List");
        }

        public ActionResult Show(int id)
        {
            Region region = db.Regions.Include("RestaurantRegions").SingleOrDefault(x => x.RegionID == id);
            return View(region);
        }

    }
}