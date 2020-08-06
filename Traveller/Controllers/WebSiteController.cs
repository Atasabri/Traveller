using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Traveller.Models;

namespace Traveller.Controllers
{
    [Authorize]
    public class WebSiteController : Controller
    {
        DB db = new DB();
        // GET: WebSite
        public ActionResult Contacts()
        {
            return View(db.Contacts.ToList());
        }
        public ActionResult Subscribers()
        {
            return View(db.Subscribers.ToList());
        }
        [HttpPost]
        public ActionResult SendMail(string Subject, HttpPostedFileBase file, List<string> fooa)
        {
            if (fooa != null && fooa.Count() >= 1 && file != null)
            {
                List<string> Users = fooa.Distinct().ToList();
                DEL.Send_Mail(Subject, file, Users);
            }

            return RedirectToAction("Contacts");
        }
    }
}