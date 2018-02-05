using Caixa.Web.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Caixa.Web.Controllers
{
    public class AcertoController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Maquinas
        public ActionResult Index()
        {
            return View(db.Acerto.Include("Estabelecimento").Include("Maquina").ToList());
        }

        // GET: Acerto/Create
        public ActionResult Create()
        {
            var acertoVM = new AcertoViewModel();

            acertoVM.Acerto = new Acerto() { Data = DateTime.Now };
            acertoVM.Estabelecimentos = db.Estabelecimento.Where(x => x.Ativo == true).ToList();
            acertoVM.Maquinas = db.Maquina.Where(x => x.Ativo == true).ToList();

            return View(acertoVM);
        }

        // POST: Acerto/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Acerto acerto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    #region Calculos
                    var comissao = (acerto.Entrada / acerto.Comissao);
                    acerto.Subtotal = acerto.Entrada - (acerto.Despesa + acerto.Quebra + acerto.Saida);
                    acerto.Total = acerto.Entrada - (acerto.Despesa + acerto.Quebra + acerto.Saida + comissao);
                    #endregion

                    if (acerto.Total.HasValue)
                    {
                        db.Acerto.Add(acerto);
                        db.SaveChanges();
                    }

                    return RedirectToAction("Index");
                }
            }
            catch (DataException /* dex */)
            {
                ModelState.AddModelError("", "Não foi possível salvar as alterações. Tente de novo, e se o problema persistir consulte o administrador do sistema.");
            }
            return View(acerto);

        }

        // GET: Acerto/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Acerto acerto = db.Acerto.Find(id);
            if (acerto == null)
                return HttpNotFound();

            var mvm = new AcertoViewModel();
            mvm.Acerto = acerto;
            mvm.Estabelecimentos = db.Estabelecimento.Where(x => x.Ativo == true).ToList();
            mvm.Maquinas = db.Maquina.Where(x => x.Ativo == true).ToList();

            return View(mvm);
        }

        // GET: Maquinas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Acerto acerto = db.Acerto.Find(id);

            if (acerto == null)
                return HttpNotFound();

            var mvm = new AcertoViewModel();
            mvm.Acerto = acerto;
            mvm.Estabelecimentos = db.Estabelecimento.Where(x => x.Ativo == true).ToList();
            mvm.Maquinas = db.Maquina.Where(x => x.Ativo == true).ToList();

            return View(mvm);
        }

        // POST: Maquinas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AcertoViewModel acertos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(acertos.Acerto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(acertos);
        }


        // GET: Acerto/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Acerto acerto = db.Acerto.Find(id);
            if (acerto == null)
            {
                return HttpNotFound();
            }
            return View(acerto);
        }

        // POST: Acerto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //validação para tratar qual usuário pode realizar exclusão
            //Salvar log de exclusão
            Acerto acerto = db.Acerto.Find(id);
            db.Acerto.Remove(acerto);
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