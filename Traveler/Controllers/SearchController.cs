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
   
        [HttpPost]
        public ActionResult Find([Bind(Include = "Country,City,UserName")]Search search)
        {
            
            Country _country = new Country();
            City _city = new City();
            List<Place> li = new List<Place>();
            if (search.Country != null)
            {
                int id = db.Places.Where(e => e.City.Country.Name == search.Country).FirstOrDefault().PlaceID;
                return RedirectToAction("Show", "Travels", new { ID = id } );
            }
            else if (search.City != null)
            {
                int id = db.Places.Where(e => e.City.Name == search.City).FirstOrDefault().PlaceID;
                return RedirectToAction("Show", "Travels", new { ID = id });
            }
            else if (search.UserName != null)
            {
                string user = db.Users.FirstOrDefault(e => e.UserName == search.UserName).UserName;
                return RedirectToAction("Show", "Users", new { id = user });
            }
            return RedirectToAction("Index", "Home");
        }
    }
}