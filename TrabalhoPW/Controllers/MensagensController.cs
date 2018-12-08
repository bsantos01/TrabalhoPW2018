using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TrabalhoPW.Models;

namespace TrabalhoPW.Controllers
{
    public class MensagensController : Controller
    {
        private MuseuContext db = new MuseuContext();

        // GET: Mensagens
        [Authorize(Roles = "Especialista, Membro")]
        public ActionResult Index()
        {
            // var query = db.Utilizador.Where(s => s.Nome == User.Identity.Name).FirstOrDefault<Utilizador>();

            var mensagem = db.Mensagem.Where(m => m.Destinatario.Nome == User.Identity.Name);
            return View(mensagem.ToList());
        }
        [Authorize(Roles = "Especialista, Membro")]
        public ActionResult Sent()
        {
            // var query = db.Utilizador.Where(s => s.Nome == User.Identity.Name).FirstOrDefault<Utilizador>();

            var mensagem = db.Mensagem.Where(m => m.RemNome == User.Identity.Name);
            foreach (var item in mensagem.ToList()) {
                item.Destinatario = db.Utilizador.Where(m => m.UtilizadorID == item.DestinatarioID).First();
            }

            return View(mensagem.ToList());
        }
        [Authorize(Roles = "Especialista, Membro")]
        // GET: Mensagens/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mensagem mensagem = db.Mensagem.Find(id);
            if (mensagem == null)
            {
                return HttpNotFound();
            }
            return View(mensagem);
        }

        // GET: Mensagens/Create

        [Authorize(Roles = "Especialista, Membro")]
        public ActionResult Create()
        {
            if (Request.QueryString["i"] != null)
            {
                string dest = Request.QueryString["i"].ToString();

                ViewBag.DestinatarioID = new SelectList(db.Utilizador.Where(m => m.Nome == dest), "UtilizadorID", "Nome");
            }
            else
            {
                ViewBag.DestinatarioID = new SelectList(db.Utilizador.Where(m => m.Tipo != "Admin" && m.Nome != User.Identity.Name), "UtilizadorID", "Nome");
            }
                return View();
 
        }

        // POST: Mensagens/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MensagemID,RemNome,DestinatarioID,Conteudo,data")] Mensagem mensagem)
        {
            if (ModelState.IsValid)
            {

                var query = db.Utilizador.Where(s => s.Nome == User.Identity.Name).FirstOrDefault<Utilizador>();
                mensagem.data= DateTime.Today;
                mensagem.RemNome = query.Nome;
                db.Mensagem.Add(mensagem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DestinatarioID = new SelectList(db.Utilizador, "UtilizadorID", "Nome", mensagem.DestinatarioID);
            return View(mensagem);
        }

        // GET: Mensagens/Delete/5
        [Authorize(Roles = "Especialista, Membro")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mensagem mensagem = db.Mensagem.Find(id);
            if (mensagem == null)
            {
                return HttpNotFound();
            }
            return View(mensagem);
        }

        // POST: Mensagens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Mensagem mensagem = db.Mensagem.Find(id);
            db.Mensagem.Remove(mensagem);
            db.SaveChanges();
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
