using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Traveler.Models;

namespace Traveler.Controllers
{
    [Authorize]
    public class TravelsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Travels
        // Display travels which are assigned to current user
        public ActionResult Index()
        {
            string CurrentUserID = User.Identity.GetUserId();
            return View(db.Travels.Where(t => t.UserID == CurrentUserID).ToList());
        }

        // GET: Travels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Travel travel = db.Travels.Find(id);
            if (travel == null || !HasAccess(travel))
            {
                return View("~/Views/Shared/AccessDeniedError.cshtml");
            }
            return View(travel);
        }

        // GET: Travels/Create
        public ActionResult Create()
        {
            ViewData["Country"] = new SelectList(db.Countries.ToList(), "CountryID", "Name");
            return View();
        }

        // POST: Travels/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,Description")] Travel travel, FormCollection formCollection)
        {
            if (ModelState.IsValid)
            {
                // Assign current user id to travel.UserID property
                travel.UserID = User.Identity.GetUserId();
                travel.Cities = new HashSet<City>();

                //if (formCollection["CityID"] != null)
                //{
                //    string[] ids = formCollection["CityID"].Split(',');

                //    foreach (string id in ids)
                //    {
                //        City city = new City { CityID = int.Parse(id) };
                //        travel.Cities.Add(city);
                //    }
                //}
                
                db.Travels.Add(travel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(travel);
        }

        // GET: Travels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Travel travel = db.Travels.Find(id);
            if (travel == null || !HasAccess(travel))
            {
                return View("~/Views/Shared/AccessDeniedError.cshtml");
            }
            return View(travel);
        }

        // POST: Travels/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TravelID,Name,Description")] Travel travel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(travel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(travel);
        }

        // GET: Travels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Travel travel = db.Travels.Find(id);
            if (travel == null || !HasAccess(travel))
            {
                return View("~/Views/Shared/AccessDeniedError.cshtml");
            }
            return View(travel);
        }

        // POST: Travels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Travel travel = db.Travels.Find(id);
            db.Travels.Remove(travel);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private Boolean HasAccess(Travel travel)
        {
            return travel.UserID == User.Identity.GetUserId();
        }
    }
}
