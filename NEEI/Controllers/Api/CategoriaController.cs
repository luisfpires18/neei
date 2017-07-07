using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using NEEI.Models;

namespace NEEI.Controllers.Api
{
    public class CategoriaController : ApiController
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


        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            // Procura Categoria por Id;
            var categoria = _context.Categoria.SingleOrDefault(a => a.Id == id);
            
            // Poe o Lucro a 0;
            categoria.Lucro = 0;

            // Procura Acção por Id;
            var acao = _context.Acao.SingleOrDefault(a => a.Id == categoria.AcaoId);
            var categorias = _context.Categoria.SqlQuery("SELECT * FROM Categorias c WHERE c.AcaoId = @id", new SqlParameter("@id", categoria.AcaoId)).ToList();

            acao.Total = 0;
            foreach (var c in categorias)
            {
                acao.Total += c.Lucro;
            }

            // Atualiza o Total do Relatorio de Contas;
            var rc = _context.Relatorio.SingleOrDefault(r => r.Id == acao.RelatorioId);
            var acoes = _context.Acao.SqlQuery("SELECT * FROM Acaos a WHERE a.RelatorioId = @id", new SqlParameter("@id", acao.RelatorioId)).ToList();

            rc.Valor = 0;
            foreach (var a in acoes)
            {
                rc.Valor += a.Total;
            }

            _context.Categoria.Remove(categoria);
            _context.SaveChanges();

            return Ok();
        }
    }
}
