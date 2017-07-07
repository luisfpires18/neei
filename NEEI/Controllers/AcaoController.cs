using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NEEI.Models;

namespace NEEI.Controllers
{
    public class AcaoController : Controller
    {
        /// Base de Dados;
        private ApplicationDbContext _context;

        public AcaoController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }


        // Ver Acções de um relatório;
        public ActionResult Index(int id)
        {
            var relatorio = _context.Relatorio.SingleOrDefault(r => r.Id == id);
            ViewBag.IdRelatorio = id;
            ViewBag.Relatorio = relatorio.Nome;

            var acoes = _context.Acao.SqlQuery("SELECT * FROM Acaos a WHERE a.RelatorioId = @id", new SqlParameter("@id", id)).ToList();

            return View("Index", acoes);
        }

        // View de adicionar Acção;
        public ActionResult New(int id)
        {
            var relatorio = _context.Relatorio.SingleOrDefault(r => r.Id == id);
            ViewBag.IdRelatorio = relatorio.Id;

            return View("AcaoForm");
        }

        // Criar Acção;
        [HttpPost]
        public ActionResult Create(Acao a)
        {
            var relatorio = _context.Relatorio.SingleOrDefault(r => r.Id == a.RelatorioId);
            a.Relatorios = relatorio;

            _context.Acao.Add(a);
            _context.SaveChanges();

            return RedirectToAction("Index", "Acao" + "/Index/" + a.RelatorioId);
        }

    }
}