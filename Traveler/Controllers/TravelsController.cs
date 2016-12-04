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
            if(TempData["travels"]!= null)
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
            List<Travel> travels = db.Travels.Include(x => x.Places).Where(t => t.TravelID == id).ToList();
            if (travels.Count == 0)
            {
                return View("~/Views/Shared/AccessDeniedError.cshtml");
            }
            return View(travels.First());
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
                // Assign current user id to travel.UserID property
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

        //private void Update(Travel travel, HttpPostedFileBase[] file)
        //{
        //    if(file != null)
        //    {
        //        foreach(var item in file)
        //        { 
        //            Photo img = new Photo();
        //            img.Name = travel.Name + "_" + travel.Photos.Count;

        //            string dirPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "uploads");
        //            if (!Directory.Exists(dirPath))
        //            {
        //                Directory.CreateDirectory(dirPath);
        //            }
        //            string path = Path.Combine(dirPath, img.Name+".jpg");
        //            FileStream  writeStream = new FileStream(path, FileMode.Create);
        //            BinaryWriter bw = new BinaryWriter(writeStream);
        //            byte[] buff = new byte[item.ContentLength];
        //            item.InputStream.Read(buff,0, item.ContentLength);
        //            bw.Write(buff);
        //            bw.Close();
        //            travel.Photos.Add(img);
        //            db.Entry(img).State = EntityState.Added;
        //        }
                
        //    }
        //}
    }
}
