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
    public class ObjetoesController : Controller
    {
        private MuseuContext db = new MuseuContext();

        // GET: Objetoes
        public ActionResult Index()
        {
            return View(db.Objeto.ToList());
        }

        // GET: Objetoes/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Objeto objeto = db.Objeto.Find(id);
            if (objeto == null)
            {
                return HttpNotFound();
            }
            return View(objeto);
        }

        [Authorize]
        public ActionResult Disp(int id) {

            var l = db.Aluguer.Include(m=> m.Objeto).Where(a=> a.Validado && a.ObjID== id);

            return View(l.ToList());
        }
        // GET: Objetoes/Create
        [Authorize(Roles = "Especialista, Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Objetoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ObjID,Tipo,Periodo,Zona,Origem,Descricao")] Objeto objeto)
        {
            if (ModelState.IsValid)
            {
                db.Objeto.Add(objeto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(objeto);
        }

        // GET: Objetoes/Edit/5
        [Authorize(Roles = "Especialista, Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Objeto objeto = db.Objeto.Find(id);
            if (objeto == null)
            {
                return HttpNotFound();
            }
            return View(objeto);
        }

        // POST: Objetoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ObjID,Tipo,Periodo,Zona,Origem,Descricao")] Objeto objeto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(objeto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(objeto);
        }

        // GET: Objetoes/Delete/5
        [Authorize(Roles = "Especialista, Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Objeto objeto = db.Objeto.Find(id);
            if (objeto == null)
            {
                return HttpNotFound();
            }
            return View(objeto);
        }

        // POST: Objetoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Objeto objeto = db.Objeto.Find(id);
            db.Objeto.Remove(objeto);
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
