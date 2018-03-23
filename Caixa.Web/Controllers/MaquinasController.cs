using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Caixa.Web.Models;
using Microsoft.AspNet.Identity;

namespace Caixa.Web.Controllers
{
    public class MaquinasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Maquinas
        public ActionResult Index()
        {
            return View(db.Maquina.Include("Estabelecimento").ToList());
        }

        // GET: Maquinas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Maquinas maquinas = db.Maquina.Find(id);
            if (maquinas == null)
                return HttpNotFound();

            var mvm = new MaquinaViewModel();
            mvm.Maquina = maquinas;
            mvm.Estabelecimentos = db.Estabelecimento.Where(x => x.Ativo == true).ToList();

            return View(mvm);
        }

        // GET: Maquinas/Create
        public ActionResult Create()
        {
            var mvm = new MaquinaViewModel();
            mvm.Maquina = new Maquinas() { Ativo = true };
            mvm.Estabelecimentos = db.Estabelecimento.Where(x => x.Ativo == true).ToList();
            return View(mvm);
        }

        // POST: Maquinas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MaquinaViewModel maquinas)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    maquinas.Maquina.ModUser = User.Identity.GetUserName();

                    db.Maquina.Add(maquinas.Maquina);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException /* dex */)
            {
                ModelState.AddModelError("", "Não foi possível salvar as alterações. Tente de novo, e se o problema persistir consulte o administrador do sistema.");
            }

            return View(maquinas);
        }

        // GET: Maquinas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Maquinas maquinas = db.Maquina.Find(id);

            if (maquinas == null)
                return HttpNotFound();

            var mvm = new MaquinaViewModel();
            mvm.Maquina = maquinas;
            mvm.Estabelecimentos = db.Estabelecimento.Where(x => x.Ativo == true).ToList();

            return View(mvm);
        }

        // POST: Maquinas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MaquinaViewModel maquinas)
        {
            if (ModelState.IsValid)
            {
                maquinas.Maquina.ModUser = User.Identity.GetUserName();

                db.Entry(maquinas.Maquina).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(maquinas);
        }

        // GET: Maquinas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Maquinas maquinas = db.Maquina.Find(id);
            if (maquinas == null)
            {
                return HttpNotFound();
            }
            return View(maquinas);
        }

        // POST: Maquinas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Maquinas maquinas = db.Maquina.Find(id);
            db.Maquina.Remove(maquinas);
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

        public ActionResult RelatorioMaquinas()
        {
            return View();
        }
    }
}
