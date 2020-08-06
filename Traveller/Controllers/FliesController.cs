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
    public class FliesController : Controller
    {
        private DB db = new DB();

        // GET: Flies
        public ActionResult Index()
        {
            var flys = db.Flys.Include(f => f.City);
            return View(flys.ToList());
        }

        // GET: Flies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Fly fly = db.Flys.Find(id);
            if (fly == null)
            {
                return HttpNotFound();
            }
            return View(fly);
        }

        // GET: Flies/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Flies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Link,City_ID")] Fly fly,HttpPostedFileBase Photo)
        {
            try
            {
                if (ModelState.IsValid && Photo != null)
                {
                    fly.Name_en = DEL.TranslateText(fly.Name, "ar|en");
                    db.Flys.Add(fly);
                    db.SaveChanges();
                    Photo.SaveAs(Server.MapPath("~/Uploads/Flys/" + fly.ID + ".jpg"));
                    ViewBag.Done = "تم الاضافة بنجاح ";
                }
            }catch(Exception ex)
            {
                ViewBag.error = ex.Message;
            }
            return View(fly);
        }

        // GET: Flies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Fly fly = db.Flys.Find(id);
            if (fly == null)
            {
                return HttpNotFound();
            }
            return View(fly);
        }

        // POST: Flies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Link,City_ID")] Fly fly,HttpPostedFileBase Photo)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    fly.Name_en = DEL.TranslateText(fly.Name, "ar|en");
                    db.Entry(fly).State = EntityState.Modified;
                    db.SaveChanges();
                    if (Photo != null)
                    {
                        Photo.SaveAs(Server.MapPath("~/Uploads/Flys/" + fly.ID + ".jpg"));
                    }
                    ViewBag.Done = "تم التعديل بنجاح ";
                }
            }catch(Exception ex)
            {
                ViewBag.error = ex.Message;
            }
            return View(fly);
        }

        // POST: Flies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Fly fly = db.Flys.Find(id);
            db.Flys.Remove(fly);
            db.SaveChanges();
            FileInfo F = new FileInfo(Server.MapPath("~/Uploads/Flys/" + id + ".jpg"));
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
