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
    public class Country_VideosController : Controller
    {
        private DB db = new DB();

        // GET: Country_Videos
        public ActionResult Index()
        {
            var country_Videos = db.Country_Videos.Include(c => c.Country);
            return View(country_Videos.ToList());
        }

        // GET: Country_Videos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Country_Videos country_Videos = db.Country_Videos.Find(id);
            if (country_Videos == null)
            {
                return HttpNotFound();
            }
            return View(country_Videos);
        }

        // GET: Country_Videos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Country_Videos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Description,Country_ID")] Country_Videos country_Videos,HttpPostedFileBase Video)
        {
            try
            {
                if (ModelState.IsValid && Video != null)
                {
                    country_Videos.Description_en = DEL.TranslateText(country_Videos.Description, "ar|en");
                    db.Country_Videos.Add(country_Videos);
                    db.SaveChanges();
                    Video.SaveAs(Server.MapPath("~/Uploads/Country_Videos/" + country_Videos.ID + ".mp4"));
                    ViewBag.Done = "تم الاضافة";
                }
            }catch(Exception ex)
            {
                ViewBag.error = ex.Message;
            }

            return View(country_Videos);
        }

        // GET: Country_Videos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Country_Videos country_Videos = db.Country_Videos.Find(id);
            if (country_Videos == null)
            {
                return HttpNotFound();
            }
            return View(country_Videos);
        }

        // POST: Country_Videos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Description,Country_ID")] Country_Videos country_Videos,HttpPostedFileBase Video)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    country_Videos.Description_en = DEL.TranslateText(country_Videos.Description, "ar|en");
                    db.Entry(country_Videos).State = EntityState.Modified;
                    db.SaveChanges();
                    if (Video != null)
                    {
                        Video.SaveAs(Server.MapPath("~/Uploads/Country_Videos/" + country_Videos.ID + ".mp4"));
                    }
                    ViewBag.Done = "تم التعديل";
                }
            }catch(Exception ex)
            {
                ViewBag.error = ex.Message;
            }
            return View(country_Videos);
        }
        // POST: Country_Videos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Country_Videos country_Videos = db.Country_Videos.Find(id);
            db.Country_Videos.Remove(country_Videos);
            db.SaveChanges();
            FileInfo F = new FileInfo(Server.MapPath("~/Uploads/Country_Videos/" + id + ".mp4"));
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
