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
    public class CarsController : Controller
    {
        private DB db = new DB();

        // GET: Cars
        public ActionResult Index()
        {
            var cars = db.Cars.Include(c => c.City);
            return View(cars.ToList());
        }

        // GET: Cars/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Car car = db.Cars.Find(id);
            if (car == null)
            {
                return HttpNotFound();
            }
            return View(car);
        }

        // GET: Cars/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Cars/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Link,City_ID")] Car car,HttpPostedFileBase Photo)
        {
            try
            {
                if (ModelState.IsValid && Photo != null)
                {
                    car.Name_en = DEL.TranslateText(car.Name, "ar|en");
                    db.Cars.Add(car);
                    db.SaveChanges();
                    Photo.SaveAs(Server.MapPath("~/Uploads/Cars/" + car.ID + ".jpg"));
                    ViewBag.Done = "تم الاضافة بنجاح ";
                }
            }catch(Exception ex)
            {
                ViewBag.error = ex.Message;
            }
            return View(car);
        }

        // GET: Cars/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Car car = db.Cars.Find(id);
            if (car == null)
            {
                return HttpNotFound();
            }
            return View(car);
        }

        // POST: Cars/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Link,City_ID")] Car car,HttpPostedFileBase Photo)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    car.Name_en = DEL.TranslateText(car.Name, "ar|en");
                    db.Entry(car).State = EntityState.Modified;
                    db.SaveChanges();
                    if (Photo != null)
                    {
                        Photo.SaveAs(Server.MapPath("~/Uploads/Cars/" + car.ID + ".jpg"));
                    }
                    ViewBag.Done = "تم التعديل بنجاح ";
                }
            }catch(Exception ex)
            {
                ViewBag.error = ex.Message;
            }
            return View(car);
        }
        // POST: Cars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Car car = db.Cars.Find(id);
            db.Cars.Remove(car);
            db.SaveChanges();
            FileInfo F = new FileInfo(Server.MapPath("~/Uploads/Cars/"+id+".jpg"));
            if(F.Exists)
            {
                F.Delete();
            }
            return RedirectToAction("Index");
        }
        public ActionResult GetCities(int CountryID)
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<City> List = db.Cities.Where(x => x.Country_ID == CountryID).ToList();
            return Json(List, JsonRequestBehavior.AllowGet);
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
