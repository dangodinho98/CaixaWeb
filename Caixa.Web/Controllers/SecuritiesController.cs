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
    public class SecuritiesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Securities
        public ActionResult Index()
        {
            return View(db.Security.ToList());
        }

        // GET: Securities/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Security security = db.Security.Find(id);
            if (security == null)
            {
                return HttpNotFound();
            }
            return View(security);
        }

        // GET: Securities/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Securities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserName,Level")] Security security)
        {
            if (ModelState.IsValid)
            {
                db.Security.Add(security);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(security);
        }

        // GET: Securities/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Security security = db.Security.Find(id);
            if (security == null)
            {
                return HttpNotFound();
            }
            return View(security);
        }

        // POST: Securities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserName,Level")] Security security)
        {
            if (ModelState.IsValid)
            {
                db.Entry(security).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(security);
        }

        // GET: Securities/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Security security = db.Security.Find(id);
            if (security == null)
            {
                return HttpNotFound();
            }
            return View(security);
        }

        // POST: Securities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Security security = db.Security.Find(id);
            db.Security.Remove(security);
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
