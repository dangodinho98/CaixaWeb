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
        public ActionResult Index()
        {
            var estabelecimento = db.Estabelecimento.Include("Regiao").ToList().OrderBy(a=> a.Nome);
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
            var estabelecimentoVM = new EstabelecimentosViewModel();
            var userName = User.Identity.GetUserName();

            estabelecimentoVM.Estabelecimento = new Estabelecimentos() { Ativo = true, ModUser = userName };
            estabelecimentoVM.Regioes = db.Regioes.Where(x => x.UserName == userName).ToList();

            return View(estabelecimentoVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Estabelecimentos estabelecimentos)
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

            var estabelecimentoVM = new EstabelecimentosViewModel();
            var userName = User.Identity.GetUserName();

            estabelecimentoVM.Estabelecimento = db.Estabelecimento.Find(id);
            estabelecimentoVM.Regioes = db.Regioes.Where(x => x.UserName == userName).ToList();

            if (estabelecimentoVM == null)
            {
                return HttpNotFound();
            }
            return View(estabelecimentoVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EstabelecimentosViewModel estabelecimentosVM)
        {
            if (ModelState.IsValid)
            {
                estabelecimentosVM.Estabelecimento.ModUser = User.Identity.GetUserName();

                db.Entry(estabelecimentosVM.Estabelecimento).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(estabelecimentosVM);
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
