using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Traveller.Models;
using System.IO;

namespace Traveller.Controllers
{
    [Authorize]
    public class AboutUsPhotoesController : Controller
    {
        private DB db = new DB();

        // GET: AboutUsPhotoes
        public ActionResult Index()
        {
            return View(db.AboutUsPhotos.ToList());
        }

        // GET: AboutUsPhotoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            AboutUsPhoto aboutUsPhoto = db.AboutUsPhotos.Find(id);
            if (aboutUsPhoto == null)
            {
                return HttpNotFound();
            }
            return View(aboutUsPhoto);
        }

        // GET: AboutUsPhotoes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AboutUsPhotoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Description")] AboutUsPhoto aboutUsPhoto,HttpPostedFileBase Photo)
        {
            try
            {
                if (ModelState.IsValid && Photo != null)
                {
                    aboutUsPhoto.Description_en = DEL.TranslateText(aboutUsPhoto.Description, "ar|en");
                    db.AboutUsPhotos.Add(aboutUsPhoto);
                    db.SaveChanges();
                    DEL.PhotoCompress(Server.MapPath("~/Uploads/AboutUsPhotos/" + aboutUsPhoto.ID + ".jpg"), Photo);
                    ViewBag.Done = "تم اضافة الصورة بنجاح .";
                }
            }catch (Exception ex)
            {
                ViewBag.error = ex.Message;
            }
            return View(aboutUsPhoto);
        }

        // GET: AboutUsPhotoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            AboutUsPhoto aboutUsPhoto = db.AboutUsPhotos.Find(id);
            if (aboutUsPhoto == null)
            {
                return HttpNotFound();
            }
            return View(aboutUsPhoto);
        }

        // POST: AboutUsPhotoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Description")] AboutUsPhoto aboutUsPhoto,HttpPostedFileBase Photo)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    aboutUsPhoto.Description_en = DEL.TranslateText(aboutUsPhoto.Description, "ar|en");
                    db.Entry(aboutUsPhoto).State = EntityState.Modified;
                    db.SaveChanges();
                    if (Photo != null)
                    {
                        DEL.PhotoCompress(Server.MapPath("~/Uploads/AboutUsPhotos/" + aboutUsPhoto.ID + ".jpg"), Photo);
                    }
                    ViewBag.Done = "تم تعديل الصورة بنجاح .";
                }
            }catch(Exception ex)
            {
                ViewBag.error = ex.Message;
            }
            return View(aboutUsPhoto);
        }

        // POST: AboutUsPhotoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AboutUsPhoto aboutUsPhoto = db.AboutUsPhotos.Find(id);
            db.AboutUsPhotos.Remove(aboutUsPhoto);
            db.SaveChanges();
            FileInfo F = new FileInfo(Server.MapPath("~/Uploads/AboutUsPhotos/" + id + ".jpg"));
            if(F.Exists)
            {
                F.Delete();
            }
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
    }
}
