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
        public ActionResult Index(string query)
        {
            Search model = new Models.Search {Query = query };

            model.Users = db.Users.Where(u => u.UserName.Contains(query)).Select(u => u.UserName).ToList();
            model.Countries = db.Countries.Where(c => c.Name.Contains(query)).ToList();
            model.Cities = db.Cities.Where(c => c.Name.Contains(query)).ToList();

            return View(model);
        }
    }
}