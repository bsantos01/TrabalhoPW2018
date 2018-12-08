using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrabalhoPW.Models;

namespace TrabalhoPW.Controllers
{
    public class HomeController : Controller
    {
        private MuseuContext db = new MuseuContext();

        public ActionResult Index()
        {
            Texts home = (db.Texts.Where(m => m.Pagina == "HomePage").First());
            return View(home);
        }

        public ActionResult Contact()
        {
            Texts cont = (db.Texts.Where(m => m.Pagina == "Contactos").First());
            return View(cont);
        }
        [Authorize(Roles ="Admin, Especialista")]
        public ActionResult EditC()
        {
            var cont = (db.Texts.Where(m => m.Pagina == "Contactos").First());
            return View(cont);
        }
        [Authorize(Roles = "Admin, Especialista")]
        public ActionResult EditH()
        {
            var cont = (db.Texts.Where(m => m.Pagina == "HomePage").First());
            return View(cont);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditC([Bind(Include = "Pagina,SubT,Conteudo")] Texts T)
        {

            var text = db.Texts.First(a => a.Pagina == "Contactos");
            text.SubT = T.SubT;
            text.Conteudo = T.Conteudo;
            db.SaveChanges();


            return RedirectToAction("Contact");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditH([Bind(Include = "Pagina,SubT,Conteudo")] Texts T)
        {

            var text = db.Texts.First(a => a.Pagina == "HomePage");
            text.SubT = T.SubT;
            text.Conteudo = T.Conteudo;
            db.SaveChanges();


            return RedirectToAction("Index");
        }

    }
}