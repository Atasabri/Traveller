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
    public class HotelsController : Controller
    {
        private DB db = new DB();

        // GET: Hotels
        public ActionResult Index()
        {
            var hotels = db.Hotels.Include(h => h.City);
            return View(hotels.ToList());
        }

        // GET: Hotels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Hotel hotel = db.Hotels.Find(id);
            if (hotel == null)
            {
                return HttpNotFound();
            }
            return View(hotel);
        }

        // GET: Hotels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Hotels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Link,City_ID")] Hotel hotel,HttpPostedFileBase Photo)
        {
            try
            {
                if (ModelState.IsValid && Photo != null)
                {
                    hotel.Name_en = DEL.TranslateText(hotel.Name, "ar|en");
                    db.Hotels.Add(hotel);
                    db.SaveChanges();
                    Photo.SaveAs(Server.MapPath("~/Uploads/Hotels/" + hotel.ID + ".jpg"));
                    ViewBag.Done = "تم الاضافة بنجاح ";
                }
            }catch(Exception ex)
            {
                ViewBag.error = ex.Message;
            }
            return View(hotel);
        }

        // GET: Hotels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Hotel hotel = db.Hotels.Find(id);
            if (hotel == null)
            {
                return HttpNotFound();
            }
            return View(hotel);
        }

        // POST: Hotels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Link,City_ID")] Hotel hotel,HttpPostedFileBase Photo)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    hotel.Name_en = DEL.TranslateText(hotel.Name, "ar|en");
                    db.Entry(hotel).State = EntityState.Modified;
                    db.SaveChanges();
                    if (Photo != null)
                    {
                        Photo.SaveAs(Server.MapPath("~/Uploads/Hotels/" + hotel.ID + ".jpg"));
                    }
                    ViewBag.Done = "تم التعديل بنجاح ";
                }
            }catch(Exception ex)
            {
                ViewBag.error = ex.Message;
            }
            return View(hotel);
        }
        // POST: Hotels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Hotel hotel = db.Hotels.Find(id);
            db.Hotels.Remove(hotel);
            db.SaveChanges();
            FileInfo F = new FileInfo(Server.MapPath("~/Uploads/Hotels/" + id + ".jpg"));
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
