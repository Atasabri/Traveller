using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Traveller.Models;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

namespace Traveller.Controllers
{
    public class HomeController : Controller
    {
        DB db = new DB();
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ContactUs(Contact contact)
        {
            try
            {
                db.Contacts.Add(contact);
                db.SaveChanges();
            }
            catch
            {
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Subscribe(Subscriber subscribe)
        {
            try
            {
                db.Subscribers.Add(subscribe);
                db.SaveChanges();
            }
            catch
            {

            }
            return RedirectToAction("Index");
        }
        //public JsonResult test()
        //{
        //    foreach (var item in Directory.GetFiles(Server.MapPath("~/Uploads/Cities_Background")))
        //    {
        //        using (var image = System.Drawing.Image.FromFile(item))
        //        {
        //            var newWidth = (int)(image.Width * .3);
        //            var newHeight = (int)(image.Height * .3);
        //            var thumbnailImg = new Bitmap(newWidth, newHeight);
        //            var thumbGraph = Graphics.FromImage(thumbnailImg);
        //            thumbGraph.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
        //            thumbGraph.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
        //            thumbGraph.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
        //            var imageRectangle = new Rectangle(0, 0, newWidth, newHeight);
        //            thumbGraph.DrawImage(image, imageRectangle);
        //            thumbnailImg.Save(item);
        //        }
        //    }

        //    return Json("1");
        //}
    }
}