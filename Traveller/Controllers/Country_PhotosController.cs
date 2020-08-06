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
    public class Country_PhotosController : Controller
    {
        private DB db = new DB();

        // GET: Country_Photos
        public ActionResult Index()
        {
            var country_Photos = db.Country_Photos.Include(c => c.Country);
            return View(country_Photos.ToList());
        }

        // GET: Country_Photos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Country_Photos country_Photos = db.Country_Photos.Find(id);
            if (country_Photos == null)
            {
                return HttpNotFound();
            }
            return View(country_Photos);
        }

        // GET: Country_Photos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Country_Photos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Description,Country_ID")] Country_Photos country_Photos,HttpPostedFileBase Photo)
        {
            try
            {
                if (ModelState.IsValid && Photo != null)
                {
                    country_Photos.Description_en = DEL.TranslateText(country_Photos.Description, "ar|en");
                    db.Country_Photos.Add(country_Photos);
                    db.SaveChanges();
                    Photo.SaveAs(Server.MapPath("~/Uploads/Country_Photos/" + country_Photos.ID + ".jpg"));
                    ViewBag.Done = "تم الاضافة";
                }
            }catch(Exception ex)
            {
                ViewBag.error = ex.Message;
            }

            return View(country_Photos);
        }

        // GET: Country_Photos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Country_Photos country_Photos = db.Country_Photos.Find(id);
            if (country_Photos == null)
            {
                return HttpNotFound();
            }
            return View(country_Photos);
        }

        // POST: Country_Photos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Description,Country_ID")] Country_Photos country_Photos,HttpPostedFileBase Photo)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    country_Photos.Description_en = DEL.TranslateText(country_Photos.Description, "ar|en");
                    db.Entry(country_Photos).State = EntityState.Modified;
                    db.SaveChanges();
                    if (Photo != null)
                    {
                        Photo.SaveAs(Server.MapPath("~/Uploads/Country_Photos/" + country_Photos.ID + ".jpg"));
                    }
                    ViewBag.Done = "تم التعديل";
                }
            }catch(Exception ex)
            {
                ViewBag.error = ex.Message;
            }
            return View(country_Photos);
        }

        // POST: Country_Photos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Country_Photos country_Photos = db.Country_Photos.Find(id);
            db.Country_Photos.Remove(country_Photos);
            db.SaveChanges();
            FileInfo F = new FileInfo(Server.MapPath("~/Uploads/Country_Photos/" + id+ ".jpg"));
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
