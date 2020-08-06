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
    public class AboutUsVideosController : Controller
    {
        private DB db = new DB();

        // GET: AboutUsVideos
        public ActionResult Index()
        {
            return View(db.AboutUsVideos.ToList());
        }

        // GET: AboutUsVideos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            AboutUsVideo aboutUsVideo = db.AboutUsVideos.Find(id);
            if (aboutUsVideo == null)
            {
                return HttpNotFound();
            }
            return View(aboutUsVideo);
        }

        // GET: AboutUsVideos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AboutUsVideos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Description")] AboutUsVideo aboutUsVideo,HttpPostedFileBase Video)
        {
            try
            {
                if (ModelState.IsValid && Video != null)
                {
                    aboutUsVideo.Description_en = DEL.TranslateText(aboutUsVideo.Description, "ar|en");
                    db.AboutUsVideos.Add(aboutUsVideo);
                    db.SaveChanges();

                    Video.SaveAs(Server.MapPath("~/Uploads/AboutUsVideos/" + aboutUsVideo.ID + ".mp4"));
                    ViewBag.Done = "تم اضافة الفيديو بنجاح .";
                }
            }catch(Exception ex)
            {
                ViewBag.error = ex.Message;
            }

            return View(aboutUsVideo);
        }

        // GET: AboutUsVideos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            AboutUsVideo aboutUsVideo = db.AboutUsVideos.Find(id);
            if (aboutUsVideo == null)
            {
                return HttpNotFound();
            }
            return View(aboutUsVideo);
        }

        // POST: AboutUsVideos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Description")] AboutUsVideo aboutUsVideo,HttpPostedFileBase Video)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    aboutUsVideo.Description_en = DEL.TranslateText(aboutUsVideo.Description, "ar|en");
                    db.Entry(aboutUsVideo).State = EntityState.Modified;
                    db.SaveChanges();
                    if (Video != null)
                    {
                        Video.SaveAs(Server.MapPath("~/Uploads/AboutUsVideos/" + aboutUsVideo.ID + ".mp4"));
                    }
                    ViewBag.Done = "تم تعديل الفيديو بنجاح .";
                }
            }catch(Exception ex)
            {
                ViewBag.error = ex.Message;
            }
            return View(aboutUsVideo);
        }

        // POST: AboutUsVideos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AboutUsVideo aboutUsVideo = db.AboutUsVideos.Find(id);
            db.AboutUsVideos.Remove(aboutUsVideo);
            db.SaveChanges();
            FileInfo F = new FileInfo(Server.MapPath("~/Uploads/AboutUsVideos/"+id+".mp4"));
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
