using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TrabalhoPW.Models;

namespace TrabalhoPW.Controllers
{
    public class UtilizadorsController : Controller
    {
        private MuseuContext db = new MuseuContext();

        // GET: Utilizadors
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return View(db.Utilizador.ToList());
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Validar()
        {
            var validacoes = db.Utilizador.Where(s => s.Valido == false);
            return View(validacoes.ToList());
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Validate(int id) {
            var user = db.Utilizador.First(a => a.UtilizadorID == id);
            user.Valido = true;

            db.SaveChanges();
            return RedirectToAction("Validar");
        }


        // GET: Utilizadors/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            Utilizador utilizador;
            if (id == null)
            {
                utilizador = (Utilizador)db.Utilizador.Where(m => m.Nome == User.Identity.Name).First();
                if (utilizador == null)
                {
                    return HttpNotFound();
                }
                return View(utilizador);
            }
            utilizador = db.Utilizador.Find(id);
            if (utilizador == null)
            {
                return HttpNotFound();
            }
            return View(utilizador);
        }


        // GET: Utilizadors/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            Utilizador utilizador;
            if (id == null)
            {
                utilizador = (Utilizador)db.Utilizador.Where(m => m.Nome == User.Identity.Name).First();
                return View(utilizador);
            }
            else { 
             utilizador = db.Utilizador.Find(id);
                if (utilizador == null)
                {
                 return HttpNotFound();
                }
                return View(utilizador);
            }

        }

     


        // POST: Utilizadors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Nome,Email,BI,NIF,Tipo,Valido,UserID")] Utilizador utilizador)
        {
            
            var user = db.Utilizador.First(a => a.Nome == utilizador.Nome);
            user.BI = utilizador.BI;
            user.NIF = utilizador.NIF;
            user.Email = utilizador.Email;
            if (utilizador.Tipo != null) {
                user.Tipo = utilizador.Tipo;
                user.Valido = utilizador.Valido;
            }
            db.SaveChanges();
               
            
            return View(utilizador);
        }

        // GET: Utilizadors/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Utilizador utilizador = db.Utilizador.Find(id);
            if (utilizador == null)
            {
                return HttpNotFound();
            }
            return View(utilizador);
        }

        // POST: Tratamentos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Utilizador utilizador = db.Utilizador.Find(id);
            string iD = utilizador.UserID;
            string role = utilizador.Tipo;
          //  db.Utilizador.Remove(utilizador);
          //  db.SaveChanges();
            return RedirectToAction("Delete", "Account", new { iD, role });
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
