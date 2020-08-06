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
    public class Cars_CompaniesController : Controller
    {
        private DB db = new DB();

        // GET: Cars_Companies
        public ActionResult Index()
        {
            var cars_Companies = db.Cars_Companies.Include(c => c.Country);
            return View(cars_Companies.ToList());
        }

        // GET: Cars_Companies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Cars_Companies cars_Companies = db.Cars_Companies.Find(id);
            if (cars_Companies == null)
            {
                return HttpNotFound();
            }
            return View(cars_Companies);
        }

        // GET: Cars_Companies/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Link,Country_ID")] Cars_Companies cars_Companies,HttpPostedFileBase Photo)
        {
            try
            {
                if (ModelState.IsValid && Photo != null)
                {
                    cars_Companies.Name_en = DEL.TranslateText(cars_Companies.Name, "ar|en");
                    db.Cars_Companies.Add(cars_Companies);
                    db.SaveChanges();
                    Photo.SaveAs(Server.MapPath("~/Uploads/Car_Companies/" + cars_Companies.ID + ".jpg"));
                    ViewBag.Done = "تم الاضافة بنجاح ";
                }
            }catch(Exception ex)
            {
                ViewBag.error = ex.Message;
            }

            return View(cars_Companies);
        }

        // GET: Cars_Companies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Cars_Companies cars_Companies = db.Cars_Companies.Find(id);
            if (cars_Companies == null)
            {
                return HttpNotFound();
            }
            return View(cars_Companies);
        }

        // POST: Cars_Companies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Link,Country_ID")] Cars_Companies cars_Companies,HttpPostedFileBase Photo)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    cars_Companies.Name_en = DEL.TranslateText(cars_Companies.Name, "ar|en");
                    db.Entry(cars_Companies).State = EntityState.Modified;
                    db.SaveChanges();
                    if (Photo != null)
                    {
                        Photo.SaveAs(Server.MapPath("~/Uploads/Car_Companies/" + cars_Companies.ID + ".jpg"));
                    }
                    ViewBag.Done = "تم التعديل بنجاح ";
                }
            }catch(Exception ex)
            {
                ViewBag.error = ex.Message;
            }
            return View(cars_Companies);
        }

        // POST: Cars_Companies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cars_Companies cars_Companies = db.Cars_Companies.Find(id);
            db.Cars_Companies.Remove(cars_Companies);
            db.SaveChanges();
            FileInfo F = new FileInfo(Server.MapPath("~/Uploads/Car_Companies/" + id + ".jpg"));
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
