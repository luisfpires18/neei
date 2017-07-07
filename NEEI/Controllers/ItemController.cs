using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NEEI.Models;

namespace NEEI.Controllers
{
    public class ItemController : Controller
    {
        /// Base de Dados;
        private ApplicationDbContext _context;

        public ItemController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // Ver Items de uma categoria;
        public ActionResult Index(int id)
        {
            var categoria = _context.Categoria.SingleOrDefault(c => c.Id == id);
            ViewBag.IdCategoria = id;
            ViewBag.Categoria = categoria.Descricao;
            ViewBag.IdAcao = categoria.AcaoId;

            var items = _context.Item.SqlQuery("SELECT * FROM Items i WHERE i.CategoriaId = @id", new SqlParameter("@id", id)).ToList();

            return View("Index", items);
        }

        // Views de adicionar Item;
        public ActionResult New(int id)
        {
            var categoria = _context.Categoria.SingleOrDefault(c => c.Id == id);
            ViewBag.IdCategoria = id;
        
            return View("ItemForm");
        }

        // Criar Item;
        [HttpPost]
        public ActionResult Create(Item i)
        {
            // Valores de Receita e Despesa;

            if (i.Quantidade == 0)
            {
                i.Quantidade = 1;
            }

            if(i.Quantidade > 1)
            {
                i.Receita = i.Receita * i.Quantidade;
                i.Despesa = i.Despesa * i.Quantidade;
            }
            
            // Associar Categoria ao Item;
            var categoria = _context.Categoria.SingleOrDefault(c => c.Id == i.CategoriaId);
            var acao = _context.Acao.SingleOrDefault(a => a.Id == categoria.AcaoId);
            var relatorio = _context.Relatorio.SingleOrDefault(r => r.Id == acao.RelatorioId);

            i.Categorias = categoria;
            // Adicionar Item;
            _context.Item.Add(i);
            _context.SaveChanges();

            // Mudança de valores em CATEGORIA;
            var items = _context.Item.SqlQuery("SELECT * FROM Items i WHERE i.CategoriaId = @id", new SqlParameter("@id", categoria.Id)).ToList();

            if (items.Count != 0)
            {
                categoria.Lucro = 0;
                foreach (var item in items)
                {
                    categoria.Lucro += item.Receita - item.Despesa;
                }
            }
            else
            {
                categoria.Lucro += i.Receita - i.Despesa;
            }
            

            // Mudança de valores em Acção;
            var categorias = _context.Categoria.SqlQuery("SELECT * FROM Categorias c WHERE c.AcaoID = @id", new SqlParameter("@id", categoria.AcaoId)).ToList();

            acao.Total = 0;
            foreach (var c in categorias)
            {
                acao.Total += c.Lucro;
            }

            // Mudança de valores em Relatorio;
            var acoes = _context.Acao.SqlQuery("SELECT * FROM Acaos a WHERE a.RelatorioId = @id", new SqlParameter("@id", acao.RelatorioId)).ToList();

            relatorio.Valor = 0;
            foreach (var a in acoes)
            {
                relatorio.Valor += a.Total;
            }
            
            _context.SaveChanges();

            return RedirectToAction("Index", "Item" + "/Index/" + i.CategoriaId);
        }

    }
}