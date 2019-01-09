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
        [Authorize]
        public ActionResult Index(int flag)
        {
            var aluguer= db.Aluguer.Include(a => a.Objeto).Include(a => a.Requerente);
            switch (flag) {
                case 0: //lista total
                    return View(aluguer.ToList());

                case 1: //lista de alugueres ja entregues a aguardar avaliação
                    aluguer = db.Aluguer.Include(a => a.Objeto).Include(a => a.Requerente).Where(m => m.DataEntrega != null && m.Validado && m.EstadoF==0);
                    return View(aluguer.ToList());

                case 2: //lista de Alugueres por validar
                    return RedirectToAction("ValidateList");

                case 3: //lista de Alugueres com prazo expirado
                    aluguer = db.Aluguer.Include(a => a.Objeto).Include(a => a.Requerente).Where(m => m.DataFim < DateTime.Today && m.Validado && m.DataEntrega == null);
                    return View(aluguer.ToList());

                case 4: //lista de Alugueres a decorrer
                    aluguer = db.Aluguer.Include(a => a.Objeto).Include(a => a.Requerente).Where(m => m.DataEntrega == null && m.Validado==true);
                    return View(aluguer.ToList());
                case 5://lista de alugueres do utilizador corrente
                    aluguer = db.Aluguer.Include(a => a.Objeto).Include(a => a.Requerente).Where(m => m.Requerente.Nome == User.Identity.Name);
                    return View(aluguer.ToList());
                default:
                    return View();
            }
        }

        [Authorize(Roles = "Admin")]
        public ActionResult ValidateList(int? i) {

            if (i != null)
            {
                ViewBag.error="Impossivel validar Aluguer por indisponibilidade de Datas.";
            }
            var aluguer = db.Aluguer.Include(a => a.Objeto).Include(a => a.Requerente).Where(m => m.Validado == false);
            return View(aluguer.ToList());
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Validate(int id)
        {
            Aluguer data = (Aluguer)db.Aluguer.Where(m => m.AluguerID == id).First();
            var list = db.Aluguer.Where(m => m.Validado).ToList();
            foreach (Aluguer item in list)
            {
                if ((data.DataIncio >= item.DataIncio && data.DataIncio <= item.DataFim)
                    || (data.DataFim >= data.DataIncio && data.DataFim <= item.DataFim))
                {
                    
                    return RedirectToAction("ValidateList", new { i=0});

                }
            }
            data.Validado = true;
            db.SaveChanges();


            return RedirectToAction("ValidateList");
        }


        // GET: Aluguers/Details/5
        [Authorize]
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
        public ActionResult Create(int? id)
        {
            if (id != null) {
                ViewBag.RequerenteID = new SelectList(db.Utilizador.Where(m => m.Nome == User.Identity.Name), "UtilizadorID", "Nome");
                ViewBag.ObjID = new SelectList(db.Objeto.Where(o=> o.ObjID == id), "ObjID", "Tipo");
                return View();
            }
            if (User.IsInRole("Membro"))
            {
                ViewBag.RequerenteID = new SelectList(db.Utilizador.Where(m => m.Nome == User.Identity.Name), "UtilizadorID", "Nome");
            }
            else {
                ViewBag.RequerenteID = new SelectList(db.Utilizador.Where(m => m.Tipo != "Admin" && m.Tipo != "Especialista" && m.Valido==true), "UtilizadorID", "Nome");
            }
            ViewBag.ObjID = new SelectList(db.Objeto, "ObjID", "Tipo");

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
                if (aluguer.DataIncio >= aluguer.DataFim) {
                    ModelState.AddModelError("", "Datas Inconsistentes");
                    ViewBag.RequerenteID = new SelectList(db.Utilizador.Where(m => m.UtilizadorID == aluguer.RequerenteID), "UtilizadorID", "Nome");
                    ViewBag.ObjID = new SelectList(db.Objeto, "ObjID", "Tipo");
                    return View(aluguer);
                }
                var list = db.Aluguer.Where(m => m.Validado).ToList();
                foreach (Aluguer item in list)
                {
                    if((aluguer.DataIncio >= item.DataIncio && aluguer.DataIncio<= item.DataFim)
                        || (aluguer.DataFim >= item.DataIncio && aluguer.DataFim <= item.DataFim))
                    {
                        ModelState.AddModelError("", "Data Indisponivel para emprestimo");
                        ViewBag.RequerenteID = new SelectList(db.Utilizador.Where(m=> m.UtilizadorID == aluguer.RequerenteID), "UtilizadorID", "Nome");
                        ViewBag.ObjID = new SelectList(db.Objeto, "ObjID", "Tipo");
                        return View(aluguer);
                    }
                }
                db.Aluguer.Add(aluguer);
                db.SaveChanges();
                if (User.IsInRole("Membro")) {
                    return RedirectToAction("Index", new { flag = 5 });

                }
                else { 
                    return RedirectToAction("Index", new { flag=0});
                }
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
        public ActionResult Edit([Bind(Include = "AluguerID,ObjID,DataIncio,DataFim,DataEntrega,Finalidade,Validado,RequerenteID,EstadoI,EstadoF,Relatorio")] Aluguer aluguer)
        {
            Aluguer data = (Aluguer)db.Aluguer.Where(m => m.AluguerID == aluguer.AluguerID).First();
            data.EstadoF = aluguer.EstadoF;
            data.Relatorio = aluguer.Relatorio;
            data.EstadoI = aluguer.EstadoI;

            if(aluguer.DataEntrega!=null)
            {
                data.DataEntrega = aluguer.DataEntrega;
            }

            db.SaveChanges();

            if (data.EstadoF != 0 && data.DataEntrega != null)
            {
                var lista = db.Aluguer.Where(m => m.RequerenteID == data.RequerenteID).ToList();
                int points = 0, c = 0; ;
                foreach(Aluguer a in lista)
                {

                    if (a.DataEntrega != null && a.EstadoF != 0)
                    {
                        c++;
                        if (a.DataEntrega != null && a.DataEntrega <= a.DataFim)
                        {
                            if(a.EstadoF <= a.EstadoI) { 
                            points++;
                            }
                        }
                    }
                }
                decimal feed = (points * 100) / c;
                decimal.Round(feed, 2, MidpointRounding.AwayFromZero);
                Utilizador user = (Utilizador)db.Utilizador.Where(u => u.UtilizadorID == data.RequerenteID).First();
                user.Feedback = feed;
                db.SaveChanges();


            }
            return RedirectToAction("Index", new { flag = 0 });


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
            return RedirectToAction("Index", new { flag = 0 });
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
