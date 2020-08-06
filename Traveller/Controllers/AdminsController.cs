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
using System.Web.Security;
namespace Traveller.Controllers
{
    [Authorize]
    public class AdminsController : Controller
    {
        private DB db = new DB();

        // GET: Admins
        public ActionResult Index()
        {
            return View(db.Admins.ToList());
        }

        // GET: Admins/Details/5
        public ActionResult Details(int? id)
        {           
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Admin admin = db.Admins.Find(id);
            if (admin == null)
            {
                return HttpNotFound();
            }

            int admin_id = int.Parse(User.Identity.Name);
            Admin _admin = db.Admins.Find(admin_id);
            if (_admin.IsManager||_admin.ID== admin.ID)
            {
                return View(admin);
            }
            return RedirectToAction("Index");
        }

        // GET: Admins/Create
        public ActionResult Create()
        {
             int id = int.Parse(User.Identity.Name);
                Admin admin = db.Admins.Find(id);
                if (!admin.IsManager)
                {
                    return RedirectToAction("Index");
                }
            return View();
        }

        // POST: Admins/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,UserName,Password,IsManager")] Admin admin,HttpPostedFileBase Photo)
        {
            try {
                if (ModelState.IsValid && Photo != null)
                {
                    if (db.Admins.Any(x => x.UserName == admin.UserName))
                    {
                        ViewBag.error = "This UserName is Used ! ";
                        return View(admin);
                    }
                    db.Admins.Add(admin);
                    db.SaveChanges();
                    Photo.SaveAs(Server.MapPath("~/Uploads/Admins/" + admin.ID + ".jpg"));
                    return RedirectToAction("Index");
                }
            }
            catch(Exception ex)
            {
                ViewBag.error = ex.Message;
            }
            return View(admin);

        }

        // GET: Admins/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Admin admin = db.Admins.Find(id);
            if (admin == null)
            {
                return HttpNotFound();
            }
            int admin_id = int.Parse(User.Identity.Name);
            Admin _admin = db.Admins.Find(admin_id);
            if (!_admin.IsManager)
            {
                return RedirectToAction("Index");
            }
            return View(admin);
        }

        // POST: Admins/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,UserName,Password,IsManager")] Admin admin,HttpPostedFileBase Photo)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int id = int.Parse(User.Identity.Name);
                    db.Entry(admin).State = EntityState.Modified;
                    db.SaveChanges();
                    if (Photo != null)
                    {
                        Photo.SaveAs(Server.MapPath("~/Uploads/Admins/" + admin.ID + ".jpg"));
                    }
                    int admin_id = int.Parse(User.Identity.Name);
                    if (admin_id == admin.ID)
                    {
                        return RedirectToAction("logout", "Auth");
                    }
                    return RedirectToAction("Index");
                }
            }catch(Exception ex)
            {
                ViewBag.error = ex.Message;
            }
            return View(admin);
        }
        // POST: Admins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            int admin_id = int.Parse(User.Identity.Name);
            Admin _admin = db.Admins.Find(admin_id);
            if (!_admin.IsManager)
            {
                return RedirectToAction("Index");
            }
            Admin admin = db.Admins.Find(id);
            db.Admins.Remove(admin);
            db.SaveChanges();
            FileInfo F = new FileInfo(Server.MapPath("~/Uploads/Admins/" + id + ".jpg"));
            if(F.Exists)
            {
                F.Delete();
            }
            return RedirectToAction("Index");
        }
        public ActionResult Tasks()
        {
            int admin_id = int.Parse(User.Identity.Name);
            Admin _admin = db.Admins.Find(admin_id);
            if (_admin.IsManager)
            {
                return View(db.Tasks.Where(x => x.FromManager == admin_id).ToList());
            }
            else
            {
                return View(db.Tasks.Where(x => x.ToAdmin == admin_id).ToList());
            }
        }
        public ActionResult AddTask()
        {

                int admin_id = int.Parse(User.Identity.Name);
                Admin _admin = db.Admins.Find(admin_id);
                if (!_admin.IsManager)
                {
                    return RedirectToAction("Tasks");
                }
            return View();
        }
        [HttpPost]
        public ActionResult AddTask(Task task)
        {
            try
            {
                int admin_id = int.Parse(User.Identity.Name);
                Admin _admin = db.Admins.Find(admin_id);
                task.FromManager = admin_id;
                db.Tasks.Add(task);
                db.SaveChanges();
                return RedirectToAction("Tasks");
            }catch(Exception ex)
            {
                ViewBag.error = ex.Message;
            }
            return View(task);
        }
        [HttpPost]
        public ActionResult DeleteTask(int id)
        {
            int admin_id = int.Parse(User.Identity.Name);
            Admin _admin = db.Admins.Find(admin_id);
            if (!_admin.IsManager)
            {
                return RedirectToAction("Tasks");
            }
            Task task = db.Tasks.Find(id);
            db.Tasks.Remove(task);
            db.SaveChanges();
            return RedirectToAction("Tasks");
        }
        [HttpPost]
        public ActionResult FinishTask(int id)
        {
            int admin_id = int.Parse(User.Identity.Name);
            Admin _admin = db.Admins.Find(admin_id);
            if (_admin.IsManager)
            {
                return RedirectToAction("Tasks");
            }
            Task task = db.Tasks.Find(id);
            task.Finished = true;
            db.Entry(task).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Tasks");
        }
        public ActionResult Codes()
        {
            return View(db.Codes.ToList());
        }
        [HttpPost]
        public JsonResult GetCode()
        {
            Code code = new Code();
            db.Codes.Add(code);
            db.SaveChanges();
            string TokenCode = DEL.encrypt(code.ID.ToString());
            return Json(TokenCode);
        }
        [HttpPost]
        public ActionResult DeleteCode(int id)
        {
            Code code = db.Codes.Find(id);
            db.Codes.Remove(code);
            db.SaveChanges();
            return RedirectToAction("Codes");
        }
        public ActionResult Dashboard()
        {
            return View();
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
