using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NEEI.Models;

namespace NEEI.Controllers
{
    public class CategoriaController : Controller
    {
        /// Base de Dados;
        private ApplicationDbContext _context;

        public CategoriaController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // Ver Categorias de uma acção;
        public ActionResult Index(int id)
        {
            var acao = _context.Acao.SingleOrDefault(a => a.Id == id);
            ViewBag.IdAcao = id;
            ViewBag.Acao = acao.Descricao;
            ViewBag.IdRelatorio = acao.RelatorioId;

            var categorias = _context.Categoria.SqlQuery("SELECT * FROM Categorias c WHERE c.AcaoId = @id", new SqlParameter("@id", id)).ToList();

            return View("Index", categorias);
        }

        // View de adicionar Categoria;
        public ActionResult New(int id)
        {
            var accao = _context.Acao.SingleOrDefault(a => a.Id == id);
            ViewBag.IdAcao = accao.Id;

            return View("CategoriaForm");
        }

        // Criar Categoria;
        [HttpPost]
        public ActionResult Create(Categoria c)
        {
            var acao = _context.Acao.SingleOrDefault(a => a.Id == c.AcaoId);
            c.Acaos = acao;

            _context.Categoria.Add(c);
            _context.SaveChanges();

            return RedirectToAction("Index", "Categoria" + "/Index/" + c.AcaoId);
        }

    }
}