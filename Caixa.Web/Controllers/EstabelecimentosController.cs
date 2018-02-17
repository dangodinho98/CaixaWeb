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
    public class EstabelecimentosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Estabelecimentos
        public ActionResult Index(string searchString)
        {
            var estabelecimento = db.Estabelecimento.ToList();
           
            return View(estabelecimento);
        }

        // GET: Estabelecimentos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Estabelecimentos estabelecimentos = db.Estabelecimento.Find(id);
            if (estabelecimentos == null)
            {
                return HttpNotFound();
            }
            return View(estabelecimentos);
        }

        // GET: Estabelecimentos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Estabelecimentos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nome,Endereco,Telefone,Regiao,Observacao,Ativo")] Estabelecimentos estabelecimentos)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    estabelecimentos.ModUser = User.Identity.GetUserName();

                    db.Estabelecimento.Add(estabelecimentos);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException /* dex */)
            {
                ModelState.AddModelError("", "Não foi possível salvar as alterações. Tente de novo, e se o problema persistir consulte o administrador do sistema.");
            }
            return View(estabelecimentos);
        }

        // GET: Estabelecimentos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Estabelecimentos estabelecimentos = db.Estabelecimento.Find(id);
            if (estabelecimentos == null)
            {
                return HttpNotFound();
            }
            return View(estabelecimentos);
        }

        // POST: Estabelecimentos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nome,Endereco,Telefone,Regiao,Observacao,Ativo")] Estabelecimentos estabelecimentos)
        {
            if (ModelState.IsValid)
            {
                estabelecimentos.ModUser = User.Identity.GetUserName();

                db.Entry(estabelecimentos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(estabelecimentos);
        }

        // GET: Estabelecimentos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Estabelecimentos estabelecimentos = db.Estabelecimento.Find(id);
            if (estabelecimentos == null)
            {
                return HttpNotFound();
            }
            return View(estabelecimentos);
        }

        // POST: Estabelecimentos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Estabelecimentos estabelecimentos = db.Estabelecimento.Find(id);
            db.Estabelecimento.Remove(estabelecimentos);
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

        public ActionResult RelatorioEstabelecimentos()
        {
            var estabelecimentos = db.Estabelecimento.ToList();
            return View(estabelecimentos);
        }


    }
}
