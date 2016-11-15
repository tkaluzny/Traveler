using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Traveler.Models;

namespace Traveler.Controllers
{
    public class SearchController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Search
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost, ActionName("Find")]
        public ActionResult Find([Bind(Include = "Country,City,TravelName")]Travel travel)
        {
            string country, city, travelName;
            Country _country = new Country();
            City _city = new City();
            int x = 0;
            //if (formCollection["Country"] != null)
            //{
            //    country = formCollection["Country"];
            //    _country = (Country)db.Countries.Where(t => t.Name == country);
            //    x++;
            //}
            //if (formCollection["City"] != null)
            //{
            //    city = formCollection["City"];
            //    _city = (City)db.Countries.Where(t => t.Name == city);
            //    x++;
            //}
            //if (formCollection["TravelName"] != null)
            //{
            //    travelName = formCollection["TravelName"];
            //    x++;
            //}

            return View(db.Travels.Where(e => e.Cities == _city).ToList());
        }
    }
}