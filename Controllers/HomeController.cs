using project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using project.Models;
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
            ViewBag.Pyhad = new SelectList(pyhad, "Nimetus", "Kuupaev");


            return View();
        }
    }
}