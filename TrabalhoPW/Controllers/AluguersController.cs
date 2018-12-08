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
    public class AluguersController : Controller
    {
        private MuseuContext db = new MuseuContext();

        // GET: Aluguers
        [Authorize(Roles = "Especialista, Admin")]
        public ActionResult Index(int flag)
        {
            var aluguer= db.Aluguer.Include(a => a.Objeto).Include(a => a.Requerente);
            switch (flag) {
                case 0: //lista total
                    return View(aluguer.ToList());

                case 1: //lista de alugueres ja entregues a aguardar avaliação
                    aluguer = db.Aluguer.Where(m => m.DataEntrega != null && m.Validado == true && m.EstadoF==null);
                    return View(aluguer.ToList());

                case 2: //lista de Alugueres por validar
                    aluguer = db.Aluguer.Where(m => m.Validado == false);
                    return View(aluguer.ToList());

                case 3: //lista de Alugueres com prazo expirado
                    aluguer = db.Aluguer.Where(m => m.DataFim < DateTime.Today);
                    return View(aluguer.ToList());

                case 4: //lista de Alugueres a decorrer
                    aluguer = db.Aluguer.Where(m => m.DataEntrega == null && m.Validado==true);
                    return View(aluguer.ToList());
                default:
                    return View();
            }
        }
        [Authorize]
        public ActionResult Historico()
        {
            var aluguer = db.Aluguer.Where(m => m.Requerente.Nome == User.Identity.Name);
            return View(aluguer.ToList());
        }

            // GET: Aluguers/Details/5
            public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Aluguer aluguer = db.Aluguer.Find(id);
            if (aluguer == null)
            {
                return HttpNotFound();
            }
            return View(aluguer);
        }

        // GET: Aluguers/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.ObjID = new SelectList(db.Objeto, "ObjID", "Tipo");
            ViewBag.RequerenteID = new SelectList(db.Utilizador, "UtilizadorID", "Nome");
            return View();
        }

        // POST: Aluguers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AluguerID,ObjID,DataIncio,DataFim,Finalidade,Validado,RequerenteID,EstadoI,EstadoF,Relatorio")] Aluguer aluguer)
        {
            if (ModelState.IsValid)
            {
                db.Aluguer.Add(aluguer);
                db.SaveChanges();
                return RedirectToAction("Index", new { flag=0});
            }

            ViewBag.ObjID = new SelectList(db.Objeto, "ObjID", "Tipo", aluguer.ObjID);
            ViewBag.RequerenteID = new SelectList(db.Utilizador, "UtilizadorID", "Nome", aluguer.RequerenteID);
            return View(aluguer);
        }

        // GET: Aluguers/Edit/5
        [Authorize(Roles = "Especialista, Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Aluguer aluguer = db.Aluguer.Find(id);
            if (aluguer == null)
            {
                return HttpNotFound();
            }
            ViewBag.ObjID = new SelectList(db.Objeto, "ObjID", "Tipo", aluguer.ObjID);
            var list = (db.Utilizador.Where(m => m.UtilizadorID == aluguer.RequerenteID));
            ViewBag.RequerenteID = new SelectList(list.ToList(), "UtilizadorID", "Nome", aluguer.RequerenteID);
            return View(aluguer);
        }

        // POST: Aluguers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AluguerID,ObjID,DataIncio,DataFim,Finalidade,Validado,RequerenteID,EstadoI,EstadoF,Relatorio")] Aluguer aluguer)
        {
            Aluguer data = (Aluguer)db.Aluguer.Where(m => m.AluguerID == aluguer.AluguerID).First();
                data.Validado = aluguer.Validado;

            db.SaveChanges();
                return RedirectToAction("Index");
            

        }

        // GET: Aluguers/Delete/5
        [Authorize(Roles = "Especialista, Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Aluguer aluguer = db.Aluguer.Find(id);
            if (aluguer == null)
            {
                return HttpNotFound();
            }
            return View(aluguer);
        }

        // POST: Aluguers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Aluguer aluguer = db.Aluguer.Find(id);
            db.Aluguer.Remove(aluguer);
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
