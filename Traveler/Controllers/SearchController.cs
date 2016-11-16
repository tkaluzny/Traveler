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
            List<Travel> li = new List<Travel>();
            if (search.Country!= null)
            {
                li = db.Travels.Where(e => e.Cities.FirstOrDefault().Country.Name == search.Country).ToList();
            }
            else if (search.City != null)
            {
                li = db.Travels.Where(e => e.Cities.FirstOrDefault().Name == search.City).ToList();
            }
            else if(search.UserName != null)
            {
                string user = db.Users.FirstOrDefault(e => e.UserName == search.UserName).Id;
                li = db.Travels.Where(e => e.UserID == user).ToList();
            }
            TempData["travels"] = li;
            return RedirectToAction("Index", "Travels");
        }
    }
}