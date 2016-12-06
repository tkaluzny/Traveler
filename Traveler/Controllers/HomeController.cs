using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;
using Traveler.Models;

namespace Traveler.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            int count = db.Travels.Count();
            List<Travel> travels = new List<Travel>();
            for (int i=0;i<6;i++)
            {
                travels.Add(db.Travels.Include(x => x.Places).AsEnumerable().ElementAt(count - 1 - i));
                if (count - 1 - i == 0) break;
            }
            ViewBag.travels = travels;
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}