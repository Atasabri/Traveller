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
    public class UsersController : Controller
    {
        private DB db = new DB();

        // GET: Users
        public ActionResult Index()
        {
            var users = db.Users.Include(u => u.Country);
            return View(users.ToList());
        }

        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Email,Password,Phone,Is_Traveller,Country_ID")] User user,HttpPostedFileBase Photo)
        {
            try
            {
                if (ModelState.IsValid && Photo != null)
                {
                    user.Token = DEL.encrypt(user.Name);
                    db.Users.Add(user);
                    db.SaveChanges();
                    Photo.SaveAs(Server.MapPath("~/Uploads/Users/" + user.Name + ".jpg"));
                    ViewBag.Done = "تم الاضافة بنجاح ";
                    return View();
                }
            }catch(Exception ex)
            {
                ViewBag.error = ex.Message;
            }
            return View(user);
        }
        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            FileInfo F = new FileInfo(Server.MapPath("~/Uploads/Users/" + user.Name + ".jpg"));
            if(F.Exists)
            {
                F.Delete();
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public FileResult VideoDownLoad(int id,string Name)
        {
            return File(Server.MapPath("~/Uploads/User_Videos/" + id + ".mp4"), "application/mp4", Name + ".mp4");
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
