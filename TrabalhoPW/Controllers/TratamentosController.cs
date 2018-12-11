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
    public class TratamentosController : Controller
    {
        private MuseuContext db = new MuseuContext();

        // GET: Tratamentos
        public ActionResult Index()
        {
            var tratamento = db.Tratamento.Include(t => t.Objeto);
            return View(tratamento.ToList());
        }

        // GET: Tratamentos/Details/5
        [Authorize(Roles = "Especialista, Admin")]

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tratamento tratamento = db.Tratamento.Include(a => a.Objeto).Where(m => m.TratID == id).First();
            if (tratamento == null)
            {
                return HttpNotFound();
            }
            return View(tratamento);
        }

        // GET: Tratamentos/Create
        [Authorize(Roles = "Especialista, Admin")]

        public ActionResult Create()
        {
            ViewBag.ObjID = new SelectList(db.Objeto, "ObjID", "Tipo");
            return View();
        }

        // POST: Tratamentos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TratID,Desc,Data,ObjID")] Tratamento tratamento)
        {
            if (ModelState.IsValid)
            {
                db.Tratamento.Add(tratamento);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ObjID = new SelectList(db.Objeto, "ObjID", "Tipo", tratamento.ObjID);
            return View(tratamento);
        }

        // GET: Tratamentos/Edit/5
        [Authorize(Roles = "Especialista, Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tratamento tratamento = db.Tratamento.Include(a => a.Objeto).Where(m => m.TratID == id).First();
            if (tratamento == null)
            {
                return HttpNotFound();
            }
            ViewBag.ObjID = new SelectList(db.Objeto, "ObjID", "Tipo", tratamento.ObjID);
            return View(tratamento);
        }

        // POST: Tratamentos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TratID,Desc,Data,ObjID")] Tratamento tratamento)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tratamento).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ObjID = new SelectList(db.Objeto, "ObjID", "Tipo", tratamento.ObjID);
            return View(tratamento);
        }

        // GET: Tratamentos/Delete/5
        [Authorize(Roles = "Especialista, Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tratamento tratamento = db.Tratamento.Include(a => a.Objeto).Where(m => m.TratID == id).First(); 
            if (tratamento == null)
            {
                return HttpNotFound();
            }
            return View(tratamento);
        }

        // POST: Tratamentos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tratamento tratamento = db.Tratamento.Find(id);
            db.Tratamento.Remove(tratamento);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize]
        public ActionResult Disp(int id)
        {

            var l = db.Tratamento.Include(p=> p.Objeto).Where(m => m.ObjID == id); 

            return View(l.ToList());
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
