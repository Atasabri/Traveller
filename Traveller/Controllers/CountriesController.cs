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
    public class CountriesController : Controller
    {
        private DB db = new DB();

        // GET: Countries
        public ActionResult Index()
        {
            return View(db.Countries.ToList());
        }

        // GET: Countries/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Country country = db.Countries.Find(id);
            if (country == null)
            {
                return HttpNotFound();
            }
            return View(country);
        }

        // GET: Countries/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Countries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Currency,CurrencyName,Log,Lat,Police_Number,Ambulance_Number,Fire_Number,History,Roles")] Country country,HttpPostedFileBase Photo,HttpPostedFileBase HistoryPDF,HttpPostedFileBase RolesPDF)
        {
            try
            {
                if (ModelState.IsValid && Photo != null)
                {
                    country.Name_en = DEL.TranslateText(country.Name, "ar|en");
                    country.History_en = DEL.TranslateText(country.History, "ar|en");
                    country.Roles_en = DEL.TranslateText(country.Roles, "ar|en");
                    db.Countries.Add(country);
                    db.SaveChanges();
                    Photo.SaveAs(Server.MapPath("/Uploads/Countries/" + country.ID + ".jpg"));
                    if (HistoryPDF != null)
                    {
                        HistoryPDF.SaveAs(Server.MapPath("/Uploads/Countries/HistoryPDF/" + country.ID + ".pdf"));
                    }
                    if (RolesPDF != null)
                    {
                        HistoryPDF.SaveAs(Server.MapPath("/Uploads/Countries/RolesPDF/" + country.ID + ".pdf"));
                    }
                    ViewBag.Done = "تم الاضافة بنجاح ";
                }
            }catch(Exception ex)
            {
                ViewBag.error = ex.Message;
            }
            return View(country);
        }

        // GET: Countries/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Country country = db.Countries.Find(id);
            if (country == null)
            {
                return HttpNotFound();
            }
            return View(country);
        }

        // POST: Countries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Currency,CurrencyName,Log,Lat,Police_Number,Ambulance_Number,Fire_Number,History,Roles")] Country country, HttpPostedFileBase Photo, HttpPostedFileBase HistoryPDF, HttpPostedFileBase RolesPDF)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    country.Name_en = DEL.TranslateText(country.Name, "ar|en");
                    country.History_en = DEL.TranslateText(country.History, "ar|en");
                    country.Roles_en = DEL.TranslateText(country.Roles, "ar|en");
                    db.Entry(country).State = EntityState.Modified;
                    db.SaveChanges();
                    if (Photo != null)
                    {
                        Photo.SaveAs(Server.MapPath("/Uploads/Countries/" + country.ID + ".jpg"));
                    }
                    if (HistoryPDF != null)
                    {
                        HistoryPDF.SaveAs(Server.MapPath("/Uploads/Countries/HistoryPDF/" + country.ID + ".pdf"));
                    }
                    if (RolesPDF != null)
                    {
                        HistoryPDF.SaveAs(Server.MapPath("/Uploads/Countries/RolesPDF/" + country.ID + ".pdf"));
                    }
                    ViewBag.Done = "تم التعديل بنجاح ";
                }
            }catch(Exception ex)
            {
                ViewBag.error = ex.Message;
            }
            return View(country);
        }
        // POST: Countries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Country country = db.Countries.Find(id);
            db.Countries.Remove(country);
            db.SaveChanges();
            FileInfo F = new FileInfo(Server.MapPath("/Uploads/Countries/" + id + ".jpg"));
            if(F.Exists)
            {
                F.Delete();
            }
            FileInfo F1 = new FileInfo(Server.MapPath("/Uploads/Countries/HistoryPDF/" + id + ".pdf"));
            if (F1.Exists)
            {
                F1.Delete();
            }
            FileInfo F2 = new FileInfo(Server.MapPath("/Uploads/Countries/RolesPDF/" + id + ".pdf"));
            if (F2.Exists)
            {
                F2.Delete();
            }
            return RedirectToAction("Index");
        }
        public ActionResult DownLoadHistory(int id,string Name)
        {
                return File(Server.MapPath("~/Uploads/Countries/HistoryPDF/" + id + ".pdf"), "application/pdf", Name + ".pdf");
        }
        public ActionResult DownLoadRoles(int id, string Name)
        {
                return File(Server.MapPath("~/Uploads/Countries/RolesPDF/" + id + ".pdf"), "application/pdf", Name + ".pdf");
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
