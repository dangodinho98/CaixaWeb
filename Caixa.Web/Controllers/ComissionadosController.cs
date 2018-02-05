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
    public class ComissionadosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Comissionados
        public ActionResult Index()
        {
            return View(db.Comissionado.ToList());
        }

        // GET: Comissionados/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comissionado comissionado = db.Comissionado.Find(id);
            if (comissionado == null)
            {
                return HttpNotFound();
            }
            return View(comissionado);
        }

        // GET: Comissionados/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Comissionados/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nome,Telefone,CPF,Comissao,GanhoAcumulado,Bloqueado")] Comissionado comissionado)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    comissionado.ModUser = User.Identity.GetUserName();

                    db.Comissionado.Add(comissionado);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException /* dex */)
            {
                ModelState.AddModelError("", "Não foi possível salvar as alterações. Tente de novo, e se o problema persistir consulte o administrador do sistema.");
            }
            return View(comissionado);
        }

        // GET: Comissionados/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comissionado comissionado = db.Comissionado.Find(id);
            if (comissionado == null)
            {
                return HttpNotFound();
            }
            return View(comissionado);
        }

        // POST: Comissionados/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nome,Telefone,CPF,Comissao,GanhoAcumulado,Bloqueado")] Comissionado comissionado)
        {
            if (ModelState.IsValid)
            {
                comissionado.ModUser = User.Identity.GetUserName();

                db.Entry(comissionado).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(comissionado);
        }

        // GET: Comissionados/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comissionado comissionado = db.Comissionado.Find(id);
            if (comissionado == null)
            {
                return HttpNotFound();
            }
            return View(comissionado);
        }

        // POST: Comissionados/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Comissionado comissionado = db.Comissionado.Find(id);
            db.Comissionado.Remove(comissionado);
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
