using project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using project.Models;
using Microsoft.Owin.Security.Provider;
namespace project.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpGet]
        public ActionResult Ankeet()
        {
            var pyhad = db.Pyhad.ToList();
            //ViewBag.Pyhad = pyhad;
            ViewBag.Pyhad = new SelectList(pyhad, "Id", "Nimetus");


            return View();
        }
        public ActionResult Ankeet(Kylaline kylaline)
        {
            if (ModelState.IsValid)
            {
                db.Kylalined.Add(kylaline);
                db.SaveChanges();
                return RedirectToAction("Tanan", new { id = kylaline.Id });
            }
            var pyhad = db.Pyhad.ToList();
            ViewBag.Pyhad = new SelectList(pyhad, "Id", "Nimetus", kylaline.PyhaId);
            return View(kylaline);
        }
        public ActionResult Tanan(int id)
        {
            var kylaline = db.Kylalined.Find(id);
            if (kylaline == null)
            {
                return HttpNotFound();
            }
            ViewBag.Pyhanimetus = db.Pyhad.Find(kylaline.PyhaId)?.Nimetus;
            ViewBag.Pilt = "smile.jpg";


            ViewBag.Pyhanimetus = db.Pyhad.Find(kylaline.PyhaId)?.Nimetus;
            ViewBag.Pilt2 = "sad.jpg";
            return View("Tanan", kylaline);
        }
    }
}