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
    public class RegioesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Regioes
        public ActionResult Index()
        {
            return View(db.Regioes.ToList());
        }

        // GET: Regioes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Regioes regioes = db.Regioes.Find(id);
            if (regioes == null)
            {
                return HttpNotFound();
            }
            return View(regioes);
        }

        // GET: Regioes/Create
        public ActionResult Create()
        {
            var username = User.Identity.GetUserName();
            var accountSecurity = db.Security.FirstOrDefault(a => a.UserName == username);

            if (accountSecurity != null && accountSecurity.Level == 5)
            {
                //var local = db.Estabelecimento.Where(x => x.Ativo == true).ToList();
                var regiaoVM = new Regioes() { UserName = username };

                return View(regiaoVM);
            }
            else
            {
                return HttpNotFound();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Regioes regioes)
        {
            if (ModelState.IsValid)
            {
                //Trata estabelecimentos
                //var estabelecimentoDB = db.Estabelecimento.Find(regioes.IdEstabelecimento);
                //regioes.Estabelecimentos = estabelecimentoDB;

                db.Regioes.Add(regioes);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(regioes);
        }

        // GET: Regioes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Regioes regioes = db.Regioes.Find(id);
            if (regioes == null)
            {
                return HttpNotFound();
            }
            return View(regioes);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Regioes regioes)
        {
            if (ModelState.IsValid)
            {
                db.Entry(regioes).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(regioes);
        }

        // GET: Regioes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Regioes regioes = db.Regioes.Find(id);
            if (regioes == null)
            {
                return HttpNotFound();
            }
            return View(regioes);
        }

        // POST: Regioes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Regioes regioes = db.Regioes.Find(id);
            db.Regioes.Remove(regioes);
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
