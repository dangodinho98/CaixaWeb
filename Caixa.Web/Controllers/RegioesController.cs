using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Caixa.Web.Models;

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
            return View();
        }

        // POST: Regioes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Regiao,UserName")] Regioes regioes)
        {
            if (ModelState.IsValid)
            {
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

        // POST: Regioes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Regiao,UserName")] Regioes regioes)
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
