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
    public class Calls_CompaniesController : Controller
    {
        private DB db = new DB();

        // GET: Calls_Companies
        public ActionResult Index()
        {
            var calls_Companies = db.Calls_Companies.Include(c => c.Country);
            return View(calls_Companies.ToList());
        }

        // GET: Calls_Companies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Calls_Companies calls_Companies = db.Calls_Companies.Find(id);
            if (calls_Companies == null)
            {
                return HttpNotFound();
            }
            return View(calls_Companies);
        }

        // GET: Calls_Companies/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Calls_Companies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Code,Help_Number,Link,Country_ID")] Calls_Companies calls_Companies,HttpPostedFileBase Photo)
        {
            try
            {
                if (ModelState.IsValid && Photo != null)
                {
                    calls_Companies.Name_en = DEL.TranslateText(calls_Companies.Name, "ar|en");
                    db.Calls_Companies.Add(calls_Companies);
                    db.SaveChanges();
                    Photo.SaveAs(Server.MapPath("~/Uploads/Calls_Companies/" + calls_Companies.ID + ".jpg"));
                    ViewBag.Done = "تم الاضافة بنجاح ";
                }
            }catch(Exception ex)
            {
                ViewBag.error = ex.Message;
                
            }
            return View(calls_Companies);
        }

        // GET: Calls_Companies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Calls_Companies calls_Companies = db.Calls_Companies.Find(id);
            if (calls_Companies == null)
            {
                return HttpNotFound();
            }
            return View(calls_Companies);
        }

        // POST: Calls_Companies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Code,Help_Number,Link,Country_ID")] Calls_Companies calls_Companies,HttpPostedFileBase Photo)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    calls_Companies.Name_en = DEL.TranslateText(calls_Companies.Name, "ar|en");
                    db.Entry(calls_Companies).State = EntityState.Modified;
                    db.SaveChanges();
                    if (Photo != null)
                    {
                        Photo.SaveAs(Server.MapPath("~/Uploads/Calls_Companies/" + calls_Companies.ID + ".jpg"));
                    }
                    ViewBag.Done = "تم التعديل بنجاح ";
                }
            }catch(Exception ex)
            {
                ViewBag.error = ex.Message;
            }
            return View(calls_Companies);
        }
        // POST: Calls_Companies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Calls_Companies calls_Companies = db.Calls_Companies.Find(id);
            db.Calls_Companies.Remove(calls_Companies);
            db.SaveChanges();
            FileInfo F = new FileInfo(Server.MapPath("~/Uploads/Calls_Companies/" + id + ".jpg"));
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
