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
    public class PhotosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        // GET: Photos/Create?placeID=1
        public ActionResult Create(int placeID)
        {
            Place place = db.Places.Find(placeID);
            ViewBag.PlaceID = place.PlaceID;
            ViewBag.PlaceName = place.Name;

            return View();
        }
  
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PhotoID,FileName,Description,PlaceID")] Photo photo, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {

                string dirPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "uploads");
                if (!Directory.Exists(dirPath))
                {
                    Directory.CreateDirectory(dirPath);
                }

                // Nazwa pliku jest wygenerowana przez Guid.NewGuid() i wpisana do ukrytego inputa w formularzu Create.cshtml
                photo.FileName = photo.FileName + Path.GetExtension(file.FileName);

                string filePath = Path.Combine(dirPath, photo.FileName);
                FileStream writeStream = new FileStream(filePath, FileMode.Create);
                BinaryWriter bw = new BinaryWriter(writeStream);
                byte[] buff = new byte[file.ContentLength];
                file.InputStream.Read(buff, 0, file.ContentLength);
                bw.Write(buff);
                bw.Close();

                db.Photos.Add(photo);
                db.SaveChanges();
                return RedirectToAction("Details", "Places", new { id = photo.PlaceID });
            }

            return Create(photo.PlaceID);
        }

        // GET: Photos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Photo photo = db.Photos.Find(id);
            if (photo == null)
            {
                return HttpNotFound();
            }

            return View(photo);
        }

        // POST: Photos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PhotoID,FileName,Description,PlaceID")] Photo photo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(photo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", "Places", new { id = photo.PlaceID });
            }
            ViewBag.PlaceID = new SelectList(db.Places, "PlaceID", "Name", photo.PlaceID);
            return View(photo);
        }

        // GET: Photos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Photo photo = db.Photos.Find(id);
            if (photo == null)
            {
                return HttpNotFound();
            }
            return View(photo);
        }

        // POST: Photos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Photo photo = db.Photos.Find(id);
            db.Photos.Remove(photo);
            db.SaveChanges();
            return RedirectToAction("Details", "Places", new { id = photo.PlaceID });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
