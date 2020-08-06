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
    public class PlacesController : Controller
    {
        private DB db = new DB();

        // GET: Places
        public ActionResult Index()
        {
            var places = db.Places.Include(p => p.City);
            return View(places.ToList());
        }

        // GET: Places/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Place place = db.Places.Find(id);
            if (place == null)
            {
                return HttpNotFound();
            }
            return View(place);
        }

        // GET: Places/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Places/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Description,Log,Lat,City_ID,NameInCountry")] Place place,List<HttpPostedFileBase>Photos,List<HttpPostedFileBase>Videos)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    place.Name_en = DEL.TranslateText(place.Name, "ar|en");
                    place.Description_en = DEL.TranslateText(place.Description, "ar|en");
                    if (Photos[0] != null)
                    {
                        foreach (var item in Photos)
                        {
                            place.Places_Photos.Add(new Places_Photos { });
                        }
                    }
                    if (Videos[0] != null)
                    {
                        foreach (var item in Videos)
                        {
                            place.Places_Videos.Add(new Places_Videos { });
                        }
                    }
                    db.Places.Add(place);
                    db.SaveChanges();
                    List<Places_Photos> ListPhotos = place.Places_Photos.ToList();
                    List<Places_Videos> Listvideos = place.Places_Videos.ToList();
                    for (int i = 0; i < ListPhotos.Count; i++)
                    {
                        Photos[i].SaveAs(Server.MapPath("~/Uploads/Places_Photos/" + ListPhotos[i].ID + ".jpg"));
                    }
                    for (int i = 0; i < Listvideos.Count; i++)
                    {
                        Videos[0].SaveAs(Server.MapPath("~/Uploads/Places_Videos/" + Listvideos[i].ID + ".mp4"));
                    }
                    var city = db.Cities.Where(x=>x.ID==place.City_ID)
                        .Select(x=>new {
                            x.ID,
                            x.Name,
                            x.Name_en,
                            x.Country_ID,
                            x.Log,
                            x.Lat,
                            Photo = DEL.Domain + "/Uploads/Cities/" + x.City_Photos.FirstOrDefault().ID + ".jpg",
                            BackgroundPhoto = DEL.Domain + "/Uploads/Cities_Background/" + x.ID + ".jpg"
                        })
                        .FirstOrDefault();
                    var notifyusers = db.Favorites.Where(x => x.Country_ID == city.Country_ID).Select(x => x.User.FCM).ToArray();
                    DEL.Notify(notifyusers,"تم اضافة مكان سياحى جديد",place.Name,place.ID,"place",city);
                    ViewBag.Done = "تم الاضافة بنجاح ";
                }
            }catch(Exception ex)
            {
                ViewBag.error = ex.Message;
            }
            return View(place);
        }

        // GET: Places/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Place place = db.Places.Find(id);
            if (place == null)
            {
                return HttpNotFound();
            }
            return View(place);
        }

        // POST: Places/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Description,Log,Lat,City_ID,NameInCountry")] Place place, List<HttpPostedFileBase> Photos, List<HttpPostedFileBase> Videos)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    place.Name_en = DEL.TranslateText(place.Name, "ar|en");
                    place.Description_en = DEL.TranslateText(place.Description, "ar|en");
                    List<Places_Photos> ListPhoto = new List<Places_Photos>();
                    if (Photos[0] != null)
                    {
                        foreach (var item in Photos)
                        {
                            ListPhoto.Add(new Places_Photos { Place_ID = place.ID });
                        }
                    }
                    List<Places_Videos> ListVideos = new List<Places_Videos>();
                    if (Videos[0] != null)
                    {
                        foreach (var item in Videos)
                        {
                            ListVideos.Add(new Places_Videos { Place_ID = place.ID });
                        }
                    }
                    db.Places_Photos.AddRange(ListPhoto);
                    db.Places_Videos.AddRange(ListVideos);
                    db.Entry(place).State = EntityState.Modified;
                    db.SaveChanges();
                    for (int i = 0; i < ListPhoto.Count; i++)
                    {
                        Photos[i].SaveAs(Server.MapPath("~/Uploads/Places_Photos/" + ListPhoto[i].ID + ".jpg"));
                    }
                    for (int i = 0; i < ListVideos.Count; i++)
                    {
                        Videos[i].SaveAs(Server.MapPath("~/Uploads/Places_Videos/" + ListVideos[i].ID + ".mp4"));
                    }
                    return RedirectToAction("Index");
                }
            }catch(Exception ex)
            {
                ViewBag.error = ex.Message;
            }
            return View(place);
        }
        // POST: Places/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Place place = db.Places.Find(id);
            foreach (var item in place.Places_Photos)
            {
                FileInfo F = new FileInfo(Server.MapPath("~/Uploads/Places_Photos/"+item.ID+".jpg"));
                if(F.Exists)
                {
                    F.Delete();
                }
            }
            foreach (var item in place.Places_Videos)
            {
                FileInfo F = new FileInfo(Server.MapPath("~/Uploads/Places_Videos/" + item.ID + ".mp4"));
                if (F.Exists)
                {
                    F.Delete();
                }
            }
            db.Places.Remove(place);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult DeletePhoto(int id)
        {
            Places_Photos photo = db.Places_Photos.Find(id);
            db.Places_Photos.Remove(photo);
            db.SaveChanges();
            FileInfo F = new FileInfo(Server.MapPath("~/Uploads/Places_Photos/"+id+".jpg"));
            if(F.Exists)
            {
                F.Delete();
            }
            return Redirect(Request.UrlReferrer.AbsolutePath);
        }
        [HttpPost]
        public ActionResult DeleteVideo(int id)
        {
            Places_Videos video = db.Places_Videos.Find(id);
            db.Places_Videos.Remove(video);
            db.SaveChanges();
            FileInfo F = new FileInfo(Server.MapPath("~/Uploads/Places_Videos/" + id + ".mp4"));
            if (F.Exists)
            {
                F.Delete();
            }
            return Redirect(Request.UrlReferrer.AbsolutePath);
        }
        [HttpPost]
        public ActionResult DeleteComment(int id)
        {
            Place_Comments comment = db.Place_Comments.Find(id);
            db.Place_Comments.Remove(comment);
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
