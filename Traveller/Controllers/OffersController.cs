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
    public class OffersController : Controller
    {
        private DB db = new DB();

        // GET: Offers
        public ActionResult Index()
        {
            var offers = db.Offers.Include(o => o.Country);
            return View(offers.ToList());
        }

        // GET: Offers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Offer offer = db.Offers.Find(id);
            if (offer == null)
            {
                return HttpNotFound();
            }
            return View(offer);
        }

        // GET: Offers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Offers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Price,FromDate,ToDate,Days_Number,Visitors_Number,Currency,Description,Country_ID")] Offer offer,HttpPostedFileBase Photo)
        {
            try
            {
                if (ModelState.IsValid && Photo != null)
                {
                    offer.Description_en = DEL.TranslateText(offer.Description, "ar|en");
                    db.Offers.Add(offer);
                    db.SaveChanges();
                    Photo.SaveAs(Server.MapPath("~/Uploads/Offers/" + offer.ID + ".jpg"));
                    var users = db.Favorites.Where(x => x.Country_ID == offer.Country_ID).Select(x => x.User.FCM).ToArray();
                    DEL.Notify(users, "تم اضافة عرض جديد", "عرض جديد ", offer.ID, "offer");
                    ViewBag.Done = "تم الاضافة بنجاح ";
                }
            }catch(Exception ex)
            {
                ViewBag.error = ex.Message;
            }
            return View(offer);
        }

        // GET: Offers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Offer offer = db.Offers.Find(id);
            if (offer == null)
            {
                return HttpNotFound();
            }
            return View(offer);
        }

        // POST: Offers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Price,FromDate,ToDate,Days_Number,Visitors_Number,Currency,Description,Country_ID")] Offer offer,HttpPostedFileBase Photo)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    offer.Description_en = DEL.TranslateText(offer.Description, "ar|en");
                    db.Entry(offer).State = EntityState.Modified;
                    db.SaveChanges();
                    if (Photo != null)
                    {
                        Photo.SaveAs(Server.MapPath("~/Uploads/Offers/" + offer.ID + ".jpg"));
                    }
                    ViewBag.Done = "تم التعديل بنجاح ";
                }
            }catch(Exception ex)
            {
                ViewBag.error = ex.Message;
            }
            return View(offer);
        }

        // POST: Offers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Offer offer = db.Offers.Find(id);
            db.Offers.Remove(offer);
            db.SaveChanges();
            FileInfo F = new FileInfo(Server.MapPath("~/Uploads/Offers/" + id + ".jpg"));
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
