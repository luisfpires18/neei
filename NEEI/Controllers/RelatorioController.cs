using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NEEI.Models;

namespace NEEI.Controllers
{
    public class RelatorioController : Controller
    {
        /// Base de Dados;
        private ApplicationDbContext _context;

        public RelatorioController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // Ver Relatorios;
        public ActionResult Index()
        {
            var relatorios = _context.Relatorio.ToList();
            return View(relatorios);
        }

        // View de adicionar Relatorio;
        public ActionResult New()
        {
            return View("RelatorioForm");
        }

        // Criar Relatorio;
        [HttpPost]
        public ActionResult Create(Relatorio rc)
        {
            _context.Relatorio.Add(rc);
            _context.SaveChanges();

            return RedirectToAction("Index", "Relatorio");
        }
    }
}