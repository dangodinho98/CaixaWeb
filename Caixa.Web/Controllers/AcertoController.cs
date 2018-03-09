using Caixa.Web.Models;
using Caixa.Web.Report;
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
    [Authorize]
    public class AcertoController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Acerto
        public ActionResult Index()
        {
            return View(db.Acerto.Include("Estabelecimento").Include("Maquina").ToList().OrderByDescending(x => x.Id).ThenByDescending(x => x.Data));
        }

        // GET: Lancamento
        public ActionResult Lancamento()
        {
            var userName = User.Identity.GetUserName();

            var regioesVM = new RegioesViewModel();
            IList<Estabelecimentos> estabelecimentoList = new List<Estabelecimentos>();

            var regioes = db.Regioes.Where(x => x.UserName == userName).ToList();

            regioesVM.Regioes = regioes;

            foreach (var item in regioes)
            {
                var estabelecimentos = db.Estabelecimento.Include("Regiao").Include("Maquinas").Where(x => x.Regiao.Nome == item.Nome).ToList();

                foreach (var est in estabelecimentos)
                {
                    estabelecimentoList.Add(est);
                }
            }

            regioesVM.Estabelecimentos = estabelecimentoList;

            return View(regioesVM);
        }


        // GET: Acerto/Create
        public ActionResult Create(int? idEstabelecimento)
        {

            var username = User.Identity.GetUserName();
            var accountSecurity = db.Security.FirstOrDefault(a => a.UserName == username);

            if (accountSecurity != null && accountSecurity.Level == 5)
            {
                var acertoVM = new AcertoViewModel();

                acertoVM.Acerto = new Acerto() {
                    Data = DateTime.Now.Date,
                    IdEstabelecimento = idEstabelecimento.GetValueOrDefault()};

                acertoVM.Estabelecimentos = db.Estabelecimento.Where(x => x.Id == idEstabelecimento).ToList();
                acertoVM.Maquinas = db.Maquina.Where(x => x.Ativo == true && x.IdEstabelecimento == idEstabelecimento).ToList();
                acertoVM.Comissionados = db.Comissionado.Where(x => x.Bloqueado == false).ToList();

                return View(acertoVM);
            }
            else
            {
                return HttpNotFound();
            }
        }

        // POST: Acerto/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Acerto acerto)
        {
            #region View Model
            var acertoVM = new AcertoViewModel();
            acertoVM.Acerto = acerto;
            acertoVM.Estabelecimentos = db.Estabelecimento.Where(x => x.Id == acerto.IdEstabelecimento).ToList();
            acertoVM.Maquinas = db.Maquina.Where(x => x.Id == acerto.IdMaquina).ToList();
            acertoVM.Comissionados = db.Comissionado.Where(x => x.Bloqueado == false).ToList();
            #endregion

            try
            {
                if (ModelState.IsValid)
                {
                    var maquina = db.Maquina.Find(acerto.IdMaquina);
                    acerto.Maquina = maquina;

                    #region Trata DI / DS
                    if (acerto.Entrada > acerto.Maquina.DI)
                    {
                        acerto.Maquina.DI = acerto.Entrada;
                    }
                    else
                    {
                        TempData["error"] = String.Format("DI (Entrada) não pode ser menor que {0}.", acerto.Maquina.DI);
                        return View("Create", acertoVM);
                    }

                    if (acerto.Saida > acerto.Maquina.DS)
                    {
                        acerto.Maquina.DS = acerto.Saida;
                    }
                    else
                    {
                        TempData["error"] = String.Format("DS (Saída) não pode ser menor que {0}.", acerto.Maquina.DS);
                        return View("Create", acertoVM);
                    }
                    #endregion

                    #region Calculos
                    var comissao = (acerto.Entrada * acerto.Comissao) / 100;
                    acerto.Subtotal = acerto.Entrada - (acerto.Despesa + acerto.Quebra + acerto.Saida);

                    if (comissao == null)
                        comissao = 0;

                    acerto.Total = acerto.Entrada - (acerto.Despesa.GetValueOrDefault(0) + acerto.Quebra.GetValueOrDefault(0) + acerto.Saida + comissao.GetValueOrDefault(0));
                    #endregion

                    if (acerto.Total.HasValue)
                    {
                        db.Acerto.Add(acerto);
                        db.SaveChanges();
                        TempData["success"] = "Acerto lançado com sucesso!";
                    }
                    else
                    {
                        TempData["error"] = "Verifique as informações e tente novamente.";
                    }

                    return View("Create", acertoVM);
                    //return RedirectToAction("Index");
                }
            }
            catch (DataException /* dex */)
            {
                ModelState.AddModelError("", "Não foi possível salvar as alterações. Tente de novo, e se o problema persistir consulte o administrador do sistema.");
            }

            return View(acertoVM);
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
            mvm.Comissionados = db.Comissionado.Where(x => x.Bloqueado == false).ToList();

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
            mvm.Comissionados = db.Comissionado.Where(x => x.Bloqueado == false).ToList();

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

        #region Relatórios

        public FileResult BaixarPDF()
        {
            var rpt = getRelatorio();

            return File(rpt.GetOutput().GetBuffer(), "application/pdf", "Documento.pdf");
        }

        private AcertoReportViewer getRelatorio()
        {
            var rpt = new AcertoReportViewer()
            {
                BasePath = Server.MapPath("/"),
                PageTitle = "Relatório de Acerto",
                ImprimirCabecalhoPadrao = true,
                ImprimirRodapePadrao = true
            };

            return rpt;
        }

        public ActionResult Preview()
        {
            var rpt = getRelatorio();

            return File(rpt.GetOutput().GetBuffer(), "application/pdf");
        }

        private ComprovanteAcertoReportViewer getRelatorioComprovante(int IdAcerto)
        {
            var acerto = db.Acerto.Find(IdAcerto);
            var estabelecimento = db.Estabelecimento.Find(acerto.IdEstabelecimento);
            var maquina = db.Maquina.Find(acerto.IdMaquina);

            var rpt = new ComprovanteAcertoReportViewer() {
                Author = User.Identity.GetUserName(),
                acerto = acerto,
                estabelecimento = estabelecimento,
                maquina = maquina,
                BasePath = Server.MapPath("/"),
                PageTitle = estabelecimento.Nome,
                PageSubTitle = "Acerto n°: " + acerto.Id,
                ImprimirCabecalhoPadrao = true,
                ImprimirRodapePadrao = true
            };
            return rpt;
        }

        public ActionResult PreviewComprovante(int id)
        {
            var rpt = getRelatorioComprovante(id);

            return File(rpt.GetOutput().GetBuffer(), "application/pdf");
        }
        #endregion


    }
}