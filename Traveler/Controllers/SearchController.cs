using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
            PlacePhoto place = new PlacePhoto();
            Photo p = new Photo();
            List<ApplicationUser> apList = db.Users.Where(u => u.UserName.Contains(query)).ToList();
            foreach(var i in apList)
            {
                model.Users.Add(i.ToUser());
            }
            model.Countries = db.Countries.Where(c => c.Name.Contains(query)).ToList();
            model.Cities = db.Cities.Where(c => c.Name.Contains(query)).ToList();

            if(model.Countries.Count > 0)
            {
                foreach (var i in model.Countries) {
                    p = db.Photos.Where(e => e.place.City.Country.Name == i.Name).FirstOrDefault();
                    if (p != null)
                    {
                        place.idPlace = i.CountryID;
                        place.photo = p;
                        model.CountriesPhoto.Add(place);
                    }
                }
            }
            if (model.Cities.Count > 0)
            {
                foreach (var i in model.Cities)
                {
                    p = db.Photos.Where(e => e.place.City.Name == i.Name).FirstOrDefault();
                    if (p != null)
                    {
                        place.idPlace = i.CityID;
                        place.photo = p;
                        model.CitiesPhoto.Add(place);
                    }
                }
            }

            return View(model);
        }
        public ActionResult Country(string countryName )
        {
            if(countryName == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            List<Place> places = db.Places.Where(e => e.City.Country.Name == countryName).ToList();
            return View(places);
        }
        public ActionResult City(string cityName)
        {
            if (cityName == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            List<Place> places = db.Places.Where(e => e.City.Name == cityName).ToList();
            return View(places);
        }
    }
}