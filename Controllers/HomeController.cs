using project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using project.Models;
using System.IO;

using Microsoft.Owin.Security.Provider;
using System.Web.Helpers;
using System.Web.UI.WebControls;
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
            if (kylaline.OnKutse)
            {
                ViewBag.Pilt = "smile.jpg";
            }
            else
            { ViewBag.Pilt = "sad.jpg"; }
            SaadaEmail(kylaline, ViewBag.Pilt, ViewBag.Pyhanimetus, kylaline.OnKutse);

            return View("Tanan", kylaline);
        }

        private void SaadaEmail(Kylaline kylaline, string pilt, string pyha, bool onkutse)
        {
            string failiTee = Path.Combine(Server.MapPath("~/images/"), pilt);
            try
            {
                WebMail.SmtpServer = "smtp.gmail.com";
                WebMail.SmtpPort = 587;
                WebMail.EnableSsl = true;
                WebMail.UserName = "eha20082@gmail.com";
                WebMail.Password = "bwdn mlju pndo jney";
                WebMail.From = "eha20082@gmail.com";

                string sisu = "";
                if (onkutse)
                {
                    sisu = $"Tere, {kylaline.Nimi}!<br/><br/>" +
                           $"Sinu registreerumine sündmusele <b>{pyha}</b> on salvestatud.<br/>" +
                           "Lisaksime kirjale ka sündmuse kutse. Ootame sind väga!<br/><br/>" +
                           "Kohtumiseni!";
                }
                else
                {
                    sisu = $"Tere, {kylaline.Nimi}!<br/><br/>" +
                           $"Sinu registreerumine sündmusele <b>{pyha}</b> on salvestatud.<br/>" +
                           "Lisaksime kirjale ka sündmuse kutse. Kahju, et sa ei tule peole!<br/><br/>" +
                           "Kõige head!";
                }

                // Saada kiri koos manusega
                WebMail.Send(
                    to: kylaline.Email,
                    subject: "Vastus: " + pyha,
                    body: sisu,
                    isBodyHtml: true,
                    filesToAttach: new string[] { failiTee } 
                );
            }
            catch (System.Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("viga");
            }

        }

    }
}