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
using System.Drawing.Imaging;
using System.Drawing;

namespace Traveller.Controllers
{
    [Authorize]
    public class CitiesController : Controller
    {
        private DB db = new DB();

        // GET: Cities
        public ActionResult Index()
        {
            var cities = db.Cities.Include(c => c.Country);
            return View(cities.ToList());
        }

        // GET: Cities/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            City city = db.Cities.Find(id);
            if (city == null)
            {
                return HttpNotFound();
            }
            return View(city);
        }

        // GET: Cities/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Cities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598 .
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Log,Lat,Country_ID")] City city,List<HttpPostedFileBase>Photos,List<HttpPostedFileBase>Reports,HttpPostedFileBase PhotoBack)
        {
            try
            {
                if (ModelState.IsValid&&PhotoBack!=null)
                {
                    city.Name_en = DEL.TranslateText(city.Name, "ar|en");
                    if (Photos[0] != null)
                    {
                        foreach (var item in Photos)
                        {
                            city.City_Photos.Add(new City_Photos { });
                        }
                    }
                    if (Reports[0] != null)
                    {
                        foreach (var item in Reports)
                        {
                            city.City_Reports.Add(new City_Reports { });
                        }
                    }
                    db.Cities.Add(city);
                    db.SaveChanges();
                    DEL.PhotoCompress(Server.MapPath("~/Uploads/Cities_Background/"+city.ID+".jpg"), PhotoBack);
                    List<City_Photos> ListPhotos = city.City_Photos.ToList();
                    for (int i = 0; i < ListPhotos.Count; i++)
                    {
                        DEL.PhotoCompress(Server.MapPath("~/Uploads/Cities/" + ListPhotos[i].ID + ".jpg"), Photos[i]);
                    }
                    List<City_Reports> ListReports = city.City_Reports.ToList();
                    for (int i = 0; i < ListReports.Count; i++)
                    {
                        DEL.PhotoCompress(Server.MapPath("~/Uploads/City_Reports/" + ListReports[i].ID + ".jpg"), Reports[i]);
                    }
                    ViewBag.Done = "تم الاضافة بنجاح ";
                }
            }catch(Exception ex)
            {
                ViewBag.error = ex.Message;
            }
            return View(city);
        }

        // GET: Cities/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            City city = db.Cities.Find(id);
            if (city == null)
            {
                return HttpNotFound();
            }
            return View(city);
        }

        // POST: Cities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Log,Lat,Country_ID")] City city, List<HttpPostedFileBase> Photos, List<HttpPostedFileBase> Reports, HttpPostedFileBase PhotoBack)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    city.Name_en = DEL.TranslateText(city.Name, "ar|en");
                    List<City_Photos> ListPhotos = new List<City_Photos>();
                    if (Photos[0] != null)
                    {
                        foreach (var item in Photos)
                        {
                            ListPhotos.Add(new City_Photos { City_ID = city.ID });
                        }
                    }

                    List<City_Reports> ListReports = new List<City_Reports>();
                    if (Reports[0] != null)
                    {
                        foreach (var item in Reports)
                        {
                            ListReports.Add(new City_Reports { City_ID = city.ID });
                        }
                    }
                    db.City_Photos.AddRange(ListPhotos);
                    db.City_Reports.AddRange(ListReports);
                    db.Entry(city).State = EntityState.Modified;
                    db.SaveChanges();
                    if(PhotoBack!=null)
                    {
                        PhotoBack.SaveAs(Server.MapPath("~/Uploads/Cities_Background/" + city.ID + ".jpg"));
                    }
                    for (int i = 0; i < ListPhotos.Count; i++)
                    {
                        Photos[i].SaveAs(Server.MapPath("~/Uploads/Cities/" + ListPhotos[i].ID + ".jpg"));
                    }
                    for (int i = 0; i < ListReports.Count; i++)
                    {
                        Reports[i].SaveAs(Server.MapPath("~/Uploads/City_Reports/" + ListReports[i].ID + ".jpg"));
                    }

                   return RedirectToAction("Index");
                }
            }catch(Exception ex)
            {
                ViewBag.error = ex.Message;
            }
            return View(city);
        }
        // POST: Cities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            City city = db.Cities.Find(id);
            foreach (var item in city.City_Photos)
            {
                FileInfo F = new FileInfo(Server.MapPath("~/Uploads/Cities/" + item.ID + ".jpg"));
                if (F.Exists)
                {
                    F.Delete();
                }
            }
            foreach (var item in city.City_Reports)
            {
                FileInfo F = new FileInfo(Server.MapPath("~/Uploads/City_Reports/" + item.ID + ".jpg"));
                if (F.Exists)
                {
                    F.Delete();
                }
            }
            db.Cities.Remove(city);
            db.SaveChanges();
            FileInfo F1 = new FileInfo(Server.MapPath("~/Uploads/Cities_Background/" + id + ".jpg"));
            if(F1.Exists)
            {
                F1.Delete();
            }

            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult DeletePhoto(int id)
        {
            City_Photos Photo = db.City_Photos.Find(id);
            db.City_Photos.Remove(Photo);
            db.SaveChanges();
            FileInfo F = new FileInfo(Server.MapPath("~/Uploads/Cities/"+id+".jpg"));
            if(F.Exists)
            {
                F.Delete();
            }
            return Redirect(Request.UrlReferrer.AbsolutePath);
        }
        public ActionResult Reports(int id)
        {
            City city = db.Cities.Find(id);
            if(city==null)
            {
                RedirectToAction("Index");
            }
            return View(city.City_Reports.ToList());
        }
        [HttpPost]
        public ActionResult DeleteReport(int id)
        {
            City_Reports Report = db.City_Reports.Find(id);
            db.City_Reports.Remove(Report);
            db.SaveChanges();
            FileInfo F = new FileInfo(Server.MapPath("~/Uploads/City_Reports/" + id + ".jpg"));
            if (F.Exists)
            {
                F.Delete();
            }
            return Redirect(Request.UrlReferrer.AbsolutePath);
        }
        [HttpPost]
        public ActionResult DeleteComment (int id)
        {
            Report_Comments comment = db.Report_Comments.Find(id);
            db.Report_Comments.Remove(comment);
            db.SaveChanges();
            return Redirect(Request.UrlReferrer.AbsolutePath);
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
