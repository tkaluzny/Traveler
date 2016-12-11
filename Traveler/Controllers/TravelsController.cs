using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
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
            if (TempData["travels"] != null)
            {
                return View(TempData["travels"]);
            }
            string CurrentUserID = User.Identity.Name;
            List<Travel> travel = db.Travels.Include(e => e.Places).Where(t => t.UserID == CurrentUserID).ToList();
            return View(travel);
        }


        // GET: Travels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                ShowTravelViewModel show = PrepareModel(id);
                if (show == null)
                {
                    return View("~/Views/Shared/AccessDeniedError.cshtml");
                }
                return View(show);
            }
        }
        [HttpPost]
        public ActionResult Details(ShowTravelViewModel showTravel)
        {
            if (showTravel == null)
            {
                return View("~/Views/Shared/AccessDeniedError.cshtml");
            }
            db.Entry(showTravel.comment).State = EntityState.Modified;
            return View(showTravel);
        }

        // GET: Travels/Show/5
        [AllowAnonymous]
        public ActionResult Show(int id)
        {
            ShowTravelViewModel showTravel = PrepareModel(id);
            if (showTravel == null)
            {
                return View("~/Views/Shared/AccessDeniedError.cshtml");
            }
            return View(showTravel);
        }
        [HttpPost]
        public ActionResult Show(int id, ShowTravelViewModel showTravel)
        {
            if (ModelState.IsValid)
            {
                Comment newComment = showTravel.comment;
                newComment.Date = DateTime.Now;
                newComment.UserID = User.Identity.GetUserName();
                db.Comments.Add(newComment);
                db.SaveChanges();
            }

            return View(PrepareModel(id));
        }

        // GET: Travels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Travels/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,Description")] Travel travel, FormCollection formCollection, HttpPostedFileBase[] file)
        {
            if (ModelState.IsValid)
            {
                travel.UserID = User.Identity.Name;

                db.Travels.Add(travel);
                db.SaveChanges();

                return RedirectToAction("Details", new { id = travel.TravelID });
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
        public ActionResult Edit([Bind(Include = "TravelID,Name,Description,UserID")] Travel travel, HttpPostedFileBase[] file)
        {
            if (ModelState.IsValid)
            {
                travel.UserID = User.Identity.Name;
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
            return travel.UserID == User.Identity.Name;
        }
        private ShowTravelViewModel PrepareModel(int? id)
        {
            Travel travel = db.Travels.Find(id);
            if (travel != null)
            {
                ShowTravelViewModel showTravel = new ShowTravelViewModel();
                showTravel.travel = travel;
                showTravel.comment = new Comment { TravelID = travel.TravelID, UserID = User.Identity.Name };
                showTravel.Comments = new List<Comment>(db.Comments.Where(e => e.TravelID == travel.TravelID).ToList());
                showTravel.comment.Date = DateTime.Now;
                return showTravel;
            }
            return null;
        }
    }
}
